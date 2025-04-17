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
    public async Task<IActionResult> ProjectDetails(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var project = await _projectInfoRepository.GetProjectInfoByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        return View(project);
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
    
    
    public async Task<IActionResult> Search(string searchTerm, string projectId)
    {
        ViewBag.ProjectId = projectId;
        
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return View(new SearchViewModel
            {
                Results = new List<User>()
            });
        }
        
        var results = await _userRepository.SearchAsync(searchTerm);
    
        var viewModel = new SearchViewModel
        {
            SearchTerm = searchTerm,
            Results = results.ToList()
        };
    
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> AddRequirements(string projectId)
    {
        var project = await _projectInfoRepository.GetProjectInfoByIdAsync(projectId);
        if (project == null)
        {
            return NotFound();
        }
        ViewBag.ProjectId = projectId;
        return View(project);
    }

    [HttpPost]
    public async Task<IActionResult> AddRequirements(string projectId, string functionalRequirements, string nonFunctionalRequirements)
    {
        var project = await _projectInfoRepository.GetProjectInfoByIdAsync(projectId);
        if (project == null)
        {
            return NotFound();
        }

        // This split requirements by new line as we discussed
        project.FunctionalRequirements = functionalRequirements
            .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            .Select(r => r.Trim())
            .Where(r => !string.IsNullOrWhiteSpace(r))
            .ToList();

        project.NonFunctionalRequirements = nonFunctionalRequirements
            .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            .Select(r => r.Trim())
            .Where(r => !string.IsNullOrWhiteSpace(r))
            .ToList();

        await _projectInfoRepository.UpdateProjectInfoAsync(projectId, project);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var project = await _projectInfoRepository.GetProjectInfoByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    [HttpGet]
    public IActionResult AddRisk(string projectId)
    {
        ViewBag.ProjectId = projectId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddRisk(Risk risk)
    {
        if (ModelState.IsValid)
        {
            var project = await _projectInfoRepository.GetProjectInfoByIdAsync(risk.ProjectId);
            if (project != null)
            {
                if (project.Risks == null)
                {
                    project.Risks = new List<Risk>();
                }
                project.Risks.Add(risk);
                await _projectInfoRepository.UpdateProjectInfoAsync(risk.ProjectId, project);
                return RedirectToAction("Index");
            }
        }
        return View(risk);
    }

    [HttpPost]
    public async Task<IActionResult> AddMember(string projectId, string userId, string username, string firstName, string lastName, float hours, string role)
    {
        if (string.IsNullOrEmpty(projectId) || string.IsNullOrEmpty(username))
        {
            return BadRequest();
        }

        var project = await _projectInfoRepository.GetProjectInfoByIdAsync(projectId);
        if (project == null)
        {
            return NotFound();
        }

        // Initialize Members list if null
        if (project.Members == null)
        {
            project.Members = new List<Member>();
        }

        // Check if member already exists
        if (!project.Members.Any(m => m.UserName == username))
        {
            Member member = new Member(userId, username, firstName, lastName, hours, role);
            project.Members.Add(member);
            await _projectInfoRepository.UpdateProjectInfoAsync(projectId, project);
        }

        // Redirect back to the project details
        return RedirectToAction(nameof(Details), new { id = projectId });
    }
}
