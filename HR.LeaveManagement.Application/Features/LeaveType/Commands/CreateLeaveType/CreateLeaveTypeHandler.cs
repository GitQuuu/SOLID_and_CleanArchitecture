using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming request
        var validator = new CreateLeaveTypeCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid is not true)
        {
            throw new BadRequestException("Validation Failed", validationResult);
        }
        
        // Convert to domain entity object 
        var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);
        
        // add to db
        
        await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);
        
        // return id
        return leaveTypeToCreate.Id;
    }
}