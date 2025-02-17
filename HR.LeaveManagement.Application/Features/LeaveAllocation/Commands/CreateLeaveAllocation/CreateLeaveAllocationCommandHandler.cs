using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public CreateLeaveAllocationCommandHandler(
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveAllocationRepository leaveAllocationRepository,
        IMapper mapper,
        IUserService userService)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Validation Failed", validationResult);
        }
        
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        var employees = await _userService.GetEmployeesAsync();
        
        var period = DateTime.UtcNow.Year;

        var allocations = new List<Domain.LeaveAllocation>();
        foreach (var employee in employees)
        {
            var allocationsExist = await _leaveAllocationRepository.AllocationExist(employee.Id, request.LeaveTypeId, period);
            if (allocationsExist is false)
            {
                allocations.Add(new Domain.LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = request.LeaveTypeId,
                    Period = period,
                    NumberOfDays = leaveType.DefaultDays,
                });
            }
        }

        if (allocations.Any())
        {
            await _leaveAllocationRepository.AddAllocations(allocations);
        }
        
        
        // var entity = _mapper.Map<Domain.LeaveAllocation>(request);
        // await _leaveAllocationRepository.CreateAsync(entity);
        
        return Unit.Value;
    }
}