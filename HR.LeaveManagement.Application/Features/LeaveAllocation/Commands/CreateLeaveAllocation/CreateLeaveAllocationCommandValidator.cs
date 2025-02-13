using FluentValidation;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    public CreateLeaveAllocationCommandValidator()
    {
        RuleFor(x => x.NumberOfDays).NotNull();
        RuleFor(x => x.Period).NotNull();
        RuleFor(x => x.EmployeeId).NotNull();
    }
}