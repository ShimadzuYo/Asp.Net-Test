using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models.Projects;

public class AddProjectViewModel
{
    public List<SelectListItem> EmployeesSelectListItems { get; set; }
    public AddProjectForm Form { get; set; }
}