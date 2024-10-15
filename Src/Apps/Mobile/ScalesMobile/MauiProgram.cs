using System.Globalization;
using BarcodeScanning;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;

namespace ScalesMobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>();
        builder.UseBarcodeScanning();
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddFluentUIComponents(c => c.ValidateClassNames = false);

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        const string currentLanguage = "ru-RU";
        CultureInfo.DefaultThreadCurrentCulture = new(currentLanguage);
        CultureInfo.DefaultThreadCurrentUICulture = new(currentLanguage);
        builder.Services.AddLocalization();

        return builder.Build();
    }
}