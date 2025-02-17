using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Application.Models.Identity;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IdentityUser? AspNetUser { get; set; }
}