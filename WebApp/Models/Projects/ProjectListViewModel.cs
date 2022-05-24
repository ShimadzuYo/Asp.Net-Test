using Application.Dtos;
using Domain;

namespace WebApp.Models.Projects;

public class ProjectListViewModel
{
    public List<Project> Projects { get; set; }
    public ProjectSearchArguments ProjectSearchArguments { get; set; }
}