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

    public static void RegisterRefitClients(this MauiAppBuilder builder)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        Type configurationType = typeof(IRefitClient);
        List<IRefitClient> configurations = assembly.GetTypes()
            .Where(t => configurationType.IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance)
            .Cast<IRefitClient>()
            .ToList();

        foreach (IRefitClient config in configurations)
            config.Configure(builder);
    }
}