using FluentValidation.Results;

namespace Ws.Labels.Service.Features.PrintLabel.Exceptions;

public class LabelException : Exception
{
    public List<string> ValidationErrors { get; init; }
    
    public LabelException(ValidationResult result)
    {
        ValidationErrors =  result.Errors.Select(error => error.ErrorMessage).ToList();
    }
}