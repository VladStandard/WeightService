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
    public static MauiAppBuilder CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();

        builder.SetupLocalizer();
        builder.Services.AddTransient<HostNameMessageHandler>();
        builder.RegisterRefitClients();
        builder.UseMauiApp<App>().UseFullScreen();

        builder.Configuration
            .AddJsonFile($"appsettings.{(ConfigurationUtil.IsDevelop ? "DevelopVS" : "ReleaseVS")}.json");

        builder.Services.AddMauiBlazorWebView();

        builder.Services
            .AddScoped<HtmlRenderer>()
            .AddScoped<IPrintingService, PrintingService>()
            .AddRefitEndpoints<IScalesDesktopAssembly>()
            .AddFluentUIComponents(c => c.ValidateClassNames = false);

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddFluxor(options =>
        {
            options.WithLifetime(StoreLifetime.Singleton);
            options.ScanAssemblies(typeof(IScalesDesktopAssembly).Assembly);
        });

        bool isPrinterMock = builder.Configuration.GetValue<bool?>("MockPrinter") ?? false;
        bool isScalesMock = builder.Configuration.GetValue<bool?>("MockScales") ?? false;

        builder.Services
            .AddServiceOrMock<IScalesService, ScalesService, MockScalesService>(isScalesMock, ServiceLifetime.Singleton)
            .AddServiceOrMock<IPrinterService, PrinterService, MockPrinterService>(isPrinterMock, ServiceLifetime.Singleton);

        return builder;
    }
}