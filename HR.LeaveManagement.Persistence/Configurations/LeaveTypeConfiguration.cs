using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistence.Configurations;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.HasData(
            new LeaveType
            {
                Id = 1,
                Name = "Vacation",
                DefaultDays = 10,
                DateCreated = DateTime.Parse("2024/12/12 10:00"),
                DateModified = DateTime.Parse("2024/12/12 10:00"),
            }
        );
        
        // database level restriction
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}