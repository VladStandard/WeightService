using Ws.PalychExchange.Api.App.Features.Boxes.Dto;

namespace Ws.PalychExchange.Api.App.Shared.Extensions;

public static class DiExtension
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        Type validatorType = typeof(AbstractValidator<>);

        List<Type> validatorTypes = typeof(BoxDtoValidator).Assembly.GetTypes().Where(t =>
                t.BaseType is { IsGenericType: true } && t.BaseType.GetGenericTypeDefinition() == validatorType
            ).ToList();

        foreach (var validator in validatorTypes)
            services.AddTransient(validator);

        return services;
    }
}