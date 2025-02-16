using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<List<Employee>> GetEmployeesAsync()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");
        return employees.Select(x => new Employee
        {
            Id = x.Id,
            Email = x.Email,
        }).ToList();
    }

    public async Task<Employee> GetEmployeeByIdAsync(string userId)
    {
        var employee = await _userManager.FindByIdAsync(userId);
        return new Employee
        {
            Id = employee.Id,
            Email = employee.Email,
        };
    }
}