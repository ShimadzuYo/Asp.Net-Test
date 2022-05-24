using Application.Dtos;
using Domain;

namespace WebApp.Models.Employees;

public class EmployeeListViewModel
{
    public List<Employee> Employees { get; set; }

    public EmployeeSearchArguments EmployeeSearchArguments { get; set; }
}