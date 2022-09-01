// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Localizations;

public static partial class LocaleData
{
    public static ShareEnums.Lang Lang { get; set; }

    static LocaleData()
    {
        Lang = ShareEnums.Lang.Russian;
    }
}
