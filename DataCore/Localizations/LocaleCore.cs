// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Localizations
{
    public static class LocaleCore
    {
        #region Public and private fields and properties

        private static ShareEnums.Lang _lang;
        public static ShareEnums.Lang Lang
        {
            get => _lang;
            set => Action.Lang = Convert.Lang = DeviceControl.Lang = Dialog.Lang = Memory.Lang = Menu.Lang = Print.Lang = 
                Scales.Lang = Settings.Lang = Sql.Lang = System.Lang = Table.Lang = _lang = value;
        }
        public static LocaleAction Action { get; private set; } = LocaleAction.Instance;
        public static LocaleConvert Convert { get; private set; } = LocaleConvert.Instance;
        public static LocaleDeviceControl DeviceControl { get; private set; } = LocaleDeviceControl.Instance;
        public static LocaleDialog Dialog { get; private set; } = LocaleDialog.Instance;
        public static LocaleMemory Memory { get; private set; } = LocaleMemory.Instance;
        public static LocaleMenu Menu { get; private set; } = LocaleMenu.Instance;
        public static LocalePrint Print { get; private set; } = LocalePrint.Instance;
        public static LocaleScale Scales { get; private set; } = LocaleScale.Instance;
        public static LocaleSettings Settings { get; private set; } = LocaleSettings.Instance;
        public static LocaleSql Sql { get; private set; } = LocaleSql.Instance;
        public static LocaleSystem System { get; private set; } = LocaleSystem.Instance;
        public static LocaleTable Table { get; private set; } = LocaleTable.Instance;

        #endregion

        #region Constructor and destructor

        static LocaleCore()
        {
            Lang = ShareEnums.Lang.Russian;
        }

        #endregion

        public static class Strings
        {
            public static string AccessRights => Lang == ShareEnums.Lang.English ? "Access rights" : "Права доступа";
            public static string AccessRightsAdmin => Lang == ShareEnums.Lang.English ? "Admin rights" : "Админ права";
            public static string AccessRightsNone => Lang == ShareEnums.Lang.English ? "No rights" : "Нет прав";
            public static string AccessRightsRead => Lang == ShareEnums.Lang.English ? "Read rights" : "Права на чтение";
            public static string AccessRightsWrite => Lang == ShareEnums.Lang.English ? "Write rights" : "Права на запись";
            public static string AppDevicesControlName => Lang == ShareEnums.Lang.English ? "Devices Control" : "Управление устройствами";
            public static string Application => Lang == ShareEnums.Lang.English ? "Application" : "Приложение";
            public static string AppSettings => Lang == ShareEnums.Lang.English ? "App settings" : "Настройки приложения";
            public static string Authorization => Lang == ShareEnums.Lang.English ? "Authorization" : "Авторизация";
            public static string AuthorizingApAddress => Lang == ShareEnums.Lang.English ? "IP address" : "IP-адрес";
            public static string AuthorizingHostName => Lang == ShareEnums.Lang.English ? "Host name" : "Имя компьютера";
            public static string AuthorizingId => Lang == ShareEnums.Lang.English ? "ID" : "ИД";
            public static string AuthorizingNot => Lang == ShareEnums.Lang.English ? "Not authorized!" : "Авторизация провалена!";
            public static string AuthorizingProcess => Lang == ShareEnums.Lang.English ? "Authorizing ..." : "Авторизация ...";
            public static string AuthorizingSuccess => Lang == ShareEnums.Lang.English ? "Success authorized" : "Успешная авторизация";
            public static string AuthorizingUserName => Lang == ShareEnums.Lang.English ? "User name" : "Имя пользователя";
            public static string ButtonHeight => Lang == ShareEnums.Lang.English ? "Buttons height" : "Высота кнопок";
            public static string ButtonWidth => Lang == ShareEnums.Lang.English ? "Buttons width" : "Ширина кнопок";
            public static string Company => Lang == ShareEnums.Lang.English ? "Vladimir Standard" : "Владимирский стандарт";
            public static string DataLoadComplete => Lang == ShareEnums.Lang.English ? "Data downloaded successfully." : "Данные загружены успешно.";
            public static string DataLoadError => Lang == ShareEnums.Lang.English ? "Error loading data!" : "Ошибка загрузки данных!";
            public static string DataLoading => Lang == ShareEnums.Lang.English ? "Loading data..." : "Загрузка данных...";
            public static string DataSizeBytes => Lang == ShareEnums.Lang.English ? "Bytes" : "Байтов";
            public static string DataSizeChars => Lang == ShareEnums.Lang.English ? "chars" : "символов";
            public static string DataSizeGBytes => Lang == ShareEnums.Lang.English ? "Gbytes" : "ГБайтов";
            public static string DataSizeKBytes => Lang == ShareEnums.Lang.English ? "Kbytes" : "КБайтов";
            public static string DataSizeLength => Lang == ShareEnums.Lang.English ? "Length" : "Длина";
            public static string DataSizeMBytes => Lang == ShareEnums.Lang.English ? "Mbytes" : "МБайтов";
            public static string DataSizeVolume => Lang == ShareEnums.Lang.English ? "Data volume" : "Объём данных";
            public static string DbInfo => Lang == ShareEnums.Lang.English ? "Information about the DB" : "Информация о БД";
            public static string DebugMode => Lang == ShareEnums.Lang.English ? "Debug mode" : "Режим отладки";
            public static string Doc => Lang == ShareEnums.Lang.English ? "Documentation" : "Документация";
            public static string Feedback => Lang == ShareEnums.Lang.English ? "Feedback" : "Обратная связь";
            public static string From => Lang == ShareEnums.Lang.English ? "from" : "из";
            public static string HostName => Lang == ShareEnums.Lang.English ? "Host name" : "Имя хоста";
            public static string IdentityError => Lang == ShareEnums.Lang.English ? "User error!" : "Ошибка пользователя";
            public static string Index => Lang == ShareEnums.Lang.English ? "Inside resources" : "Внутренние ресурсы";
            public static string IndexDescription => Lang == ShareEnums.Lang.English ? "The site was created to help you navigate through the company's internal resources" : "Сайт создан для помощи в навигации по внутренним ресурсам компании";
            public static string InputControlMuchZero = Lang == ShareEnums.Lang.English ? "The value must be greater than 0!" : "Значение должно быть больше 0!";
            public static string IsEnableHe(bool isTrue) => Lang == ShareEnums.Lang.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включен" : "Отключен";
            public static string IsEnableIt(bool isTrue) => Lang == ShareEnums.Lang.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включено" : "Отключено";
            public static string IsEnableShe(bool isTrue) => Lang == ShareEnums.Lang.English ? isTrue ? "Enable" : "Disable" : isTrue ? "Включена" : "Отключена";
            public static string IsYes(bool isTrue) => Lang == ShareEnums.Lang.English ? isTrue ? "Yes" : "No" : isTrue ? "Да" : "Нет";
            public static string ItemAccess => Lang == ShareEnums.Lang.English ? "Access" : "Доступ";
            public static string ItemAccessAllow => Lang == ShareEnums.Lang.English ? "Item access allowed" : "Доступ к элементу разрешён";
            public static string ItemAccessDeny => Lang == ShareEnums.Lang.English ? "Item access denied" : "Доступ к элементу запрещён";
            public static string ItemAccessNone => Lang == ShareEnums.Lang.English ? "No access to the item" : "Доступ к разделу не предусмотрен";
            public static string ItemLog => Lang == ShareEnums.Lang.English ? "Log" : "Лог";
            public static string ItemsCount => Lang == ShareEnums.Lang.English ? "Count of records" : "Количество записей";
            public static string Language => Lang == ShareEnums.Lang.English ? "Language" : "Язык";
            public static string LanguageDetect => Lang == ShareEnums.Lang.English ? "English" : "Русский";
            public static string MethodError => Lang == ShareEnums.Lang.English ? "Method error" : "Ошибка метода";
            public static string NotLoad => Lang == ShareEnums.Lang.English ? "Not load!" : "Не загружено";
            public static string PageError => Lang == ShareEnums.Lang.English ? "Sorry, there's nothing at this address." : "Извините, по этому адресу ничего нет.";
            public static string RecordName => Lang == ShareEnums.Lang.English ? "Setting name" : "Название записи";
            public static string RecordValue => Lang == ShareEnums.Lang.English ? "Setting value" : "Значение записи";
            public static string SectionAccess => Lang == ShareEnums.Lang.English ? "Access" : "Доступ";
            public static string SectionAccessAllow => Lang == ShareEnums.Lang.English ? "Section access allowed" : "Доступ к разделу разрешён";
            public static string SectionAccessDeny => Lang == ShareEnums.Lang.English ? "Section access denied" : "Доступ к разделу запрещён";
            public static string SectionAccessNone => Lang == ShareEnums.Lang.English ? "No access to the section" : "Доступ к разделу не предусмотрен";
            public static string SectionLog => Lang == ShareEnums.Lang.English ? "Log" : "Лог";
            public static string ServerDevelop => Lang == ShareEnums.Lang.English ? "Debug server" : "Сервер разработки";
            public static string ServerRelease => Lang == ShareEnums.Lang.English ? "Debug release" : "Промышленный сервер";
            public static string SettingName => Lang == ShareEnums.Lang.English ? "Setting name" : "Название настройки";
            public static string SettingValue => Lang == ShareEnums.Lang.English ? "Setting value" : "Значение настройки";
            public static string UserSettings => Lang == ShareEnums.Lang.English ? "User settings" : "Пользовательские настройки";
            public static string VerApp => Lang == ShareEnums.Lang.English ? "Application version" : "Версия приложения";
            public static string VerCore => Lang == ShareEnums.Lang.English ? "Core version" : "Версия ядра";
            public static string VerLibBlazorCore => Lang == ShareEnums.Lang.English ? "BlazorCore lib version" : "Версия библиотеки BlazorCore";
            public static string VerLibDataCore => Lang == ShareEnums.Lang.English ? "DataCore lib version" : "Версия библиотеки DataCore";
            public static string VerProgram => Lang == ShareEnums.Lang.English ? "Program version" : "Версия программы";
        }
    }
}
