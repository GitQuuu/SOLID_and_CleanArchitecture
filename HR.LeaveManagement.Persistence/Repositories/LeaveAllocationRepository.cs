using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {
    }

    public Task<LeaveAllocation?> GetLeaveAllocationWithDetails(int id)
    {
       var leaveAllocation = _context.LeaveAllocations
           .Include(x => x.LeaveType)
           .FirstOrDefaultAsync(x => x.Id == id);
       
       return leaveAllocation;
    }

    public async Task<List<LeaveAllocation?>> GetLeaveAllocationWithDetails()
    {
        var leaveAllocations = await _context.LeaveAllocations
            .Include(x => x.LeaveType)
            .ToListAsync();
        
        if (leaveAllocations.Count == 0)
        {
            return new List<LeaveAllocation>();
        }
        return leaveAllocations;
    }

    public Task<List<LeaveAllocation?>> GetLeaveAllocationWithDetails(string userId)
    {
        var leaveAllocations = _context.LeaveAllocations
            .Where(x => x.EmployeeId == userId)
            .Include(x => x.LeaveType)
            .ToListAsync();
        return leaveAllocations;
    }

    public async Task<bool> AllocationExist(string userId, int leaveTypeId, int period)
    {
       return await _context.LeaveAllocations.AnyAsync( x => x.EmployeeId == userId 
                                                             && x.LeaveTypeId == leaveTypeId 
                                                             && x.Period == period);
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _context.LeaveAllocations.AddRangeAsync(allocations);
        await _context.SaveChangesAsync();
    }

    public async Task<LeaveAllocation?> GetUserAllocations(string userId, int leaveTypeId)
    {
       return await _context.LeaveAllocations
           .FirstOrDefaultAsync(x => x.EmployeeId == userId && x.LeaveTypeId == leaveTypeId);
    }
}