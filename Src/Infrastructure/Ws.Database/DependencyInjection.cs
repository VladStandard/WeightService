using Microsoft.Extensions.DependencyInjection;

namespace Ws.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddEfCore(this IServiceCollection services)
    {
        services.AddDbContext<WsDbContext>();

        return services;
    }
}