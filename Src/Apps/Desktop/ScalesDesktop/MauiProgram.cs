using Append.Blazor.Printing;
using Blazor.QrCodeGen;
using MauiPageFullScreen;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ScalesDesktop.Source.Shared.Api;
using ScalesDesktop.Source.Shared.Extensions;
using ScalesDesktop.Source.Shared.Services.Devices;
using Ws.Shared.Web.Extensions;

namespace ScalesDesktop;

public static class MauiProgram
{
    public static MauiAppBuilder CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder.Configuration.LoadAppSettings<IScalesDesktopAssembly>();

        builder
            .UseMauiApp<App>()
            .UseFullScreen()
            .RegisterRefitClients();

        builder.Services.AddMauiBlazorWebView();

        builder.Services
            .SetupMauiLocalizer(builder.Configuration)
            .AddRefitEndpoints<IScalesDesktopAssembly>()
            .AddDelegatingHandlers<IScalesDesktopAssembly>()
            .AddFluentUIComponents(c => c.ValidateClassNames = false);

        builder.Services
            .AddScoped<HtmlRenderer>()
            .AddScoped<IPrintingService, PrintingService>();

        builder.Services.AddTransient(sp => new ModuleCreator(sp.GetService<IJSRuntime>()!));

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