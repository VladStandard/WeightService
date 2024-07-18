using Append.Blazor.Printing;
using MauiPageFullScreen;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ScalesDesktop.Source.Shared.Extensions;
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

        builder.Services.AddSingleton<PalletDocumentGenerator>();

        builder.Services.AddSingleton<ScalesService>();
        builder.Services.AddSingleton<PrinterService>();

        builder.Services.AddSingleton<LabelContext>();
        builder.Services.AddSingleton<PalletContext>();

        return builder;
    }
}