namespace Ws.Shared.Extensions;

public static class StringExtensions
{
    private static readonly Dictionary<char, string?> TranslitMap = new() {
        {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"}, {'д', "d"}, {'е', "e"}, {'ё', "e"},
        {'ж', "zh"}, {'з', "z"}, {'и', "i"}, {'й', "i"}, {'к', "k"}, {'л', "l"}, {'м', "m"},
        {'н', "n"}, {'о', "o"}, {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"}, {'у', "u"},
        {'ф', "f"}, {'х', "kh"}, {'ц', "ts"}, {'ч', "ch"}, {'ш', "sh"}, {'щ', "shch"}, {'ы', "y"},
        {'ъ', "ie"}, {'э', "e"}, {'ю', "iu" }, {'я', "ia"}, {' ', " "}
    };

    [Pure]
    public static bool IsDigitsOnly(this string str)
        => !string.IsNullOrWhiteSpace(str) && str.All(char.IsDigit);

    [Pure]
    public static string Capitalize(this string str)
        => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);

    [Pure]
    public static string Transliterate(this string str)
    {
        StringBuilder result = new();
        foreach (char c in str)
        {
            TranslitMap.TryGetValue(char.ToLower(c), out string? translitChar);
            if (translitChar is null) throw new ArgumentException("Invalid transliterate character");
            result.Append(char.IsUpper(c) ? translitChar.ToUpper() : translitChar);
        }
        return result.ToString();
    }
}