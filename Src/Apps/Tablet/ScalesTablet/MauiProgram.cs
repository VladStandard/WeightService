using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesTablet.Source.Shared.Api;
using ScalesTablet.Source.Shared.Extensions;
using BarcodeScanning;
using Fluxor;
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

        #if DEBUG

        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();

        #endif

        return builder.Build();
    }
}