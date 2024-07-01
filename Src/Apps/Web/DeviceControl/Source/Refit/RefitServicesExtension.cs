using System.Reflection;

namespace DeviceControl.Source.Refit;

internal static class RefitServicesExtension
{
    public static void ApplyRefitConfigurations(this WebApplicationBuilder builder)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        Type configurationType = typeof(IRefitEndpoint);
        List<IRefitEndpoint> configurations = assembly.GetTypes()
            .Where(t => configurationType.IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance)
            .Cast<IRefitEndpoint>()
            .ToList();

        foreach (var config in configurations)
            config.Configure(builder);
    }
}