using System.Globalization;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Tailwind;
using MauiPageFullScreen;
using Microsoft.Extensions.Logging;
using ScalesDesktop.Services;
using Ws.Domain.Services;
using Ws.Labels.Service;

namespace ScalesDesktop;

public static class MauiProgram
{
    public static MauiAppBuilder CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>().UseFullScreen();
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddDomainServices();
        builder.Services.AddLabelsServices();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddLocalization();
        CultureInfo.DefaultThreadCurrentCulture = new("ru-RU");
        CultureInfo.DefaultThreadCurrentUICulture = new("ru-RU");

        builder.Services.AddSingleton<ExternalDevicesService>();
        builder.Services.AddSingleton<LineContext>();
        builder.Services.AddSingleton<LabelContext>();
        builder.Services.AddSingleton<PalletContext>();

        builder.Services
            .AddBlazorise()
            .AddTailwindProviders()
            .AddFontAwesomeIcons();

        return builder;
    }
}