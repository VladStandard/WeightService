using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ws.Labels.Service.Extensions;
using Ws.Labels.Service.Generate;
using Ws.Labels.Service.Generate.Features.Piece;
using Ws.Labels.Service.Generate.Features.Weight;
using Ws.Labels.Service.Generate.Services;

namespace Ws.Labels.Service;

public static class DependencyInjection
{
    public static void AddLabelsServices(this IServiceCollection services)
    {
        services.AddScoped<IPrintLabelService, PrintLabelService>();

        services.AddScoped<CacheService>();
        services.AddTransient<ZplService>();
        services.AddScoped<LabelPieceGenerator>();
        services.AddScoped<LabelWeightGenerator>();

        services.AddPalychApi();

        services.AddEasyCaching(option =>
        {
            option.WithProtobuf();
            option.UseRedis(
            new ConfigurationBuilder()
                .AddJsonFile("redis_config.json", optional: false, reloadOnChange: false).Build(), "ws-redis");
        });
    }
}