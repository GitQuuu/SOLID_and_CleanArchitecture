using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(70)
            .WithMessage("{PropertyName} cannot be null or empty");
        
        RuleFor(p => p.DefaultDays) 
            .LessThan(100).WithMessage("{PropertyName} cannot exceed 100") 
            .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");
        
        RuleFor(q => q)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("{PropertyName} is already taken");

        _leaveTypeRepository = leaveTypeRepository;
    }

    private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken ctx)
    {
        return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}