using FluentValidation;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation;

public class GetLeaveAllocationValidator : AbstractValidator<GetLeaveAllocationQuery>
{
    public GetLeaveAllocationValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}