using Microsoft.Extensions.DependencyInjection;
using Ws.Services.Services.Host;
using Ws.Services.Services.Line;
using Ws.Services.Services.Plu;

namespace Ws.Services;

public static class DependencyInjection
{
    public static void AddVsServices(this IServiceCollection services)
    {
        services.AddScoped<IHostService, HostService>();
        services.AddScoped<ILineService, LineService>();
        services.AddScoped<IPluService, PluService>();
    }
}