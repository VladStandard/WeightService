using System.Globalization;
using Append.Blazor.Printing;
using MauiPageFullScreen;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Api;
using ScalesDesktop.Source.Shared.Services;
using ScalesDesktop.Source.Shared.Utils;

namespace ScalesDesktop;

public static class MauiProgram
{
    public static MauiAppBuilder CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>().UseFullScreen();
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddFluentUIComponents();

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
        builder.Services.AddSingleton<ArmContext>();
        builder.Services.AddSingleton<LabelContext>();
        builder.Services.AddSingleton<PalletContext>();

#if DEBUG
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "DevelopVS");
#else
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "ReleaseVS");
#endif

        builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true);
        IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Api");

        builder.Services.AddTransient<JsonContentHandler>();
        builder.Services.AddHttpClient<IDesktopApi, DesktopApi>(client =>
            {
                client.BaseAddress = new($"{oidcConfiguration.GetValue<string>("BaseUrl") ?? ""}/api/");
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            })
            .AddHttpMessageHandler<JsonContentHandler>();

        return builder;
    }
}