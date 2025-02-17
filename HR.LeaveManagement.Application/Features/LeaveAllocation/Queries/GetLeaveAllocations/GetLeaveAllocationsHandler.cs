using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationsHandler : IRequestHandler<GetLeaveAllocationsQuery, List<GetLeaveAllocationsDto>>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public GetLeaveAllocationsHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }
    
    public async Task<List<GetLeaveAllocationsDto>> Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken)
    {
        var list = await _leaveAllocationRepository.GetLeaveAllocationWithDetails();
        return _mapper.Map<List<GetLeaveAllocationsDto>>(list);
    }
}

