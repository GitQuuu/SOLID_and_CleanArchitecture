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
        dynamic? problem;

        switch (exception)
        {
            case BadRequestException badRequestException:   
                problem = new CustomProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequestException.ValidationErrors,

                };
                break;
            case NotFoundException notFoundException:
                problem = new CustomProblemDetails
                {
                    Title = notFoundException.Message,
                    Status = (int)statusCode,
                    Detail = notFoundException.InnerException?.Message,
                    Type = nameof(NotFoundException),
                    Errors = notFoundException.ValidationErrors,

                };
                break;
            default:
                problem = new CustomProblemDetails
                {
                    Title = exception.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Detail = exception.StackTrace,
                };
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        await HttpResponseJsonExtensions.WriteAsJsonAsync(httpContext.Response, problem);
    }
}   