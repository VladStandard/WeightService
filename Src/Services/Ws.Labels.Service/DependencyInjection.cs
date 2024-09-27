using Microsoft.Extensions.DependencyInjection;
using Ws.Labels.Service.Extensions;
using Ws.Labels.Service.Generate;
using Ws.Labels.Service.Generate.Features.Piece;
using Ws.Labels.Service.Generate.Features.Weight;
using Ws.Labels.Service.Generate.Services;
using Ws.Labels.Service.Settings;

namespace Ws.Labels.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddLabelsServices(this IServiceCollection services, PalychSettings palychConfiguration)
    {
        services.AddScoped<IPrintLabelService, PrintLabelService>();

        services.AddScoped<CacheService>();
        services.AddTransient<ZplService>();
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