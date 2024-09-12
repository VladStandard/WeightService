using Ws.PalychExchange.Api.App.Features.Boxes.Dto;

namespace Ws.PalychExchange.Api.App.Shared.Extensions;

public static class DiExtension
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        Type validatorType = typeof(AbstractValidator<>);

        typeof(BoxDtoValidator).Assembly.GetTypes()
            .Where(t => t.BaseType is { IsGenericType: true }
                        && t.BaseType.GetGenericTypeDefinition() == validatorType)
            .ToList().
            ForEach(i => services.AddTransient(i));

        return services;
    }
}