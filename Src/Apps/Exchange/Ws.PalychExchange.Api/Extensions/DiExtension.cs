using FluentValidation;
using Ws.PalychExchange.Api.Features.Boxes.Dto;

namespace Ws.PalychExchange.Api.Extensions;

public static class DiExtension
{
    public static void AddValidators(this IServiceCollection services)
    {
        Type validatorType = typeof(AbstractValidator<>);

        List<Type> validatorTypes = typeof(BoxDtoValidator).Assembly.GetTypes().Where(t =>
                t.BaseType is { IsGenericType: true } && t.BaseType.GetGenericTypeDefinition() == validatorType
            ).ToList();

        foreach (var validator in validatorTypes)
            services.AddTransient(validator);
    }
}