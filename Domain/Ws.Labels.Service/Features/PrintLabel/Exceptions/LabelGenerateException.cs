using FluentValidation.Results;

namespace Ws.Labels.Service.Features.PrintLabel.Exceptions;

public class LabelGenerateException : Exception
{
    public List<string> ValidationErrors { get; init; }
    
    public LabelGenerateException(ValidationResult result)
    {
        ValidationErrors =  result.Errors.Select(error => error.ErrorMessage).ToList();
    }
    
    public LabelGenerateException(string result)
    {
        ValidationErrors = [result];
    }
}