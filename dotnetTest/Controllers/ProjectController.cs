using System.Security.Claims;
using dotnetTest.Models;
using dotnetTest.Repositories;

namespace dotnetTest.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class ProjectController : BaseController
{
    private readonly ProjectInfoRepository _projectInfoRepository;

    public ProjectController(ProjectInfoRepository projectInfoRepository, UserRepository userRepository)
        : base(userRepository)
    {
        _projectInfoRepository = projectInfoRepository;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _projectInfoRepository.GetAllProjectInfosAsync();
        ViewBag.ProfilePhotoUrl = await GetUserProfilePhoto();
        return View(projects);
    }

    public async Task<IActionResult> TeamInfo()
    {
        var projects = await _projectInfoRepository.GetAllProjectInfosAsync();
        return View(projects);
    }

    public async Task<IActionResult> Reports()
    {
        var projects = await _projectInfoRepository.GetAllProjectInfosAsync();
        return View(projects);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(ProjectInfo project)
    {
        project.ProjectActive = true;
        var userId = await _userRepository.GetUserIdByUsernameAsync(User.Identity.Name);
        if (string.IsNullOrEmpty(userId))
        {
            Console.WriteLine("User ID could not be found for the current user.");
            return View(project);
        }
        project.Id = userId;
        
        if (project.Tasks == null)
        {
            project.Tasks = new List<Tasks>();
        }
        
        if (ModelState.IsValid)
        {
            await _projectInfoRepository.CreateProjectInfoAsync(project);
            return RedirectToAction("Index");
        }
        
        foreach (var key in ModelState.Keys)
        {
            var errors = ModelState[key].Errors;
            foreach (var error in errors)
            {
                Console.WriteLine($"Validation error in {key}: {error.ErrorMessage}");
            }
        }
        
        return View(project);
    }

    [HttpGet]
    public IActionResult AddTask(string projectId)
    {
        ViewBag.ProjectId = projectId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddTask(Tasks task)
    {
        if (ModelState.IsValid)
        {
            task.TaskActive = true;
            var project = await _projectInfoRepository.GetProjectInfoByIdAsync(task.ProjectId);
            if (project != null)
            {
                if (project.Tasks == null)
                {
                    project.Tasks = new List<Tasks>();
                }
                project.Tasks.Add(task);
                await _projectInfoRepository.UpdateProjectInfoAsync(task.ProjectId, project);
                return RedirectToAction("Index");
            }
        }
        return View(task);
    }
}
