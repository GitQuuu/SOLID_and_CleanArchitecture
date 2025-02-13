using AutoMapper;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation;

public class GetLeaveAllocationHandler : IRequestHandler<GetLeaveAllocationQuery, GetLeaveAllocationDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public GetLeaveAllocationHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
    }
    
    public async Task<GetLeaveAllocationDto> Handle(GetLeaveAllocationQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetLeaveAllocationValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);
        if (leaveAllocation is null)
        {
            throw new NotFoundException("Leave allocation not found.", nameof(request.Id));
        }

        return _mapper.Map<GetLeaveAllocationDto>(leaveAllocation);
        
    }
}