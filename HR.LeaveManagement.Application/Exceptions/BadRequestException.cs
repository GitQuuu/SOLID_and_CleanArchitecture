using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base($"{message}")
    {
       
    }
    
    public BadRequestException(string message, ValidationResult validationResult) : base($"{message}")
    {
        List<string> validationErrors = [];
        foreach (var error in validationErrors.ToList())
        {
            validationErrors.Add(error);
        }
    }

    public List<string> ValidationErrors { get; set; }
}