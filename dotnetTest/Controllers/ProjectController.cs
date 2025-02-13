using dotnetTest.Models;
using dotnetTest.Repositories;

namespace dotnetTest.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class ProjectController(ProjectInfoRepository projectInfoRepository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var projects = await projectInfoRepository.GetAllProjectInfosAsync();
        return View(projects);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(ProjectInfo project)
    {
        if (project.Tasks == null || project.Tasks.Count == 0)
        {
            ModelState.AddModelError("Tasks", "At least one task is required.");
        }
        if (ModelState.IsValid)
        {
            Console.WriteLine("Success: "+ project.ToString());
            await projectInfoRepository.CreateProjectInfoAsync(project);
            //projectInfoRepository.AddProject(project);
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
        Console.WriteLine("Error: "+ project.ToString());
        
        return View(project);
    }
    
    /*
     [HttpPost]
    public IActionResult Create(ProjectInfo project)
    {
        if (ModelState.IsValid)
        {
            //await projectInfoRepository.CreateProjectInfoAsync(project);
            projectInfoRepository.AddProject(project);
            return RedirectToAction("Index");
        }
        return View(project);
    }
    */
}
