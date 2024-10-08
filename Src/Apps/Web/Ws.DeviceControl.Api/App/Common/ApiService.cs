using System.Net;
using FluentValidation;

namespace Ws.DeviceControl.Api.App.Common;

public abstract class ApiService
{
    protected static async Task ValidateAsync<T>(T dto, AbstractValidator<T> validator)
    {
        ValidationResult result = await validator.ValidateAsync(dto);
        if (!result.IsValid)
            throw new ApiExceptionServer
            {
                ErrorDisplayMessage = result.Errors.FirstOrDefault()?.ToString() ?? string.Empty,
                StatusCode = HttpStatusCode.UnprocessableEntity
            };
    }
}