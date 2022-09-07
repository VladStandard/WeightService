// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations
{
    public static class LocaleCore
    {
        #region Public and private fields, properties, constructor

        private static LangEnum _lang;
        public static LangEnum Lang
        {
            get => _lang;
            set => Action.Lang = Buttons.Lang = Convert.Lang = DeviceControl.Lang = Dialog.Lang = Memory.Lang = Menu.Lang = Print.Lang = 
                Scales.Lang = Settings.Lang = Sql.Lang = System.Lang = Table.Lang = Validator.Lang = _lang = value;
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

        #endregion

        #region Constructor and destructor

        static LocaleCore()
        {
            Lang = LangEnum.Russian;
        }

        #endregion

        public static class Strings
        {
            public static string AccessRights => Lang == LangEnum.English ? "Access rights" : "Права доступа";
            public static string AccessRightsAdmin => Lang == LangEnum.English ? "Admin rights" : "Админ права";
            public static string AccessRightsNone => Lang == LangEnum.English ? "No rights" : "Нет прав";
            public static string AccessRightsRead => Lang == LangEnum.English ? "Read rights" : "Права на чтение";
            public static string AccessRightsWrite => Lang == LangEnum.English ? "Write rights" : "Права на запись";
            public static string AppDevicesControlName => Lang == LangEnum.English ? "Devices Control" : "Управление устройствами";
            public static string Application => Lang == LangEnum.English ? "Application" : "Приложение";
            public static string AppSettings => Lang == LangEnum.English ? "App settings" : "Настройки приложения";
            public static string Authorization => Lang == LangEnum.English ? "Authorization" : "Авторизация";
            public static string AuthorizingApAddress => Lang == LangEnum.English ? "IP address" : "IP-адрес";
            public static string AuthorizingHostName => Lang == LangEnum.English ? "Host name" : "Имя компьютера";
            public static string AuthorizingId => Lang == LangEnum.English ? "ID" : "ИД";
            public static string AuthorizingNot => Lang == LangEnum.English ? "Not authorized!" : "Авторизация провалена!";
            public static string AuthorizingProcess => Lang == LangEnum.English ? "Authorizing ..." : "Авторизация ...";
            public static string AuthorizingSuccess => Lang == LangEnum.English ? "Success authorized" : "Успешная авторизация";
            public static string AuthorizingUserName => Lang == LangEnum.English ? "User name" : "Имя пользователя";
            public static string ButtonHeight => Lang == LangEnum.English ? "Buttons height" : "Высота кнопок";
            public static string ButtonWidth => Lang == LangEnum.English ? "Buttons width" : "Ширина кнопок";
            public static string Company => Lang == LangEnum.English ? "Vladimir Standard" : "Владимирский стандарт";
            public static string DataLoadComplete => Lang == LangEnum.English ? "Data downloaded successfully." : "Данные загружены успешно.";
            public static string DataLoadError => Lang == LangEnum.English ? "Error loading data!" : "Ошибка загрузки данных!";
            public static string DataLoading => Lang == LangEnum.English ? "Loading data..." : "Загрузка данных...";
            public static string DataSizeBytes => Lang == LangEnum.English ? "Bytes" : "Байтов";
            public static string DataSizeChars => Lang == LangEnum.English ? "chars" : "символов";
            public static string DataSizeGBytes => Lang == LangEnum.English ? "Gbytes" : "ГБайтов";
            public static string DataSizeKBytes => Lang == LangEnum.English ? "Kbytes" : "КБайтов";
            public static string DataSizeLength => Lang == LangEnum.English ? "Length" : "Длина";
            public static string DataSizeMBytes => Lang == LangEnum.English ? "Mbytes" : "МБайтов";
            public static string DataSizeVolume => Lang == LangEnum.English ? "Data volume" : "Объём данных";
            public static string DbAndMemory => Lang == LangEnum.English ? "DB and memory" : "БД и память";
            public static string DbAndMemoryGauge => Lang == LangEnum.English ? "DB and memory (gauge)" : "БД и память (измеритель)";
            public static string DbAndMemoryProgressBar => Lang == LangEnum.English ? "DB and memory (progressbar)" : "БД и память (шкала прогресса)";
            public static string DbInfo => Lang == LangEnum.English ? "Information about the DB" : "Информация о БД";
            public static string DebugMode => Lang == LangEnum.English ? "Debug mode" : "Режим отладки";
            public static string Doc => Lang == LangEnum.English ? "Documentation" : "Документация";
            public static string Feedback => Lang == LangEnum.English ? "Feedback" : "Обратная связь";
            public static string FillSize => Lang == LangEnum.English ? "Fill percentage" : "Процент заполнения";
            public static string From => Lang == LangEnum.English ? "from" : "из";
            public static string HostName => Lang == LangEnum.English ? "Host name" : "Имя хоста";
            public static string IdentityError => Lang == LangEnum.English ? "User error!" : "Ошибка пользователя";
            public static string Index => Lang == LangEnum.English ? "Inside resources" : "Внутренние ресурсы";
            public static string IndexDescription => Lang == LangEnum.English ? "The site was created to help you navigate through the company's internal resources" : "Сайт создан для помощи в навигации по внутренним ресурсам компании";
            public static string InputControlCheck(int min, int max) => Lang == LangEnum.English ? $"The value must be beetwen {min} and {max}!" : $"Значение должно быть между {min} и {max}!";
            public static string IsEnableHe(bool isTrue) => Lang == LangEnum.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включен" : "Отключен";
            public static string IsEnableIt(bool isTrue) => Lang == LangEnum.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включено" : "Отключено";
            public static string IsEnableShe(bool isTrue) => Lang == LangEnum.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включена" : "Отключена";
            public static string IsYes(bool isTrue) => Lang == LangEnum.English ? isTrue ? "Yes" : "No" : isTrue ? "Да" : "Нет";
            public static string ItemAccess => Lang == LangEnum.English ? "Access" : "Доступ";
            public static string ItemAccessAllow => Lang == LangEnum.English ? "Item access allowed" : "Доступ к элементу разрешён";
            public static string ItemAccessDeny => Lang == LangEnum.English ? "Item access denied" : "Доступ к элементу запрещён";
            public static string ItemAccessNone => Lang == LangEnum.English ? "No access to the item" : "Доступ к разделу не предусмотрен";
            public static string ItemLog => Lang == LangEnum.English ? "Log" : "Лог";
            public static string ItemsCount => Lang == LangEnum.English ? "Records" : "Записей";
            public static string Language => Lang == LangEnum.English ? "Language" : "Язык";
            public static string LanguageDetect => Lang == LangEnum.English ? "English" : "Русский";
            public static string MethodError => Lang == LangEnum.English ? "Method error" : "Ошибка метода";
            public static string NotLoad => Lang == LangEnum.English ? "Not load!" : "Не загружено";
            public static string PageError => Lang == LangEnum.English ? "Sorry, there's nothing at this address." : "Извините, по этому адресу ничего нет.";
            public static string SectionAccess => Lang == LangEnum.English ? "Access" : "Доступ";
            public static string SectionAccessAllow => Lang == LangEnum.English ? "Section access allowed" : "Доступ к разделу разрешён";
            public static string SectionAccessDeny => Lang == LangEnum.English ? "Section access denied" : "Доступ к разделу запрещён";
            public static string SectionAccessNone => Lang == LangEnum.English ? "No access to the section" : "Доступ к разделу не предусмотрен";
            public static string SectionLog => Lang == LangEnum.English ? "Log" : "Лог";
            public static string ServerDevelop => Lang == LangEnum.English ? "Debug server" : "Сервер разработки";
            public static string ServerRelease => Lang == LangEnum.English ? "Debug release" : "Промышленный сервер";
            public static string SettingName => Lang == LangEnum.English ? "Setting" : "Настройка";
            public static string SettingValue => Lang == LangEnum.English ? "Value" : "Значение";
            public static string UserSettings => Lang == LangEnum.English ? "User settings" : "Пользовательские настройки";
            public static string VerApp => Lang == LangEnum.English ? "Application version" : "Версия приложения";
            public static string VerCore => Lang == LangEnum.English ? "Core version" : "Версия ядра";
            public static string VerLibBlazorAndDataCore => Lang == LangEnum.English ? "BlazorCore / DataCore lib versions" : "Версия библиотек BlazorCore / DataCore";
            public static string VerLibBlazorCore => Lang == LangEnum.English ? "BlazorCore lib version" : "Версия библиотеки BlazorCore";
            public static string VerLibDataCore => Lang == LangEnum.English ? "DataCore lib version" : "Версия библиотеки DataCore";
            public static string VerProgram => Lang == LangEnum.English ? "Program version" : "Версия программы";
            public static string VersionDb => Lang == LangEnum.English ? "DB version" : "Версия БД";
            public static string VersionsDb => Lang == LangEnum.English ? "DB versions" : "Версии БД";
        }
    }
}
