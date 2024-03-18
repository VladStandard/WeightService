using Microsoft.Extensions.DependencyInjection;
using Ws.Database.Nhibernate;
using Ws.Domain.Services.Features.Box;
using Ws.Domain.Services.Features.Brand;
using Ws.Domain.Services.Features.Bundle;
using Ws.Domain.Services.Features.Claim;
using Ws.Domain.Services.Features.Clip;
using Ws.Domain.Services.Features.DatabaseFile;
using Ws.Domain.Services.Features.Label;
using Ws.Domain.Services.Features.Line;
using Ws.Domain.Services.Features.LogWeb;
using Ws.Domain.Services.Features.Pallet;
using Ws.Domain.Services.Features.PalletMan;
using Ws.Domain.Services.Features.Plu;
using Ws.Domain.Services.Features.Printer;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Domain.Services.Features.StorageMethod;
using Ws.Domain.Services.Features.Template;
using Ws.Domain.Services.Features.User;
using Ws.Domain.Services.Features.Warehouse;
using Ws.Domain.Services.Features.ZplResource;
using Ws.Domain.Services.Redis;

namespace Ws.Domain.Services;

public static class DependencyInjection
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddNhibernate();

        services.AddEasyCaching(option => {
            option.WithProtobuf();
            option.UseRedis(RedisUtils.LoadRedisCfg(), "ws-redis");
        });

        services.AddScoped<IBoxService, BoxService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IBundleService, BundleService>();
        services.AddScoped<IClaimService, ClaimService>();
        services.AddScoped<IClipService, ClipService>();
        services.AddScoped<IDatabaseFileService, DatabaseFileService>();
        services.AddScoped<ILabelService, LabelService>();
        services.AddScoped<ILineService, LineService>();
        services.AddScoped<ILogWebService, LogWebService>();
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