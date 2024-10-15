using Microsoft.Extensions.Configuration;

namespace ScalesDesktop.Source.Shared.Extensions;

public static class ConfigurationSectionExtensions
{
    public static T GetValueSafe<T>(this IConfigurationSection section, string key)
        => section.GetValue<T>(key) ?? throw new NotImplementedException();

    public static T GetValueOrDefault<T>(this IConfigurationSection section, string key, T defaultValue)
        => section.GetValue<T>(key) ?? defaultValue;
}