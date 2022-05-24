using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models.Projects;

namespace WebApp.Controllers;

[Route("[controller]/[action]")]
public class ProjectsController : Controller
{
    private readonly IProjectService _projectService;
    private readonly IEmployeeService _employeeService;

    public ProjectsController(
        IProjectService projectService,
        IEmployeeService employeeService)
    {
        _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] ProjectSearchArguments projectSearchArguments)
    {
        var projects = await _projectService.GetProjectsAsync(projectSearchArguments);
        var viewModel = new ProjectListViewModel
        {
            Projects = projects,
        };
        return View(viewModel);
    }

    [HttpGet("{id:int}")]
    public IActionResult Details(int id)
    {
        var project = _projectService.GetProjectById(id);
        if (project is null)
            return RedirectToAction(nameof(List));
        var viewModel = new ProjectDetailsViewModel
        {
            Project = project
        };
        return View(viewModel);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _projectService.DeleteProject(id);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    { 
        var employees = await _employeeService.GetEmployeesForSelectListAsync();
        var selectListItems = employees.Select(x => 
                new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
        var viewModel = new AddProjectViewModel
        {
            EmployeesSelectListItems = selectListItems,
            Form = new AddProjectForm(),
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddProjectForm form)
    {
        if (!ModelState.IsValid)
        {
            
            var employees = await _employeeService.GetEmployeesForSelectListAsync();
            var selectListItems = employees.Select(x => 
                    new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
            var viewModel = new AddProjectViewModel
            {
                EmployeesSelectListItems = selectListItems,
                Form = form,
            };
            return View(viewModel);
        }

        var addProjectDto = new AddProjectDto
        {
            Name = form.Name,
            Client = form.Client,
            ExecutiveCompanyName = form.ExecutiveCompanyName,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            EmployeeIds = form.ProjectEmployeeIds,
            ProjectLeadId = form.ProjectLeadId,
        };
        var id = _projectService.AddProject(addProjectDto);
        return RedirectToAction("Details", new {id});
    }

    [HttpGet("{id:int}")]
    public IActionResult Edit(int id)
    {
        var project = _projectService.GetProjectById(id);
        var viewForm = new EditProjectForm
        {
            Name = project.Name,
            Client = project.Client,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            ExecutiveCompanyName = project.ExecutiveCompanyName,
            Priority = project.Priority
        };
        return View(viewForm);
    }

    [HttpPost("{id:int}")]
    public IActionResult Edit(int id, EditProjectForm form)
    {
        if (!ModelState.IsValid)
            return View();

        var editProjectDto = new EditProjectDto
        {
            Name = form.Name,
            Client = form.Client,
            ExecutiveCompanyName = form.ExecutiveCompanyName,
            StartDate = form.StartDate,
            EndDate = form.EndDate
        };
        _projectService.UpdateProject(id, editProjectDto);
        return RedirectToAction("Details", new {id});
    }
}