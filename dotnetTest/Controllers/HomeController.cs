using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnetTest.Models;
using dotnetTest.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace dotnetTest.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ProjectInfoRepository _projectInfoRepository;

    public HomeController(ILogger<HomeController> logger, ProjectInfoRepository projectInfoRepository)
    {
        _logger = logger;
        _projectInfoRepository = projectInfoRepository;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _projectInfoRepository.GetAllProjectInfosAsync();
       /* ViewBag.ProfilePhotoUrl = await GetUserProfilePhoto(); */
        return View(projects);
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
