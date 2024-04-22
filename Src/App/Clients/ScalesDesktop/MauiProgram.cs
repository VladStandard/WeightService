using System.Globalization;
using Append.Blazor.Printing;
using MauiPageFullScreen;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;
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
        builder.Services.AddFluentUIComponents();
        builder.Services.AddDomainServices();
        builder.Services.AddLabelsServices();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        const string currentLanguage = "ru-RU";
        CultureInfo.DefaultThreadCurrentCulture = new(currentLanguage);
        CultureInfo.DefaultThreadCurrentUICulture = new(currentLanguage);
        builder.Services.AddLocalization();

        builder.Services.AddScoped<IPrintingService, PrintingService>();
        builder.Services.AddSingleton<ExternalDevicesService>();
        builder.Services.AddSingleton<LineContext>();
        builder.Services.AddSingleton<LabelContext>();
        builder.Services.AddSingleton<PalletContext>();

        return builder;
    }
}