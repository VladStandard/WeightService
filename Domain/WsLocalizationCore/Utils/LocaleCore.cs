namespace WsLocalizationCore.Utils;

[DebuggerDisplay("{ToString()}")]
public static class LocaleCore
{
    #region Public and private fields, properties, constructor

    private static EnumLanguage _lang;
    public static EnumLanguage Lang
    {
        get => _lang;
        set
        {
            _lang = value;
            Action.Lang = _lang;
            Buttons.Lang = _lang;
            ContextMenu.Lang = _lang;
            Convert.Lang = _lang;
            DeviceControl.Lang = _lang;
            Dialog.Lang = _lang;
            LabelPrint.Lang = _lang;
            Memory.Lang = _lang;
            Print.Lang = _lang;
            Settings.Lang = _lang;
            Sql.Lang = _lang;
            System.Lang = _lang;
            Table.Lang = _lang;
            Tests.Lang = _lang;
            Validator.Lang = _lang;
            WebService.Lang = _lang;
        }
    }

    private static LocaleConvert Convert { get; } = new();
    private static LocaleSettings Settings { get; } = new();
    public static LocaleAction Action { get; } = new();
    public static LocaleButtons Buttons { get; } = new();
    public static LocaleContextMenu ContextMenu { get; } = new();
    public static LocaleDeviceControl DeviceControl { get; } = new();
    public static LocaleDialog Dialog { get; } = new();
    public static LocaleMemory Memory { get; } = new();
    public static LocaleMenu Menu { get; } = new();
    public static LocalePrint Print { get; } = new();
    public static LocaleSql Sql { get; } = new();
    public static LocaleSystem System { get; } = new();
    public static LocaleTable Table { get; } = new();
    public static LocaleValidator Validator { get; } = new();
    public static LocaleWebService WebService { get; } = new();
    public static LocalizationLabelPrint LabelPrint { get; } = new();
    public static LocalizationTests Tests { get; } = new();

    static LocaleCore()
    {
        Lang = EnumLanguage.Russian;
    }

    #endregion

    public static class Strings
    {
        public static string DefaultRowCount => Lang == EnumLanguage.English ? "Default row count" : "Кол-во строк по умолчанию";
        public static string AccessRights => Lang == EnumLanguage.English ? "Access rights" : "Права доступа";
        public static string AccessRightsAdmin => Lang == EnumLanguage.English ? "Admin rights" : "Админ права";
        public static string AccessRightsNone => Lang == EnumLanguage.English ? "No rights" : "Нет прав";
        public static string AccessRightsRead => Lang == EnumLanguage.English ? "Read rights" : "Права на чтение";
        public static string AccessRightsWrite => Lang == EnumLanguage.English ? "Write rights" : "Права на запись";
        public static string AppDevicesControlName => Lang == EnumLanguage.English ? "Devices Control" : "Управление устройствами";
        public static string Application => Lang == EnumLanguage.English ? "Application" : "Приложение";
        public static string AuthorizingApAddress => Lang == EnumLanguage.English ? "IP address" : "IP-адрес";
        public static string AuthorizingUserName => Lang == EnumLanguage.English ? "User name" : "Имя пользователя";
        public static string DataSizeBytes => Lang == EnumLanguage.English ? "Bytes" : "Байт(ов)";
        public static string DataSizeKBytes => Lang == EnumLanguage.English ? "Kbytes" : "КБайт(ов)";
        public static string DataSizeMBytes => Lang == EnumLanguage.English ? "Mbytes" : "МБайтов";
        public static string DataSizeVolume => Lang == EnumLanguage.English ? "Data volume" : "Объём данных";
        public static string From => Lang == EnumLanguage.English ? "from" : "из";
        public static string ItemsCount => Lang == EnumLanguage.English ? "Records" : "Записей";
        public static string Language => Lang == EnumLanguage.English ? "Language" : "Язык";
        public static string PageError => Lang == EnumLanguage.English ? "Sorry, there's nothing at this address." : "Извините, по этому адресу ничего нет.";
        public static string SettingName => Lang == EnumLanguage.English ? "Setting" : "Настройка";
        public static string SettingValue => Lang == EnumLanguage.English ? "Value" : "Значение";
        public static string VersionsDb => Lang == EnumLanguage.English ? "DB versions" : "Версии БД";
        public static string Clips => Lang == EnumLanguage.English ? "SectionClips" : "Клипсы";

    }
}