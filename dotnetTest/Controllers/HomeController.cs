using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnetTest.Models;
using Microsoft.AspNetCore.Authorization;

namespace dotnetTest.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string returnUrl = null)
    {
        _logger.LogError("An error occurred. ReturnUrl: {ReturnUrl}", returnUrl);
        
        // Clear any problematic return URLs to prevent redirect loops
        if (returnUrl?.Contains("/Home/Error") == true)
        {
            returnUrl = null;
        }

        var errorModel = new ErrorViewModel 
        { 
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            ReturnUrl = returnUrl
        };
        
        return View(errorModel);
    }
}
