using System.Globalization;
using System.Reflection;
using ScalesDesktop.Source.Shared.Refit;
using Ws.Shared.Constants;

namespace ScalesDesktop.Source.Shared.Extensions;

public static class BuilderExtensions
{
    public static void SetupLocalizer(this MauiAppBuilder builder)
    {
        CultureInfo.DefaultThreadCurrentCulture = Cultures.Ru;
        CultureInfo.DefaultThreadCurrentUICulture = Cultures.Ru;
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