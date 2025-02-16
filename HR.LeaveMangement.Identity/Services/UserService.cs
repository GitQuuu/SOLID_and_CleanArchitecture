using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace HR.LeaveMangement.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<List<Employee>> GetEmployeesAsync()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");
        return employees.Select(x => new Employee
        {
            Id = x.AspNetUser.Id,
            Email = x.AspNetUser.Email,
            FirstName = x.FirstName,
            LastName = x.LastName,
        }).ToList();
    }

    public async Task<Employee> GetEmployeeByIdAsync(string userId)
    {
        var employee = await _userManager.FindByIdAsync(userId);
        return new Employee
        {
            Id = employee.AspNetUser.Id,
            Email = employee.AspNetUser.Email,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
        };
    }
}