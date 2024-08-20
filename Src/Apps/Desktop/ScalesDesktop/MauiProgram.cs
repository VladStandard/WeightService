using Append.Blazor.Printing;
using Fluxor;
using MauiPageFullScreen;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ScalesDesktop.Source.Shared.Extensions;
using ScalesDesktop.Source.Shared.Services.Devices;
using Ws.Shared.Utils;

namespace ScalesDesktop;

public static class MauiProgram
{
    public static MauiAppBuilder CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();

        builder.Configuration
            .AddJsonFile($"appsettings.{(ConfigurationUtil.IsDevelop ?  "DevelopVS" : "ReleaseVS")}.json");

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddFluentUIComponents(c => c.ValidateClassNames = false);

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.UseMauiApp<App>().UseFullScreen();
        builder.SetupLocalizer();
        builder.ApplyRefitConfigurations();

        builder.Services.AddScoped<IPrintingService, PrintingService>();
        builder.Services.AddFluxor(options =>
        {
            options.WithLifetime(StoreLifetime.Singleton);
            options.ScanAssemblies(typeof(MauiProgram).Assembly);
        });

        builder.Services.AddSingleton<PalletDocumentGenerator>();

        if (builder.Configuration.GetValue<bool?>("MockPrinter") == true && ConfigurationUtil.IsDevelop)
            builder.Services.AddSingleton<IPrinterService, MockPrinterService>();
        else
            builder.Services.AddSingleton<IPrinterService, PrinterService>();

        if (builder.Configuration.GetValue<bool?>("MockScales") == true && ConfigurationUtil.IsDevelop)
            builder.Services.AddSingleton<IScalesService, MockScalesService>();
        else
            builder.Services.AddSingleton<IScalesService ,ScalesService>();

        return builder;
    }
}