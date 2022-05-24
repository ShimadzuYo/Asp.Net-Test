using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Employees;

public class EditEmployeeForm
{
    [Required] [MaxLength(50)] public string FirstName { get; set; }
    [MaxLength(50)] public string SecondName { get; set; }
    [Required] [MaxLength(50)] public string LastName { get; set; }
    [MaxLength(50)] public string Email { get; set; }
}