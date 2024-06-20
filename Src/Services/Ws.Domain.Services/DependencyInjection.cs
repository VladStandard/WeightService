using Microsoft.Extensions.DependencyInjection;
using Ws.Database.Nhibernate;
using Ws.Domain.Services.Features.Arms;
using Ws.Domain.Services.Features.Boxes;
using Ws.Domain.Services.Features.Brands;
using Ws.Domain.Services.Features.Bundles;
using Ws.Domain.Services.Features.Clips;
using Ws.Domain.Services.Features.DatabaseFiles;
using Ws.Domain.Services.Features.Labels;
using Ws.Domain.Services.Features.PalletMen;
using Ws.Domain.Services.Features.Pallets;
using Ws.Domain.Services.Features.Plus;
using Ws.Domain.Services.Features.Printers;
using Ws.Domain.Services.Features.ProductionSites;
using Ws.Domain.Services.Features.StorageMethods;
using Ws.Domain.Services.Features.Templates;
using Ws.Domain.Services.Features.Users;
using Ws.Domain.Services.Features.Warehouses;
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

        services.AddScoped<IBoxService, BoxService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IBundleService, BundleService>();
        services.AddScoped<IClipService, ClipService>();
        services.AddScoped<IDatabaseFileService, DatabaseFileService>();
        services.AddScoped<ILabelService, LabelService>();
        services.AddScoped<IArmService, ArmService>();
        services.AddScoped<IPluService, PluService>();
        services.AddScoped<IPrinterService, PrinterService>();
        services.AddScoped<IProductionSiteService, ProductionSiteService>();
        services.AddScoped<IStorageMethodService, StorageMethodService>();
        services.AddScoped<ITemplateService, TemplateService>();
        services.AddScoped<IZplResourceService, ZplResourceService>();
        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddScoped<IPalletManService, PalletManService>();
        services.AddScoped<IPalletService, PalletService>();

        services.AddSingleton<IUserService, UserService>();
    }
}