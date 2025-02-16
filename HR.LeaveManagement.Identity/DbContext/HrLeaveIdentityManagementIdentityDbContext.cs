using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Identity.DbContext;

public class HrLeaveIdentityManagementIdentityDbContext : IdentityDbContext<IdentityUser>
{
    public HrLeaveIdentityManagementIdentityDbContext(
        DbContextOptions<HrLeaveIdentityManagementIdentityDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof
            (HrLeaveIdentityManagementIdentityDbContext).Assembly);
    }
}