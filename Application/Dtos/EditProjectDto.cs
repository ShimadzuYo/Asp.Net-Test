using Domain.Enums;

namespace Application.Dtos;

public class EditProjectDto
{
    public string Name { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectPriorityEnum Priority { get; set; }

    public string ExecutiveCompanyName { get; set; }
    public string Client { get; set; }
}