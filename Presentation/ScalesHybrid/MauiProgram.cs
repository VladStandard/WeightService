using System.Globalization;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Tailwind;
using MauiPageFullScreen;
using Microsoft.Extensions.Logging;
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
        
        builder.UseMauiApp<App>().UseFullScreen();
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddVsServices();
        
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        
        builder.Services.AddLocalization();
        CultureInfo.DefaultThreadCurrentCulture = new("ru-RU");
        CultureInfo.DefaultThreadCurrentUICulture = new("ru-RU");
        
        builder.Services.AddSingleton<ExternalDevicesService>();
        builder.Services.AddSingleton<LineContext>();
        
        builder.Services
            .AddBlazorise()
            .AddTailwindProviders()
            .AddFontAwesomeIcons();
        
        return builder;
    }
}
