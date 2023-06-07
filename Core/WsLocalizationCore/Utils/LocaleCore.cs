// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalizationCore.Models;

namespace WsLocalizationCore.Utils;

public static class LocaleCore
{
    #region Public and private fields, properties, constructor

    private static WsEnumLanguage _lang;
    public static WsEnumLanguage Lang
    {
        get => _lang;
        set => Action.Lang = Buttons.Lang = Convert.Lang = DeviceControl.Lang = Dialog.Lang = Memory.Lang = Menu.Lang =
            Print.Lang = Scales.Lang = Settings.Lang = Sql.Lang = System.Lang = Table.Lang = Validator.Lang =
            WebService.Lang = ContextMenu.Lang = _lang = value;
    }
    public static LocaleAction Action { get; } = LocaleAction.Instance;
    public static LocaleButtons Buttons { get; } = LocaleButtons.Instance;
    public static LocaleConvert Convert { get; } = LocaleConvert.Instance;
    public static LocaleDeviceControl DeviceControl { get; } = LocaleDeviceControl.Instance;
    public static LocaleDialog Dialog { get; } = LocaleDialog.Instance;
    public static LocaleMemory Memory { get; } = LocaleMemory.Instance;
    public static LocaleMenu Menu { get; } = LocaleMenu.Instance;
    public static LocalePrint Print { get; } = LocalePrint.Instance;
    public static LocaleScale Scales { get; } = LocaleScale.Instance;
    public static LocaleSettings Settings { get; } = LocaleSettings.Instance;
    public static LocaleSql Sql { get; } = LocaleSql.Instance;
    public static LocaleSystem System { get; } = LocaleSystem.Instance;
    public static LocaleTable Table { get; } = LocaleTable.Instance;
    public static LocaleValidator Validator { get; } = LocaleValidator.Instance;
    public static WsLocaleWebService WebService { get; } = WsLocaleWebService.Instance;
    public static LocaleContextMenu ContextMenu { get; } = LocaleContextMenu.Instance;

    #endregion

    #region Constructor and destructor

    static LocaleCore()
    {
        Lang = WsEnumLanguage.Russian;
    }

    #endregion

