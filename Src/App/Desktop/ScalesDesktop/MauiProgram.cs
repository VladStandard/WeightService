using System.Globalization;
using System.Text.Json;
using Append.Blazor.Printing;
using MauiPageFullScreen;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Api;
using ScalesDesktop.Source.Shared.Services;
using ScalesDesktop.Source.Shared.Utils;
using TscZebra.Plugin.Abstractions.Enums;
using Ws.Domain.Services;
using Ws.Labels.Service;
using Ws.Shared.Converters.Json;

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
        builder.Services.AddSingleton<ScalesService>();
        builder.Services.AddSingleton<PrinterService>();
        builder.Services.AddSingleton<LineContext>();
        builder.Services.AddSingleton<LabelContext>();
        builder.Services.AddSingleton<PalletContext>();

        builder.Services.AddTransient<JsonContentHandler>();
        builder.Services.AddHttpClient<IDesktopApi, DesktopApi>(client =>
            client.BaseAddress = new("https://localhost:7173/api/"))
            .AddHttpMessageHandler<JsonContentHandler>();

        return builder;
    }
}