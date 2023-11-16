namespace WsLocalizationCore.Models;

public sealed class LocaleConvert : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public string BoolToString(bool isFlag) => isFlag ? Lang == EnumLanguage.English ? "yes" : "да" : Lang == EnumLanguage.English ? "no" : "нет";
    public string ByteToString(byte isFlag, string yes, string no) => isFlag == 0x01 ? yes : no;

    #endregion
}