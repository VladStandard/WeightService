using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ws.Labels.Service.Api;
using Ws.Labels.Service.Extensions;
using Ws.Labels.Service.Generate;
using Ws.Labels.Service.Generate.Features.Piece;
using Ws.Labels.Service.Generate.Features.Weight;
using Ws.Labels.Service.Generate.Services;
using Ws.Shared.Utils;

namespace Ws.Labels.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddLabelsServices(this IServiceCollection services,
        IConfigurationSection palychConfiguration)
    {
        services.AddScoped<IPrintLabelService, PrintLabelService>();

        services.AddScoped<CacheService>();
        services.AddTransient<ZplService>();
        services.AddScoped<LabelPieceGenerator>();
        services.AddScoped<LabelWeightGenerator>();

        services.AddPalychApi(
        palychConfiguration.Get<PalychSettingsModel>() ?? throw new NullReferenceException()
        );


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