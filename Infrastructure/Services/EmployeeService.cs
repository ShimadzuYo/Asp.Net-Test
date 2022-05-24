using Application.Dtos;
using Application.Interfaces;
using Domain;
using Infrastructure.EntityFramework;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public int AddEmployee(AddEmployeeDto addEmployeeDto)
    {
        if (addEmployeeDto is null)
            throw new ArgumentNullException(nameof(addEmployeeDto));

        var employee = new Employee
        {
            Id = addEmployeeDto.Id,
            FirstName = addEmployeeDto.FirstName,
            MiddleName = addEmployeeDto.MiddleName,
            LastName = addEmployeeDto.LastName,
            Email = addEmployeeDto.Email,
        };
        _dbContext.Employees.Add(employee);
        _dbContext.SaveChanges();

        return employee.Id;
    }

    public Employee GetEmployeeById(int id)
    {
        var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
        return employee;
    }

    public void UpdateEmployee(int id, EditEmployeeDto newEmployee)
    {
        var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
        if (employee == null)
            throw new ArgumentException("Project not found!");
        employee.FirstName = newEmployee.FirstName;
        employee.LastName = newEmployee.LastName;
        employee.MiddleName = newEmployee.MiddleName;
        employee.Email = newEmployee.Email;

        _dbContext.SaveChanges();
    }

    public void DeleteEmployee(int id)
    {
        var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
        if (employee is null)
            return;
        _dbContext.Employees.Remove(employee);
        _dbContext.SaveChanges();
    }

    public async Task<List<EmployeeShortInfoDto>> GetEmployeesForSelectListAsync()
    {
        var result =  await _dbContext.Employees.Select(x => new EmployeeShortInfoDto
        {
            Id = x.Id,
            Name = x.FirstName + " " + x.LastName, 
        }).ToListAsync();
        return result;
    }

    public List<Employee> GetEmployees(EmployeeSearchArguments searchArguments)
    {
        var employees = _dbContext.Employees;
        var filter = CreateFilter(searchArguments);

        var result = employees
            .Where(filter)
            .ToList();
        return result;
    }


    private static ExpressionStarter<Employee> CreateFilter(EmployeeSearchArguments searchArguments)
    {
        var filter = PredicateBuilder.New<Employee>(true);

        if (searchArguments.Id.HasValue)
        {
            filter = filter.And(x => x.Id == searchArguments.Id);
        }

        if (!string.IsNullOrWhiteSpace(searchArguments.FirstName))
        {
            filter = filter.And(x => x.FirstName.Contains(searchArguments.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(searchArguments.MiddleName))
        {
            filter = filter.And(x => x.MiddleName.Contains(searchArguments.MiddleName));
        }

        if (!string.IsNullOrWhiteSpace(searchArguments.LastName))
        {
            filter = filter.And(x => x.LastName.Contains(searchArguments.LastName));
        }

        if (!string.IsNullOrWhiteSpace(searchArguments.Email))
        {
            filter = filter.And(x => x.Email.Contains(searchArguments.Email));
        }


        return filter;
    }
}