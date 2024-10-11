using Ws.Desktop.Api.App.Shared.Labels.Extensions;
using Ws.Desktop.Api.App.Shared.Labels.Generate;
using Ws.Desktop.Api.App.Shared.Labels.Generate.Features.Piece;
using Ws.Desktop.Api.App.Shared.Labels.Generate.Features.Weight;
using Ws.Desktop.Api.App.Shared.Labels.Generate.Services;
using Ws.Desktop.Api.App.Shared.Labels.Settings;

namespace Ws.Desktop.Api.App.Shared.Labels;

public static class DependencyInjection
{
    public static IServiceCollection AddLabelsServices(this IServiceCollection services, PalychSettings palychConfiguration)
    {
        services.AddScoped<IPrintLabelService, PrintLabelService>();

        services.AddScoped<CacheService>();
        services.AddScoped<LabelPieceGenerator>();
        services.AddScoped<LabelWeightGenerator>();

        services.AddPalychApi(palychConfiguration);

        // services.AddEasyCaching(option =>
        // {
        //     option.WithProtobuf();
        //     option.UseRedis(
        //     new ConfigurationBuilder()
        //         .AddJsonFile("redis_config.json", optional: false, reloadOnChange: false).Build(), "ws-redis");
        // });

        return services;
    }
}