using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<GetLeaveAllocationDto, LeaveAllocation>().ReverseMap();
        CreateMap<LeaveAllocation,GetLeaveAllocationDto>();
        CreateMap<CreateLeaveAllocationCommand,LeaveAllocation>();
        CreateMap<UpdateLeaveAllocationCommand,LeaveAllocation>();
    }
}