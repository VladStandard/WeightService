using Microsoft.Extensions.DependencyInjection;
using Ws.Labels.Service.Features.Generate;
using Ws.Labels.Service.Features.Generate.Features.Piece;
using Ws.Labels.Service.Features.Generate.Features.Weight;
using Ws.Labels.Service.Features.Generate.Services;

namespace Ws.Labels.Service;

public static class DependencyInjection
{
    public static void AddLabelsServices(this IServiceCollection services)
    {
        services.AddScoped<IPrintLabelService, PrintLabelService>();

        services.AddScoped<CacheService>();
        services.AddScoped<LabelPieceGenerator>();
        services.AddScoped<LabelWeightGenerator>();
    }
}