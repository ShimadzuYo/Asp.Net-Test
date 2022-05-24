using Domain.Enums;

namespace Application.Dtos;

public class AddProjectDto
{
    public string Name { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectPriorityEnum Priority { get; set; }

    public string ExecutiveCompanyName { get; set; }
    public string Client { get; set; }
    
    public List<int> EmployeeIds { get; set; }
    
    public int ProjectLeadId { get; set; }
}