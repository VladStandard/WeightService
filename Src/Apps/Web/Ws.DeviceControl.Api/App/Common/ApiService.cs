using FluentValidation;

namespace Ws.DeviceControl.Api.App.Common;

public abstract class ApiService
{
    protected static async Task ValidateAsync<T>(T dto, AbstractValidator<T> validator)
    {
        ValidationResult result = await validator.ValidateAsync(dto);
        if (!result.IsValid)
            throw new ApiInternalException
            {
                ErrorDisplayMessage = result.Errors[0].ToString(),
                StatusCode = HttpStatusCode.UnprocessableEntity
            };
    }
}