using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;
using ScalesHybrid.Services;
using Ws.Services.Services.Host;
using Ws.Services.Services.Line;
using Ws.StorageCore.Helpers;

namespace ScalesHybrid;
public static class MauiProgram
{
    public static MauiAppBuilder CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        
        SqlCoreHelper.Instance.SetSessionFactory(false);
        if (SqlCoreHelper.Instance.SessionFactory is null)
            throw new ArgumentException($"{nameof(SqlCoreHelper.Instance.SessionFactory)}");
        
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddScoped<IHostService, HostService>();
        builder.Services.AddScoped<ILineService, LineService>();
        
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        CultureInfo[] supportedCultures = { new("en-US"), new("ru-RU") };
        
        builder.Services.AddLocalization();
        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture("ru-RU", "ru-RU");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
        builder.Services.AddSingleton<PageTitleService>();
        
        return builder;
    }
}
