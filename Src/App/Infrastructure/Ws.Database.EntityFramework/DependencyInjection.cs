using Microsoft.Extensions.DependencyInjection;

namespace Ws.Database.EntityFramework;

public static class DependencyInjection
{
    public static void AddEfCore(this IServiceCollection services)
    {
        services.AddDbContext<WsDbContext>();
    }
}