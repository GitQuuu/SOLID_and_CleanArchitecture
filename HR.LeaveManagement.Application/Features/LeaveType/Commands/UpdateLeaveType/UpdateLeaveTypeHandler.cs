using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Logging;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveTypeHandler> _logger;

    public UpdateLeaveTypeHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, 
        IAppLogger<UpdateLeaveTypeHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }
    
    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
       var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);
       var validator = new UpdateLeaveTypeCommandValidator();
       var validationResult = await validator.ValidateAsync(request, cancellationToken);
       if (validationResult.Errors.Any())
       {
           _logger.LogWarning("Validation erros in update for {0} - {1}", nameof(LeaveType), request.Id );
           throw new NotFoundException(nameof(leaveTypeToUpdate), request);
       }
       await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);
       
       return Unit.Value;
    }
}