using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
       services.AddAutoMapper(Assembly.GetExecutingAssembly());
       services.AddMediatR(cfg => Assembly.GetExecutingAssembly());
       
       return services;
    }
}