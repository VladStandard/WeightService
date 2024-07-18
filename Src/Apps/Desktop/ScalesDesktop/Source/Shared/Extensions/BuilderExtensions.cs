using System.Globalization;
using System.Reflection;
using ScalesDesktop.Source.Shared.Refit;

namespace ScalesDesktop.Source.Shared.Extensions;

public static class BuilderExtensions
{
    public static void SetupLocalizer(this MauiAppBuilder builder)
    {
        const string currentLanguage = "ru-RU";
        CultureInfo.DefaultThreadCurrentCulture = new(currentLanguage);
        CultureInfo.DefaultThreadCurrentUICulture = new(currentLanguage);
        builder.Services.AddLocalization();
    }

    public static void ApplyRefitConfigurations(this MauiAppBuilder builder)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        Type configurationType = typeof(IRefitEndpoint);
        List<IRefitEndpoint> configurations = assembly.GetTypes()
            .Where(t => configurationType.IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance)
            .Cast<IRefitEndpoint>()
            .ToList();

        foreach (IRefitEndpoint config in configurations)
            config.Configure(builder);
    }
}