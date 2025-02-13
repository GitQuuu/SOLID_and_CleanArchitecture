namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationsDto
{
    public int NumberOfDays { get; set; }
    public GetLeaveAllocationsLeaveTypeDto? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
}

public class GetLeaveAllocationsLeaveTypeDto
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}