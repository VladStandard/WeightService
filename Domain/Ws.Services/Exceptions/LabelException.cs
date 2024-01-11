using FluentValidation.Results;

namespace Ws.Services.Exceptions;

public class LabelException : Exception
{
    public List<string> ValidationErrors { get; init; }
    
    public LabelException(ValidationResult result)
    {
        ValidationErrors =  result.Errors.Select(error => error.ErrorMessage).ToList();;
    }
}