using System.Globalization;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Ws.Shared.Constants;
using Ws.Shared.Handlers;

namespace ScalesMobile.Source.Shared.Extensions;

public static class BuilderExtensions
{
    public static MauiAppBuilder SetupLocalizer(this MauiAppBuilder builder)
    {
        IConfigurationSection systemSection = builder.Configuration.GetSection("System");

        CultureInfo defaultCulture = new(systemSection.GetValueOrDefault("Language", Cultures.Ru.Name));

        CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
        CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

        builder.Services.AddLocalization();
        builder.Services.AddTransient<AcceptLanguageHandler>();

        return builder;
    }

    public static MauiAppBuilder LoadSettings(this MauiAppBuilder builder)
    {
        using Stream? appsettingsStream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("ScalesMobile.appsettings.json");

        if (appsettingsStream == null) return builder;

        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonStream(appsettingsStream)
            .Build();

        builder.Configuration.AddConfiguration(config);

        return builder;
    }
}