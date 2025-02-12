using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public DeleteLeaveTypeHandler(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }
    
    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteLeaveTypeValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Count != 0)
        {
            throw new BadRequestException("Delete leave type validation failed", validationResult);
        }   
        var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);
        if (leaveTypeToDelete is  null)
        {
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }
        
        await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);
        
        return Unit.Value;
    }
}