using Application.Dtos;
using Application.Interfaces;
using Domain;
using Infrastructure.EntityFramework;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProjectService : IProjectService
{
    private readonly ApplicationDbContext _dbContext;

    public ProjectService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Project>> GetProjectsAsync(ProjectSearchArguments searchArguments)
    {
        var projects = _dbContext.Projects;
        var filter = CreateFilter(searchArguments);

        var result = await projects
            .Where(filter)
            .ToListAsync();
        return result;
    }

    public List<Project> GetProjects(ProjectSearchArguments searchArguments)
    {
        var projects = _dbContext.Projects;
        var filter = CreateFilter(searchArguments);

        var result = projects
            .Where(filter)
            .ToList();
        return result;
    }

    private static ExpressionStarter<Project> CreateFilter(ProjectSearchArguments projectSearchArguments)
    {
        var filter = PredicateBuilder.New<Project>(true);
        if (!string.IsNullOrWhiteSpace(projectSearchArguments.FirstName))
        {
            filter = filter.And(x => x.Name.Contains(projectSearchArguments.FirstName));
        }

        if (projectSearchArguments.Priority.HasValue)
        {
            filter = filter.And(x => x.Priority == projectSearchArguments.Priority);
        }

        if (projectSearchArguments.StartDate.HasValue)
        {
            filter = filter.And(x => x.StartDate >= projectSearchArguments.StartDate);
        }

        if (projectSearchArguments.EndDate.HasValue)
        {
            filter = filter.And(x => x.EndDate >= projectSearchArguments.EndDate);
        }

        if (!string.IsNullOrWhiteSpace(projectSearchArguments.Client))
        {
            filter = filter.And(x => x.Client.Contains(projectSearchArguments.Client));
        }

        if (!string.IsNullOrWhiteSpace(projectSearchArguments.ExecutiveCompanyName))
        {
            filter = filter.And(x => x.ExecutiveCompanyName.Contains(projectSearchArguments.ExecutiveCompanyName));
        }

        return filter;
    }

    public Project GetProjectById(int id)
    {
        // eager loading
        var project = _dbContext.Projects
            .Include(x => x.ProjectLead)
            .Include(x => x.Employees)
            .ThenInclude(x => x.Employee)
            .FirstOrDefault(x => x.Id == id);
        return project;
    }

    public int AddProject(AddProjectDto addProjectDto)
    {
        if (addProjectDto is null)
            throw new ArgumentNullException(nameof(addProjectDto));

        var project = new Project
        {
            Name = addProjectDto.Name,
            Client = addProjectDto.Client,
            Priority = addProjectDto.Priority,
            StartDate = addProjectDto.StartDate,
            EndDate = addProjectDto.EndDate,
            ExecutiveCompanyName = addProjectDto.ExecutiveCompanyName,
            Employees = addProjectDto.EmployeeIds.Select(x => new EmployeeProject {EmployeeId = x}).ToList(),
            ProjectLeadId = addProjectDto.ProjectLeadId,
        };
        _dbContext.Projects.Add(project);
        _dbContext.SaveChanges();

        return project.Id;
    }

    public void UpdateProject(int id, EditProjectDto editProjectDto)
    {
        var project = _dbContext.Projects.FirstOrDefault(x => x.Id == id);
        if (project == null)
            throw new ArgumentException("Project not found!");
        project.Name = editProjectDto.Name;
        project.Priority = editProjectDto.Priority;
        project.StartDate = editProjectDto.StartDate;
        project.EndDate = editProjectDto.EndDate;
        project.Client = editProjectDto.Client;
        project.ExecutiveCompanyName = editProjectDto.ExecutiveCompanyName;

        _dbContext.SaveChanges();
    }

    public void DeleteProject(int id)
    {
        var project = _dbContext.Projects.FirstOrDefault(x => x.Id == id);
        if (project is null)
            return;
        _dbContext.Projects.Remove(project);
        _dbContext.SaveChanges();
    }
}