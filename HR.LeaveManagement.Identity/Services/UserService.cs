using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly HrLeaveIdentityManagementIdentityDbContext _identityDbContext;

    public UserService(UserManager<IdentityUser> userManager, HrLeaveIdentityManagementIdentityDbContext identityDbContext)
    {
        _userManager = userManager;
        _identityDbContext = identityDbContext;
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
        // var employee = await _userManager.FindByIdAsync(userId);
        var employee = await _identityDbContext.Users
            .Include(x=> x.AspNetUser)
            .FirstOrDefaultAsync(x => x.AspNetUser.Id == userId);
        
        return new Employee
        {
            Id = employee.AspNetUser.Id,
            Email = employee.AspNetUser.Email,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
        };
    }
}