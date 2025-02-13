using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public CreateLeaveAllocationHandler(
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveAllocationRepository leaveAllocationRepository,
        IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Validation Failed", validationResult);
        }
        var entity = _mapper.Map<Domain.LeaveAllocation>(request);
        await _leaveAllocationRepository.CreateAsync(entity);
        
        return Unit.Value;
    }
}