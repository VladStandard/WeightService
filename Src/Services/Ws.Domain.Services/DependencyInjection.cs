using Microsoft.Extensions.DependencyInjection;
using Ws.Database.Nhibernate;
using Ws.Domain.Services.Features.Arms;
using Ws.Domain.Services.Features.Labels;
using Ws.Domain.Services.Features.PalletMen;
using Ws.Domain.Services.Features.Pallets;
using Ws.Domain.Services.Features.Plus;
using Ws.Domain.Services.Features.Templates;
using Ws.Domain.Services.Features.ZplResources;
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

        services.AddScoped<IPluService, PluService>();
        services.AddScoped<IArmService, ArmService>();
        services.AddScoped<ILabelService, LabelService>();
        services.AddScoped<IPalletService, PalletService>();
        services.AddScoped<ITemplateService, TemplateService>();
        services.AddScoped<IPalletManService, PalletManService>();
        services.AddScoped<IZplResourceService, ZplResourceService>();
    }
}