using Microsoft.Extensions.DependencyInjection;
using Ws.Database.Nhibernate;
using Ws.Domain.Services.Features;
using Ws.Domain.Services.Features.Plus;
using Ws.Domain.Services.Redis;

namespace Ws.Domain.Services;

public static class DependencyInjection
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddNhibernate();

        services.AddEasyCaching(option =>
        {
            option.WithProtobuf();
            option.UseRedis(RedisUtils.LoadRedisCfg(), "ws-redis");
        });

        services.AddScoped<ArmService>();
        services.AddScoped<LabelService>();
        services.AddScoped<ZplResourceService>();
        services.AddScoped<TemplateService>();
        services.AddScoped<PalletService>();
        services.AddScoped<PalletManService>();

        services.AddScoped<PluService>();
    }
}