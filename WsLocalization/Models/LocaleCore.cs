// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalization.Models;

public static class LocaleCore
{
    #region Public and private fields, properties, constructor

    private static Lang _lang;
    public static Lang Lang
    {
        get => _lang;
        set => Action.Lang = Buttons.Lang = Convert.Lang = DeviceControl.Lang = Dialog.Lang = Memory.Lang = Menu.Lang = Print.Lang =
            Scales.Lang = Settings.Lang = Sql.Lang = System.Lang = Table.Lang = Validator.Lang = Ping.Lang = WebService.Lang = ContextMenu.Lang = _lang = value;
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
    public static LocalePing Ping { get; } = LocalePing.Instance;
    public static LocaleWebService WebService { get; } = LocaleWebService.Instance;
	public static LocaleContextMenu ContextMenu { get; } = LocaleContextMenu.Instance;

    #endregion

    #region Constructor and destructor

    static LocaleCore()
    {
        Lang = Lang.Russian;
    }

    #endregion

    public static class Strings
    {
        public static string AccessRights => Lang == Lang.English ? "Access rights" : "Права доступа";
        public static string AccessRightsAdmin => Lang == Lang.English ? "Admin rights" : "Админ права";
        public static string AccessRightsNone => Lang == Lang.English ? "No rights" : "Нет прав";
        public static string AccessRightsRead => Lang == Lang.English ? "Read rights" : "Права на чтение";
        public static string AccessRightsWrite => Lang == Lang.English ? "Write rights" : "Права на запись";
        public static string AppDevicesControlName => Lang == Lang.English ? "Devices Control" : "Управление устройствами";
        public static string Application => Lang == Lang.English ? "Application" : "Приложение";
        public static string AppSettings => Lang == Lang.English ? "App settings" : "Настройки приложения";
        public static string Authorization => Lang == Lang.English ? "Authorization" : "Авторизация";
        public static string AuthorizingApAddress => Lang == Lang.English ? "IP address" : "IP-адрес";
        public static string AuthorizingHostName => Lang == Lang.English ? "Host name" : "Имя компьютера";
        public static string AuthorizingId => Lang == Lang.English ? "ID" : "ИД";
        public static string AuthorizingProcess => Lang == Lang.English ? "Authorizing ..." : "Авторизация ...";
        public static string AuthorizingSuccess => Lang == Lang.English ? "Success authorized" : "Успешная авторизация";
        public static string AuthorizingUserName => Lang == Lang.English ? "User name" : "Имя пользователя";
        public static string ButtonHeight => Lang == Lang.English ? "Buttons height" : "Высота кнопок";
        public static string ButtonWidth => Lang == Lang.English ? "Buttons width" : "Ширина кнопок";
        public static string Company => Lang == Lang.English ? "Vladimir Standard" : "Владимирский стандарт";
        public static string DataLoadComplete => Lang == Lang.English ? "Data downloaded successfully." : "Данные загружены успешно.";
        public static string DataLoadError => Lang == Lang.English ? "Error loading data!" : "Ошибка загрузки данных!";
        public static string DataLoading => Lang == Lang.English ? "Loading data..." : "Загрузка данных...";
        public static string DataSizeBytes => Lang == Lang.English ? "Bytes" : "Байт(ов)";
        public static string DataSizeChars => Lang == Lang.English ? "chars" : "символов";
        public static string DataSizeGBytes => Lang == Lang.English ? "Gbytes" : "ГБайт(ов)";
        public static string DataSizeKBytes => Lang == Lang.English ? "Kbytes" : "КБайт(ов)";
        public static string DataSizeLength => Lang == Lang.English ? "Length" : "Длина";
        public static string DataSizeMBytes => Lang == Lang.English ? "Mbytes" : "МБайтов";
        public static string DataSizeVolume => Lang == Lang.English ? "Data volume" : "Объём данных";
        public static string DbAndMemory => Lang == Lang.English ? "DB and memory" : "БД и память";
        public static string DbAndMemoryGauge => Lang == Lang.English ? "DB and memory (gauge)" : "БД и память (измеритель)";
        public static string DbAndMemoryProgressBar => Lang == Lang.English ? "DB and memory (progressbar)" : "БД и память (шкала прогресса)";
        public static string DbInfo => Lang == Lang.English ? "Information about the DB" : "Информация о БД";
        public static string DebugMode => Lang == Lang.English ? "Debug mode" : "Режим отладки";
        public static string Doc => Lang == Lang.English ? "Documentation" : "Документация";
        public static string Feedback => Lang == Lang.English ? "Feedback" : "Обратная связь";
        public static string FillSize => Lang == Lang.English ? "Fill percentage" : "Процент заполнения";
        public static string From => Lang == Lang.English ? "from" : "из";
        public static string HostName => Lang == Lang.English ? "Host name" : "Имя хоста";
        public static string IdentityError => Lang == Lang.English ? "User error!" : "Ошибка пользователя";
        public static string Index => Lang == Lang.English ? "Inside resources" : "Внутренние ресурсы";
        public static string IndexDescription => Lang == Lang.English ? "The site was created to help you navigate through the company's internal resources" : "Сайт создан для помощи в навигации по внутренним ресурсам компании";
        public static string InputControlCheck(int min, int max) => Lang == Lang.English ? $"The value must be beetwen {min} and {max}!" : $"Значение должно быть между {min} и {max}!";
        public static string IsEnableHe(bool isTrue) => Lang == Lang.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включен" : "Отключен";
        public static string IsEnableIt(bool isTrue) => Lang == Lang.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включено" : "Отключено";
        public static string IsEnableShe(bool isTrue) => Lang == Lang.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включена" : "Отключена";
        public static string IsYes(bool isTrue) => Lang == Lang.English ? isTrue ? "Yes" : "No" : isTrue ? "Да" : "Нет";
        public static string ItemAccess => Lang == Lang.English ? "Access" : "Доступ";
        public static string ItemAccessAllow => Lang == Lang.English ? "Item access allowed" : "Доступ к элементу разрешён";
        public static string ItemAccessDeny => Lang == Lang.English ? "Item access denied" : "Доступ к элементу запрещён";
        public static string ItemAccessNone => Lang == Lang.English ? "No access to the item" : "Доступ к разделу не предусмотрен";
        public static string ItemLog => Lang == Lang.English ? "Log" : "Лог";
        public static string ItemsCount => Lang == Lang.English ? "Records" : "Записей";
        public static string Language => Lang == Lang.English ? "Language" : "Язык";
        public static string LanguageDetect => Lang == Lang.English ? "English" : "Русский";
        public static string MethodError => Lang == Lang.English ? "Method error" : "Ошибка метода";
        public static string NotLoad => Lang == Lang.English ? "Not load!" : "Не загружено";
        public static string PageError => Lang == Lang.English ? "Sorry, there's nothing at this address." : "Извините, по этому адресу ничего нет.";
        public static string SectionAccess => Lang == Lang.English ? "Access" : "Доступ";
        public static string SectionAccessAllow => Lang == Lang.English ? "Section access allowed" : "Доступ к разделу разрешён";
        public static string SectionAccessDeny => Lang == Lang.English ? "Section access denied" : "Доступ к разделу запрещён";
        public static string SectionAccessNone => Lang == Lang.English ? "No access to the section" : "Доступ к разделу не предусмотрен";
        public static string SectionLog => Lang == Lang.English ? "Log" : "Лог";
        public static string ServerDevelop => Lang == Lang.English ? "Debug server" : "Сервер разработки";
        public static string ServerRelease => Lang == Lang.English ? "Debug release" : "Промышленный сервер";
        public static string SettingName => Lang == Lang.English ? "Setting" : "Настройка";
        public static string SettingValue => Lang == Lang.English ? "Value" : "Значение";
        public static string UserSettings => Lang == Lang.English ? "User settings" : "Пользовательские настройки";
        public static string VerApp => Lang == Lang.English ? "Application version" : "Версия приложения";
        public static string VerCore => Lang == Lang.English ? "Core version" : "Версия ядра";
        public static string VerLibBlazorAndDataCore => Lang == Lang.English ? "BlazorCore / DataCore lib versions" : "Версия библиотек BlazorCore / DataCore";
        public static string VerLibBlazorCore => Lang == Lang.English ? "BlazorCore lib version" : "Версия библиотеки BlazorCore";
        public static string VerLibDataCore => Lang == Lang.English ? "DataCore lib version" : "Версия библиотеки DataCore";
        public static string VerProgram => Lang == Lang.English ? "Program version" : "Версия программы";
        public static string VersionDb => Lang == Lang.English ? "DB version" : "Версия БД";
        public static string VersionsDb => Lang == Lang.English ? "DB versions" : "Версии БД";
    }
}