using System.Text;

namespace Ws.Shared.Utils;

public static class TranslitUtil
{
    private static readonly Dictionary<char, string> TranslitMap = new() {
        {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"}, {'д', "d"}, {'е', "e"}, {'ё', "e"},
        {'ж', "zh"}, {'з', "z"}, {'и', "i"}, {'й', "i"}, {'к', "k"}, {'л', "l"}, {'м', "m"},
        {'н', "n"}, {'о', "o"}, {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"}, {'у', "u"},
        {'ф', "f"}, {'х', "kh"}, {'ц', "ts"}, {'ч', "ch"}, {'ш', "sh"}, {'щ', "shch"}, {'ы', "y"},
        {'ъ', "ie"}, {'э', "e"}, {'ю', "iu" }, {'я', "ia"}, {' ', " "}
    };

    public static string Transliterate(string input)
    {
        StringBuilder result = new();
        foreach (char c in input)
        {
            string b = TranslitMap.GetValueOrDefault(char.ToLower(c), " ");
            result.Append(char.IsUpper(c) ? b.ToUpper() : b);
        }
        return result.ToString();
    }
}