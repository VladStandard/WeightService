using System.Globalization;
using Append.Blazor.Printing;
using MauiPageFullScreen;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using Refit;
using ScalesDesktop.Source.Shared.Services;
using Ws.Desktop.Models;

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
        builder.Services.AddSingleton<LabelContext>();
        builder.Services.AddSingleton<PalletContext>();

        builder.Services.AddScoped<PalletApi>();
        builder.Services.AddScoped<ArmApi>();
        builder.Services.AddScoped<PluApi>();

#if DEBUG
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "DevelopVS");
#else
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "ReleaseVS");
#endif

        builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true);
        IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Api");

        builder.Services.AddRefitClient<IDesktopApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{oidcConfiguration.GetValue<string>("BaseUrl") ?? ""}"))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            });

        return builder;
    }
}