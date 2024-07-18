using System.Globalization;
using Append.Blazor.Printing;
using MauiPageFullScreen;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ws.Desktop.Models;

namespace ScalesDesktop;

public static class MauiProgram
{
    public static MauiAppBuilder CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();

#if DEBUG
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "DevelopVS");
#else
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "ReleaseVS");
#endif

        builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true);

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddFluentUIComponents(c => c.ValidateClassNames = false);

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.UseMauiApp<App>().UseFullScreen();

        const string currentLanguage = "ru-RU";
        CultureInfo.DefaultThreadCurrentCulture = new(currentLanguage);
        CultureInfo.DefaultThreadCurrentUICulture = new(currentLanguage);
        builder.Services.AddLocalization();

        builder.Services.AddScoped<IPrintingService, PrintingService>();
        builder.Services.AddSingleton<PalletDocumentGenerator>();

        builder.Services.AddSingleton<ScalesService>();
        builder.Services.AddSingleton<PrinterService>();

        builder.Services.AddSingleton<LabelContext>();
        builder.Services.AddSingleton<PalletContext>();

        IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Api");
        builder.Services.AddRefitClient<IDesktopApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{oidcConfiguration.GetValue<string>("BaseUrl") ?? ""}"))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            });

        builder.Services.AddScoped<PalletApi>();
        builder.Services.AddScoped<ArmApi>();
        builder.Services.AddScoped<PluApi>();

        return builder;
    }
}