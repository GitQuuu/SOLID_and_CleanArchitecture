using System.Net;
using HR.LeaveManagement.Api.Models;
using HR.LeaveManagement.Application.Exceptions;

namespace HR.LeaveManagement.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        dynamic problem = null;

        switch (exception)
        {
            case BadRequestException badRequestException:   
                problem = new CustomValidationProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequestException.ValidationErrors,

                };
                break;
            case NotFoundException notFoundException:
                break;
            default:
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}   