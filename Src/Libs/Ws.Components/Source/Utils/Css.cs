namespace Ws.Components.Source.Utils;

public static class Css
{
    public static string Class(params string?[] classes) =>
        string.Join(" ", classes).Trim();

    public static string Class(params (string?, bool)[] classes) =>
        string.Join(" ", classes.Where(x => x.Item2).Select(x => x.Item1)).Trim();

    public static string Class(string? always, params (string?, bool)[] classes) =>
        string.Join(" ", classes.Where(x => x.Item2).Select(x => x.Item1).Prepend(always)).Trim();
}