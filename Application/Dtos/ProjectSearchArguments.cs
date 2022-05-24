using Domain.Enums;

namespace Application.Dtos;

public class ProjectSearchArguments
{
    public string FirstName { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectPriorityEnum? Priority { get; set; }

    public string ExecutiveCompanyName { get; set; }
    public string Client { get; set; }
}