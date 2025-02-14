using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public record GetLeaveAllocationsQuery : IRequest<List<GetLeaveAllocationsDto>>
{
    
}