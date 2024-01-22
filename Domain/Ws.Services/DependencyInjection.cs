using Microsoft.Extensions.DependencyInjection;
using Ws.Database.Core.Helpers;
using Ws.Services.Features.Box;
using Ws.Services.Features.Brand;
using Ws.Services.Features.Bundle;
using Ws.Services.Features.Claim;
using Ws.Services.Features.Clip;
using Ws.Services.Features.DatabaseFile;
using Ws.Services.Features.Label;
using Ws.Services.Features.Line;
using Ws.Services.Features.LogWeb;
using Ws.Services.Features.Plu;
using Ws.Services.Features.Printer;
using Ws.Services.Features.ProductionSite;
using Ws.Services.Features.StorageMethod;
using Ws.Services.Features.Template;
using Ws.Services.Features.TemplateResource;
using Ws.Services.Features.User;
using Ws.Services.Features.Warehouse;

namespace Ws.Services;

public static class DependencyInjection
{
    public static void AddVsServices(this IServiceCollection services)
    {
        SqlCoreHelper.Instance.SetSessionFactory();
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
        services.AddScoped<ITemplateResourceService, TemplateResourceService>();
        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddSingleton<IUserService, UserService>();
    }
}