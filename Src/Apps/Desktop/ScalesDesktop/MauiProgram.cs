using Append.Blazor.Printing;
using MauiPageFullScreen;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ScalesDesktop.Source.Shared.Api;
using ScalesDesktop.Source.Shared.Extensions;
using ScalesDesktop.Source.Shared.Services.Devices;
using Ws.Shared.Extensions;

namespace ScalesDesktop;

public static class MauiProgram
{
    public static MauiAppBuilder CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();

        builder.Configuration
            .AddJsonFile($"appsettings.{(ConfigurationUtils.IsDevelop ? "DevelopVS" : "ReleaseVS")}.json");

        builder.SetupLocalizer();
        builder.RegisterRefitClients();
        builder.UseMauiApp<App>().UseFullScreen();

        builder.Services.AddMauiBlazorWebView();

        builder.Services
            .AddRefitEndpoints<IScalesDesktopAssembly>()
            .AddDelegatingHandlers<IScalesDesktopAssembly>()
            .AddFluentUIComponents(c => c.ValidateClassNames = false);

        builder.Services
            .AddScoped<HtmlRenderer>()
            .AddScoped<IPrintingService, PrintingService>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddFluxor(options =>
        {
            options.WithLifetime(StoreLifetime.Singleton);
            options.ScanAssemblies(typeof(IScalesDesktopAssembly).Assembly);
        });

        IConfigurationSection systemSection = builder.Configuration.GetSection("System");
        bool isScalesMock = systemSection.GetValueOrDefault("MockScales", false);
        bool isPrinterMock = systemSection.GetValueOrDefault("MockPrinter", false);

        builder.Services
            .AddServiceOrMock<IScalesService, ScalesService, MockScalesService>(isScalesMock, ServiceLifetime.Singleton)
            .AddServiceOrMock<IPrinterService, PrinterService, MockPrinterService>(isPrinterMock, ServiceLifetime.Singleton);

        return builder;
    }
}