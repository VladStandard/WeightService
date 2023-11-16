using WsLocalizationCore.Common;
using WsLocalizationCore.DeviceControlModels;
using WsLocalizationCore.Models;
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
        public static string AppSettings => Lang == EnumLanguage.English ? "App settings" : "Настройки приложения";
        public static string Authorization => Lang == EnumLanguage.English ? "Authorization" : "Авторизация";
        public static string AuthorizingApAddress => Lang == EnumLanguage.English ? "IP address" : "IP-адрес";
        public static string AuthorizingHostName => Lang == EnumLanguage.English ? "Host name" : "Имя компьютера";
        public static string AuthorizingId => Lang == EnumLanguage.English ? "ID" : "ИД";
        public static string AuthorizingProcess => Lang == EnumLanguage.English ? "Authorizing ..." : "Авторизация ...";
        public static string AuthorizingSuccess => Lang == EnumLanguage.English ? "Success authorized" : "Успешная авторизация";
        public static string AuthorizingUserName => Lang == EnumLanguage.English ? "User name" : "Имя пользователя";
        public static string ButtonHeight => Lang == EnumLanguage.English ? "Buttons height" : "Высота кнопок";
        public static string ButtonWidth => Lang == EnumLanguage.English ? "Buttons width" : "Ширина кнопок";
        public static string Company => Lang == EnumLanguage.English ? "Vladimir Standard" : "Владимирский стандарт";
        public static string DataLoadComplete => Lang == EnumLanguage.English ? "Data downloaded successfully." : "Данные загружены успешно.";
        public static string DataLoadError => Lang == EnumLanguage.English ? "Error loading data!" : "Ошибка загрузки данных!";
        public static string DataLoading => Lang == EnumLanguage.English ? "Loading data..." : "Загрузка данных...";
        public static string DataSizeBytes => Lang == EnumLanguage.English ? "Bytes" : "Байт(ов)";
        public static string DataSizeChars => Lang == EnumLanguage.English ? "chars" : "символов";
        public static string DataSizeGBytes => Lang == EnumLanguage.English ? "Gbytes" : "ГБайт(ов)";
        public static string DataSizeKBytes => Lang == EnumLanguage.English ? "Kbytes" : "КБайт(ов)";
        public static string DataSizeLength => Lang == EnumLanguage.English ? "Length" : "Длина";
        public static string DataSizeMBytes => Lang == EnumLanguage.English ? "Mbytes" : "МБайтов";
        public static string DataSizeVolume => Lang == EnumLanguage.English ? "Data volume" : "Объём данных";
        public static string DbAndMemory => Lang == EnumLanguage.English ? "DB and memory" : "БД и память";
        public static string DbAndMemoryGauge => Lang == EnumLanguage.English ? "RAM" : "Оперативная память";
        public static string DbAndMemoryProgressBar => Lang == EnumLanguage.English ? "DB and memory (progressbar)" : "БД и память (шкала прогресса)";
        public static string DbInfo => Lang == EnumLanguage.English ? "Information about the DB" : "Информация о БД";
        public static string DebugMode => Lang == EnumLanguage.English ? "Debug mode" : "Режим отладки";
        public static string Doc => Lang == EnumLanguage.English ? "Documentation" : "Документация";
        public static string Feedback => Lang == EnumLanguage.English ? "Feedback" : "Обратная связь";
        public static string FillSize => Lang == EnumLanguage.English ? "Fill percentage" : "Процент заполнения";
        public static string From => Lang == EnumLanguage.English ? "from" : "из";
        public static string HostName => Lang == EnumLanguage.English ? "Host name" : "Имя хоста";
        public static string IdentityError => Lang == EnumLanguage.English ? "User error!" : "Ошибка пользователя";
        public static string Index => Lang == EnumLanguage.English ? "Inside resources" : "Внутренние ресурсы";
        public static string IndexDescription => Lang == EnumLanguage.English ? "The site was created to help you navigate through the company's internal resources" : "Сайт создан для помощи в навигации по внутренним ресурсам компании";
        public static string InputControlCheck(int min, int max) => Lang == EnumLanguage.English ? $"The value must be beetwen {min} and {max}!" : $"Значение должно быть между {min} и {max}!";
        public static string IsEnableHe(bool isTrue) => Lang == EnumLanguage.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включен" : "Отключен";
        public static string IsEnableIt(bool isTrue) => Lang == EnumLanguage.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включено" : "Отключено";
        public static string IsEnableShe(bool isTrue) => Lang == EnumLanguage.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включена" : "Отключена";
        public static string IsYes(bool isTrue) => Lang == EnumLanguage.English ? isTrue ? "Yes" : "No" : isTrue ? "Да" : "Нет";
        public static string ItemAccess => Lang == EnumLanguage.English ? "Access" : "Доступ";
        public static string ItemAccessAllow => Lang == EnumLanguage.English ? "Item access allowed" : "Доступ к элементу разрешён";
        public static string ItemAccessDeny => Lang == EnumLanguage.English ? "Item access denied" : "Доступ к элементу запрещён";
        public static string ItemAccessNone => Lang == EnumLanguage.English ? "No access to the item" : "Доступ к разделу не предусмотрен";
        public static string ItemLog => Lang == EnumLanguage.English ? "Log" : "Лог";
        public static string ItemsCount => Lang == EnumLanguage.English ? "Records" : "Записей";
        public static string Language => Lang == EnumLanguage.English ? "Language" : "Язык";
        public static string LanguageDetect => Lang == EnumLanguage.English ? "English" : "Русский";
        public static string MethodError => Lang == EnumLanguage.English ? "Method error" : "Ошибка метода";
        public static string NotLoad => Lang == EnumLanguage.English ? "Not load!" : "Не загружено";
        public static string PageError => Lang == EnumLanguage.English ? "Sorry, there's nothing at this address." : "Извините, по этому адресу ничего нет.";
        public static string SectionAccess => Lang == EnumLanguage.English ? "Access" : "Доступ";
        public static string SectionAccessAllow => Lang == EnumLanguage.English ? "Section access allowed" : "Доступ к разделу разрешён";
        public static string SectionAccessDeny => Lang == EnumLanguage.English ? "Section access denied" : "Доступ к разделу запрещён";
        public static string SectionAccessNone => Lang == EnumLanguage.English ? "No access to the section" : "Доступ к разделу не предусмотрен";
        public static string SectionLog => Lang == EnumLanguage.English ? "Log" : "Лог";
        public static string ServerDevelop => Lang == EnumLanguage.English ? "Debug server" : "Сервер разработки";
        public static string ServerRelease => Lang == EnumLanguage.English ? "Debug release" : "Промышленный сервер";
        public static string SettingName => Lang == EnumLanguage.English ? "Setting" : "Настройка";
        public static string SettingValue => Lang == EnumLanguage.English ? "Value" : "Значение";
        public static string UserSettings => Lang == EnumLanguage.English ? "User settings" : "Пользовательские настройки";
        public static string VerApp => Lang == EnumLanguage.English ? "Application version" : "Версия приложения";
        public static string VerCore => Lang == EnumLanguage.English ? "Core version" : "Версия ядра";
        public static string VerLibBlazorAndDataCore => Lang == EnumLanguage.English ? "BlazorCore / DataCore lib versions" : "Версия библиотек BlazorCore / DataCore";
        public static string VerLibBlazorCore => Lang == EnumLanguage.English ? "BlazorCore lib version" : "Версия библиотеки BlazorCore";
        public static string VerLibDataCore => Lang == EnumLanguage.English ? "DataCore lib version" : "Версия библиотеки DataCore";
        public static string VerProgram => Lang == EnumLanguage.English ? "Program version" : "Версия программы";
        public static string VersionDb => Lang == EnumLanguage.English ? "DB version" : "Версия БД";
        public static string VersionsDb => Lang == EnumLanguage.English ? "DB versions" : "Версии БД";
        public static string DeviceType => Lang == EnumLanguage.English ? "Device types" : "Типы устройств";
        public static string Clips => Lang == EnumLanguage.English ? "SectionClips" : "Клипсы";

    }
}