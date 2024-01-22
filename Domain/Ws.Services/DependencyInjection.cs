using Microsoft.Extensions.DependencyInjection;
using Ws.Services.Features.Line;
using Ws.Services.Features.Plu;

namespace Ws.Services;

public static class DependencyInjection
{
    public static void AddVsServices(this IServiceCollection services)
    {
        services.AddScoped<ILineService, LineService>();
        services.AddScoped<IPluService, PluService>();
    }
}