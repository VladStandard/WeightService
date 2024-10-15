using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesTablet.Source.Shared.Api;
using ScalesTablet.Source.Shared.Extensions;
using Ws.Shared.Utils;

namespace ScalesTablet;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();

        builder.Configuration
            .AddJsonFile($"appsettings.{(ConfigurationUtils.IsDevelop ? "DevelopVS" : "ReleaseVS")}.json");

        builder.UseMauiApp<App>();

        builder.SetupLocalizer();
        builder.RegisterRefitClients();
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddFluentUIComponents(c => c.ValidateClassNames = false);

        #if DEBUG

        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();

        #endif

        return builder.Build();
    }
}