using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Exceptions;

public class ValidateException : ServiceException
{
    public IDictionary<string, string> Errors { get; }
    
    public ValidateException(ValidationResult result)
    {
        Errors = new Dictionary<string, string>();

        foreach (ValidationFailure error in result.Errors)
        {
            if (Errors.ContainsKey(error.PropertyName)) continue;
            Errors.Add(error.PropertyName, error.ErrorMessage);
        }
    }
}