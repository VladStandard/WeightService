using System.Globalization;
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
}