using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace WebApp.Models.Projects;

public class AddProjectForm
{
    [Required] [MaxLength(50)] public string Name { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectPriorityEnum Priority { get; set; }

    [Required] [MaxLength(50)] public string ExecutiveCompanyName { get; set; }

    [Required] [MaxLength(50)] public string Client { get; set; }
    
    public List<int> ProjectEmployeeIds { get; set; }
    
    public int ProjectLeadId { get; set; }
}