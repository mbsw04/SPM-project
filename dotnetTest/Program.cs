using dotnetTest.Data;
using dotnetTest.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // Get PORT from environment variable or use default 10000
    //var port = int.Parse(Environment.GetEnvironmentVariable("PORT") ?? "10000");
    var port = 5000;
    serverOptions.ListenAnyIP(port); // Bind to 0.0.0.0:PORT
});

// Load .env file only in Development
if (builder.Environment.IsDevelopment())
{
    Env.Load();
}

// Add services to the container.
builder.Services.AddControllersWithViews();
// Register MongoDbContext
builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddScoped<ProjectInfoRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddHttpContextAccessor();

// Add Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
        options.SlidingExpiration = true;
        // Prevent redirect loops
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                // Don't redirect to login if it's an API request or Error page
                if (context.Request.Path.StartsWithSegments("/Home/Error") || 
                    context.Request.Path.StartsWithSegments("/api"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                }
                context.Response.Redirect(context.RedirectUri);
                return Task.CompletedTask;
            }
        };
    });

// Add Authorization with default policy requiring authentication
builder.Services.AddAuthorization(options =>
{
    // Set default policy
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    // Add policy for Error page to allow anonymous access
    options.AddPolicy("AllowAnonymous", policy =>
    {
        policy.RequireAssertion(context => true);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Show detailed errors in development
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Add global error handling
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An unhandled exception occurred.");

        if (app.Environment.IsDevelopment())
        {
            throw; // Re-throw in development to show detailed error page
        }

        context.Response.Redirect("/Home/Error");
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Map routes with specific authorization policies
app.MapControllerRoute(
    name: "error",
    pattern: "Home/Error",
    defaults: new { controller = "Home", action = "Error" })
    .AllowAnonymous();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

