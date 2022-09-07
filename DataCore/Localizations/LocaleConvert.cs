// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations;

public class LocaleConvert
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleConvert _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleConvert Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public LangEnum Lang { get; set; } = LangEnum.Russian;

    #region Public and private fields, properties, constructor

    public string BoolToString(bool isFlag) => isFlag ? (Lang == LangEnum.English ? "yes" : "да") : (Lang == LangEnum.English ? "no" : "нет");
    public string ByteToString(byte isFlag, string yes, string no) => isFlag == 0x01 ? yes : no;

    #endregion
}
