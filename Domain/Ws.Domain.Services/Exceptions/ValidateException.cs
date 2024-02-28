namespace Ws.Domain.Services.Exceptions;

public class ValidateException : Exception
{
    public IDictionary<string, string> Errors { get; private set; }
    
    
    public ValidateException(ValidationResult result) : base()
    {
        Errors = new Dictionary<string, string>();

        foreach (ValidationFailure error in result.Errors)
        {
            Errors.Add(error.PropertyName, error.ErrorMessage);
        }
    }
}