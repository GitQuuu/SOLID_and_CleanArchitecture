using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation;

public record GetLeaveAllocationQuery(int Id) : IRequest<GetLeaveAllocationDto>
{
}