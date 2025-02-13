using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommand : IRequest<int>
{
    public int NumberOfDays { get; set; }
    public CreateLeaveAllocationLeaveType ? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
}

public class CreateLeaveAllocationLeaveType
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}