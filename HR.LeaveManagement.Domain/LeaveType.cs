namespace HR.LeaveManagement.Domain;

public class LeaveType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}

public class LeaveRequest
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime DateRequested { get; set; }
    public string? RequestComments  { get; set; }
    public bool Approved { get; set; }
    public bool Cancelled { get; set; }
    public string? RequestingEmployeeId { get; set; }
}