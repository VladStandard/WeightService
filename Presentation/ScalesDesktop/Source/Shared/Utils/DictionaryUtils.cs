namespace ScalesDesktop.Source.Shared.Utils;

public static class DictionaryUtils
{
    public static string TryGetValue(IReadOnlyDictionary<string, object> attributes, string field)
    {
        if (attributes.TryGetValue(field, out object? classObj))
            return classObj.ToString() ?? string.Empty;
        return string.Empty;
    }
}