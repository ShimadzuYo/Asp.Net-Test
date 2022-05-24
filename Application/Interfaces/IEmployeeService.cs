using Application.Dtos;
using Domain;

namespace Application.Interfaces;

public interface IEmployeeService
{
    List<Employee> GetEmployees(EmployeeSearchArguments searchArguments);
    Employee GetEmployeeById(int id);
    void UpdateEmployee(int id, EditEmployeeDto employeeDto);
    int AddEmployee(AddEmployeeDto addEmployeeDto);
    void DeleteEmployee(int id);

    Task<List<EmployeeShortInfoDto>> GetEmployeesForSelectListAsync();
}