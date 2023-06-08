// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

public sealed class WsLocaleConvert : WsLocaleBase
{
    #region Public and private fields, properties, constructor

    public string BoolToString(bool isFlag) => isFlag ? Lang == WsEnumLanguage.English ? "yes" : "да" : Lang == WsEnumLanguage.English ? "no" : "нет";
    public string ByteToString(byte isFlag, string yes, string no) => isFlag == 0x01 ? yes : no;

    #endregion
}