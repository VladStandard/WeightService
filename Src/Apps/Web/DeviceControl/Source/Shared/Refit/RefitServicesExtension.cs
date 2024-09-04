using System.Reflection;

namespace DeviceControl.Source.Shared.Refit;

internal static class RefitServicesExtension
{
    public static void RegisterRefitClients(this WebApplicationBuilder builder)
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