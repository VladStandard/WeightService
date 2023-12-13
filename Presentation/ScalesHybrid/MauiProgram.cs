using System.Globalization;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Tailwind;
using Microsoft.AspNetCore.Builder;
using ScalesHybrid.Services;
using Ws.Services;
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

        builder.UseMauiApp<App>();
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddVsServices();
        
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        CultureInfo[] supportedCultures = { new("en-US"), new("ru-RU") };
        
        builder.Services.AddLocalization();
        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new("ru-RU", "ru-RU");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
        builder.Services.AddSingleton<ExternalDevicesService>();
        builder.Services.AddSingleton<LineContext>();
        
        builder.Services
            .AddBlazorise()
            .AddTailwindProviders()
            .AddFontAwesomeIcons();
        
        return builder;
    }
}
