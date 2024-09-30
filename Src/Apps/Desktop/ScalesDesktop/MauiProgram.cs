using Append.Blazor.Printing;
using MauiPageFullScreen;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ScalesDesktop.Source.Shared.Extensions;
using ScalesDesktop.Source.Shared.Refit;
using ScalesDesktop.Source.Shared.Services.Devices;
using Ws.Shared.Extensions;

namespace ScalesDesktop;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();

        builder.SetupLocalizer();
        builder.Services.AddTransient<HostNameMessageHandler>();
        // builder.RegisterRefitClients();

        builder.UseMauiApp<App>();
        builder.Services.AddRefitClient<IDesktopApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new("https://scales-api-dev.kolbasa-vs.local/api/desktop"))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            })
            .AddHttpMessageHandler<HostNameMessageHandler>();

        // builder.Configuration
        //     .AddJsonFile($"appsettings.{(ConfigurationUtil.IsDevelop ? "DevelopVS" : "ReleaseVS")}.json");

        builder.Services.AddMauiBlazorWebView();

        builder.Services
            .AddScoped<HtmlRenderer>()
            .AddScoped<IPrintingService, PrintingService>()
            .AddFluentUIComponents(c => c.ValidateClassNames = false);

        builder.Services.AddScoped<ArmEndpoints>();
        builder.Services.AddScoped<PalletEndpoints>();
        builder.Services.AddScoped<PluEndpoints>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddFluxor(options =>
        {
            options.WithLifetime(StoreLifetime.Singleton);
            options.ScanAssemblies(typeof(IScalesDesktopAssembly).Assembly);
        });

        builder.Services.AddSingleton<IPrinterService, MockPrinterService>();
        builder.Services.AddSingleton<IScalesService, MockScalesService>();

        return builder.Build();
    }
}