using BarcodeScanning;
using Fluxor;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesMobile.Source.Shared.Api;
using ScalesMobile.Source.Shared.Extensions;
using Ws.Shared.Extensions;

namespace ScalesMobile;

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
            .AddRefitEndpoints<IScalesMobileAssembly>()
            .AddDelegatingHandlers<IScalesMobileAssembly>();

        builder.Services.AddFluxor(options =>
        {
            options.WithLifetime(StoreLifetime.Singleton);
            options.ScanAssemblies(typeof(IScalesMobileAssembly).Assembly);
        });

        #if DEBUG

        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();

        #endif

        return builder.Build();
    }
}