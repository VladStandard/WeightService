// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations;

public static partial class LocaleData
{
    public static LangEnum Lang { get; set; }

    static LocaleData()
    {
        Lang = LangEnum.Russian;
    }
}
