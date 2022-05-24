using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Employees;

public class AddEmployeeForm
{
    [Required] public int Id { get; set; }
    [Required] [MaxLength(50)] public string FirstName { get; set; }
    [Required] [MaxLength(50)] public string MiddleName { get; set; }
    [Required] [MaxLength(50)] public string LastName { get; set; }
    [Required] [MaxLength(50)] public string Email { get; set; }
}