    public static class Strings
    {
        public static string DefaultRowCount => Lang == WsEnumLanguage.English ? "Default row count" : "Кол-во строк по умолчанию";
        public static string AccessRights => Lang == WsEnumLanguage.English ? "Access rights" : "Права доступа";
        public static string AccessRightsAdmin => Lang == WsEnumLanguage.English ? "Admin rights" : "Админ права";
        public static string AccessRightsNone => Lang == WsEnumLanguage.English ? "No rights" : "Нет прав";
        public static string AccessRightsRead => Lang == WsEnumLanguage.English ? "Read rights" : "Права на чтение";
        public static string AccessRightsWrite => Lang == WsEnumLanguage.English ? "Write rights" : "Права на запись";
        public static string AppDevicesControlName => Lang == WsEnumLanguage.English ? "Devices Control" : "Управление устройствами";
        public static string Application => Lang == WsEnumLanguage.English ? "Application" : "Приложение";
        public static string AppSettings => Lang == WsEnumLanguage.English ? "App settings" : "Настройки приложения";
        public static string Authorization => Lang == WsEnumLanguage.English ? "Authorization" : "Авторизация";
        public static string AuthorizingApAddress => Lang == WsEnumLanguage.English ? "IP address" : "IP-адрес";
        public static string AuthorizingHostName => Lang == WsEnumLanguage.English ? "Host name" : "Имя компьютера";
        public static string AuthorizingId => Lang == WsEnumLanguage.English ? "ID" : "ИД";
        public static string AuthorizingProcess => Lang == WsEnumLanguage.English ? "Authorizing ..." : "Авторизация ...";
        public static string AuthorizingSuccess => Lang == WsEnumLanguage.English ? "Success authorized" : "Успешная авторизация";
        public static string AuthorizingUserName => Lang == WsEnumLanguage.English ? "User name" : "Имя пользователя";
        public static string ButtonHeight => Lang == WsEnumLanguage.English ? "Buttons height" : "Высота кнопок";
        public static string ButtonWidth => Lang == WsEnumLanguage.English ? "Buttons width" : "Ширина кнопок";
        public static string Company => Lang == WsEnumLanguage.English ? "Vladimir Standard" : "Владимирский стандарт";
        public static string DataLoadComplete => Lang == WsEnumLanguage.English ? "Data downloaded successfully." : "Данные загружены успешно.";
        public static string DataLoadError => Lang == WsEnumLanguage.English ? "Error loading data!" : "Ошибка загрузки данных!";
        public static string DataLoading => Lang == WsEnumLanguage.English ? "Loading data..." : "Загрузка данных...";
        public static string DataSizeBytes => Lang == WsEnumLanguage.English ? "Bytes" : "Байт(ов)";
        public static string DataSizeChars => Lang == WsEnumLanguage.English ? "chars" : "символов";
        public static string DataSizeGBytes => Lang == WsEnumLanguage.English ? "Gbytes" : "ГБайт(ов)";
        public static string DataSizeKBytes => Lang == WsEnumLanguage.English ? "Kbytes" : "КБайт(ов)";
        public static string DataSizeLength => Lang == WsEnumLanguage.English ? "Length" : "Длина";
        public static string DataSizeMBytes => Lang == WsEnumLanguage.English ? "Mbytes" : "МБайтов";
        public static string DataSizeVolume => Lang == WsEnumLanguage.English ? "Data volume" : "Объём данных";
        public static string DbAndMemory => Lang == WsEnumLanguage.English ? "DB and memory" : "БД и память";
        public static string DbAndMemoryGauge => Lang == WsEnumLanguage.English ? "RAM" : "Оперативная память";
        public static string DbAndMemoryProgressBar => Lang == WsEnumLanguage.English ? "DB and memory (progressbar)" : "БД и память (шкала прогресса)";
        public static string DbInfo => Lang == WsEnumLanguage.English ? "Information about the DB" : "Информация о БД";
        public static string DebugMode => Lang == WsEnumLanguage.English ? "Debug mode" : "Режим отладки";
        public static string Doc => Lang == WsEnumLanguage.English ? "Documentation" : "Документация";
        public static string Feedback => Lang == WsEnumLanguage.English ? "Feedback" : "Обратная связь";
        public static string FillSize => Lang == WsEnumLanguage.English ? "Fill percentage" : "Процент заполнения";
        public static string From => Lang == WsEnumLanguage.English ? "from" : "из";
        public static string HostName => Lang == WsEnumLanguage.English ? "Host name" : "Имя хоста";
        public static string IdentityError => Lang == WsEnumLanguage.English ? "User error!" : "Ошибка пользователя";
        public static string Index => Lang == WsEnumLanguage.English ? "Inside resources" : "Внутренние ресурсы";
        public static string IndexDescription => Lang == WsEnumLanguage.English ? "The site was created to help you navigate through the company's internal resources" : "Сайт создан для помощи в навигации по внутренним ресурсам компании";
        public static string InputControlCheck(int min, int max) => Lang == WsEnumLanguage.English ? $"The value must be beetwen {min} and {max}!" : $"Значение должно быть между {min} и {max}!";
        public static string IsEnableHe(bool isTrue) => Lang == WsEnumLanguage.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включен" : "Отключен";
        public static string IsEnableIt(bool isTrue) => Lang == WsEnumLanguage.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включено" : "Отключено";
        public static string IsEnableShe(bool isTrue) => Lang == WsEnumLanguage.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включена" : "Отключена";
        public static string IsYes(bool isTrue) => Lang == WsEnumLanguage.English ? isTrue ? "Yes" : "No" : isTrue ? "Да" : "Нет";
        public static string ItemAccess => Lang == WsEnumLanguage.English ? "Access" : "Доступ";
        public static string ItemAccessAllow => Lang == WsEnumLanguage.English ? "Item access allowed" : "Доступ к элементу разрешён";
        public static string ItemAccessDeny => Lang == WsEnumLanguage.English ? "Item access denied" : "Доступ к элементу запрещён";
        public static string ItemAccessNone => Lang == WsEnumLanguage.English ? "No access to the item" : "Доступ к разделу не предусмотрен";
        public static string ItemLog => Lang == WsEnumLanguage.English ? "Log" : "Лог";
        public static string ItemsCount => Lang == WsEnumLanguage.English ? "Records" : "Записей";
        public static string Language => Lang == WsEnumLanguage.English ? "Language" : "Язык";
        public static string LanguageDetect => Lang == WsEnumLanguage.English ? "English" : "Русский";
        public static string MethodError => Lang == WsEnumLanguage.English ? "Method error" : "Ошибка метода";
        public static string NotLoad => Lang == WsEnumLanguage.English ? "Not load!" : "Не загружено";
        public static string PageError => Lang == WsEnumLanguage.English ? "Sorry, there's nothing at this address." : "Извините, по этому адресу ничего нет.";
        public static string SectionAccess => Lang == WsEnumLanguage.English ? "Access" : "Доступ";
        public static string SectionAccessAllow => Lang == WsEnumLanguage.English ? "Section access allowed" : "Доступ к разделу разрешён";
        public static string SectionAccessDeny => Lang == WsEnumLanguage.English ? "Section access denied" : "Доступ к разделу запрещён";
        public static string SectionAccessNone => Lang == WsEnumLanguage.English ? "No access to the section" : "Доступ к разделу не предусмотрен";
        public static string SectionLog => Lang == WsEnumLanguage.English ? "Log" : "Лог";
        public static string ServerDevelop => Lang == WsEnumLanguage.English ? "Debug server" : "Сервер разработки";
        public static string ServerRelease => Lang == WsEnumLanguage.English ? "Debug release" : "Промышленный сервер";
        public static string SettingName => Lang == WsEnumLanguage.English ? "Setting" : "Настройка";
        public static string SettingValue => Lang == WsEnumLanguage.English ? "Value" : "Значение";
        public static string UserSettings => Lang == WsEnumLanguage.English ? "User settings" : "Пользовательские настройки";
        public static string VerApp => Lang == WsEnumLanguage.English ? "Application version" : "Версия приложения";
        public static string VerCore => Lang == WsEnumLanguage.English ? "Core version" : "Версия ядра";
        public static string VerLibBlazorAndDataCore => Lang == WsEnumLanguage.English ? "BlazorCore / DataCore lib versions" : "Версия библиотек BlazorCore / DataCore";
        public static string VerLibBlazorCore => Lang == WsEnumLanguage.English ? "BlazorCore lib version" : "Версия библиотеки BlazorCore";
        public static string VerLibDataCore => Lang == WsEnumLanguage.English ? "DataCore lib version" : "Версия библиотеки DataCore";
        public static string VerProgram => Lang == WsEnumLanguage.English ? "Program version" : "Версия программы";
        public static string VersionDb => Lang == WsEnumLanguage.English ? "DB version" : "Версия БД";
        public static string VersionsDb => Lang == WsEnumLanguage.English ? "DB versions" : "Версии БД";
    }
}