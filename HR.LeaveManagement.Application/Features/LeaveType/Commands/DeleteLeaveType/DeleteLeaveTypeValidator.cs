using FluentValidation;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeValidator : AbstractValidator<DeleteLeaveTypeCommand>
{
    public DeleteLeaveTypeValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
    }
}