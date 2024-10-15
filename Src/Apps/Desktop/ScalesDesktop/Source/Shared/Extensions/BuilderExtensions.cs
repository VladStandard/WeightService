using System.Globalization;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Ws.Shared.Handlers;

namespace ScalesDesktop.Source.Shared.Extensions;

public static class BuilderExtensions
{
    public static void SetupLocalizer(this MauiAppBuilder builder)
    {
        IConfigurationSection systemSection = builder.Configuration.GetSection("System");

        CultureInfo defaultCulture = new(systemSection.GetValueOrDefault("Language", "ru-RU"));

        CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
        CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

        builder.Services.AddLocalization();
        builder.Services.AddTransient<AcceptLanguageHandler>();
    }

    public static void LoadSettings(this MauiAppBuilder builder)
    {
        using Stream? appsettingsStream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("ScalesDesktop.appsettings.json");

        if (appsettingsStream == null) return;

        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonStream(appsettingsStream)
            .Build();

        builder.Configuration.AddConfiguration(config);
    }
}