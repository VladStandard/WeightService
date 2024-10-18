using BarcodeScanning;
using Fluxor;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesTablet.Source.Shared.Api;
using ScalesTablet.Source.Shared.Extensions;
using Microsoft.AspNetCore.Components.Web;
using ScalesTablet.Source.Shared;
using ScalesTablet.Source.Shared.Services;
using Ws.Shared.Extensions;

namespace ScalesTablet;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>();

        builder
            .LoadSettings()
            .SetupLocalizer()
            .UseBarcodeScanning()
            .RegisterRefitClients();

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddFluentUIComponents(c => c.ValidateClassNames = false);

        builder.Services
            .AddRefitEndpoints<IScalesTabletAssembly>()
            .AddDelegatingHandlers<IScalesTabletAssembly>();

        builder.Services.AddFluxor(options =>
        {
            options.WithLifetime(StoreLifetime.Singleton);
            options.ScanAssemblies(typeof(IScalesTabletAssembly).Assembly);
        });

        builder.Services
            .AddScoped<HtmlRenderer>()
            .AddScoped<IPrintService, PrintService>();

        builder.Services.AddSingleton<IPrinterService, PrinterService>();

        #if DEBUG

        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();

        #endif

        return builder.Build();
    }
}