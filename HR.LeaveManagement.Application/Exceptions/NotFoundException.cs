using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key) : base($"{name} {key} was not found")
    {
       
    }
    
    public NotFoundException(string message, ValidationResult validationResult) : base($"{message}")
    {
        ValidationErrors = validationResult.ToDictionary();
    }

    public IDictionary<string, string[]> ValidationErrors { get; set; }
}