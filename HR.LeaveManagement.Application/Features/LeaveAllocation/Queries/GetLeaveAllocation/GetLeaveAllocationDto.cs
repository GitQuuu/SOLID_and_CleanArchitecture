namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation;

public class GetLeaveAllocationDto
{
    public int NumberOfDays { get; set; }
    public GetLeaveAllocationLeaveTypeDto? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
}

public class GetLeaveAllocationLeaveTypeDto
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}