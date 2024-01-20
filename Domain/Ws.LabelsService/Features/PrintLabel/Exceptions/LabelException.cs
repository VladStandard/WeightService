using FluentValidation.Results;

namespace Ws.LabelsService.Features.PrintLabel.Exceptions;

public class LabelException : Exception
{
    public List<string> ValidationErrors { get; init; }
    
    public LabelException(ValidationResult result)
    {
        ValidationErrors =  result.Errors.Select(error => error.ErrorMessage).ToList();
    }
}