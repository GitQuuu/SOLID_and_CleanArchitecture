using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(70)
            .WithMessage("{PropertyName} cannot be null or empty");
        
        RuleFor(q => q)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("{PropertyName} is already taken");
    }

    private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken ctx)
    {
        return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}