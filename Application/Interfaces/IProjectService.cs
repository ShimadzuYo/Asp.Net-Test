using Application.Dtos;
using Domain;

namespace Application.Interfaces;

public interface IProjectService
{
    Task<List<Project>> GetProjectsAsync(ProjectSearchArguments searchArguments);
    List<Project> GetProjects(ProjectSearchArguments searchArguments);
    Project GetProjectById(int id);
    int AddProject(AddProjectDto addProjectDto);
    void UpdateProject(int id, EditProjectDto editProjectDto);
    void DeleteProject(int id);
}