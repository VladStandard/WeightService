// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesCore.Models
{
    /// <summary>
    /// Разрешение экрана.
    /// </summary>
    public enum EnumWindowResolution
    {
        Default,
        Res_800x600,
        Res_1024x768,
        Res_1366х768,
        Res_1920х1080,
    }

    /// <summary>
    /// Поддерживаемые языки.
    /// </summary>
    public enum EnumLanguage
    {
        /// <summary>
        /// Английская локализация.
        /// </summary>
        English = 0,

        /// <summary>
        /// Русская локализация.
        /// </summary>
        Russian = 1,
    }

    /// <summary>
    /// Возвращаемое значение.
    /// </summary>
    public enum EnumResult
    {
        /// <summary>
        /// Ошибка.
        /// </summary>
        Error = -1,

        /// <summary>
        /// Выполнено успешно.
        /// </summary>
        Good = 0,
    }

    /// <summary>
    /// Возвращаемое значение.
    /// </summary>
    public enum EnumWinVersion
    {
/*
+------------------------------------------------------------------------------+
|                    |   PlatformID    |   Major version   |   Minor version   |
+------------------------------------------------------------------------------+
| Windows 95         |  Win32Windows   |         4         |          0        |
| Windows 98         |  Win32Windows   |         4         |         10        |
| Windows Me         |  Win32Windows   |         4         |         90        |
| Windows NT 4.0     |  Win32NT        |         4         |          0        |
| Windows 2000       |  Win32NT        |         5         |          0        |
| Windows XP         |  Win32NT        |         5         |          1        |
| Windows 2003       |  Win32NT        |         5         |          2        |
| Windows Vista      |  Win32NT        |         6         |          0        |
| Windows 2008       |  Win32NT        |         6         |          0        |
| Windows 7          |  Win32NT        |         6         |          1        |
| Windows 2008 R2    |  Win32NT        |         6         |          1        |
| Windows 8          |  Win32NT        |         6         |          2        |
| Windows 8.1        |  Win32NT        |         6         |          3        |
+------------------------------------------------------------------------------+
| Windows 10         |  Win32NT        |        10         |          0        |
+------------------------------------------------------------------------------+
*/
        /// <summary>
        /// Не поддерживается.
        /// </summary>
        Unsupported = -1,

        /// <summary>
        /// Windows 7 x32.
        /// </summary>
        Win7x32 = 0,

        /// <summary>
        /// Windows 7 x64.
        /// </summary>
        Win7x64 = 1,

        /// <summary>
        /// Windows 10 x32.
        /// </summary>
        Win10x32 = 2,

        /// <summary>
        /// Windows 10 x64.
        /// </summary>
        Win10x64 = 3,
    }

    /// <summary>
    /// Провайдер Windows.
    /// </summary>
    public enum EnumWinProvider
    {
        /// <summary>
        /// Реестр.
        /// </summary>
        Registry = 0,

        /// <summary>
        /// Псевдонимы.
        /// </summary>
        Alias = 1,

        /// <summary>
        /// Окружение.
        /// </summary>
        Environment = 2,

        /// <summary>
        /// Файловая система.
        /// </summary>
        FileSystem = 3,

        /// <summary>
        /// Функции.
        /// </summary>
        Function = 4,

        /// <summary>
        /// Переменные.
        /// </summary>
        Variable = 5,

        /// <summary>
        /// Windows Management Instrumentation.
        /// </summary>
        Wmi = 6,
    }

    /// <summary>
    /// Шаблон строки.
    /// </summary>
    public enum EnumStringTemplate
    {
        /// <summary>
        /// Использовать шаблон "text" или = "text".
        /// </summary>
        Equals = 0,

        /// <summary>
        /// Использовать шаблон "*text*" или like "%text%".
        /// </summary>
        Contains = 1,

        /// <summary>
        /// Использовать шаблон "text*" или like "text%".
        /// </summary>
        StartsWith = 2,

        /// <summary>
        /// Использовать шаблон "*text" или like "%text".
        /// </summary>
        EndsWith = 3,
    }

    /// <summary>
    /// Сообщение мыши.
    /// </summary>
    public enum EnumMouseMessages
    {
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_MOUSEMOVE = 0x0200,
        WM_MOUSEWHEEL = 0x020A,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_MBUTTONDOWN = 0x0207
    }

    /// <summary>
    /// Параметры отображения окна.
    /// </summary>
    public enum EnumCmdShow
    {
        SW_HIDE, // Скрывает окно и активизирует другое окно.
        SW_MAXIMIZE, // Развертывает определяемое окно.
        SW_MINIMIZE, // Свертывает определяемое окно и активизирует следующее окно верхнего уровня в Z-последовательности.
        SW_RESTORE, // Активизирует и отображает окно. Если окно свернуто или развернуто, Windows восстанавливает в его первоначальных размерах и позиции.

        // Прикладная программа должна установить этот флажок при восстановлении свернутого окна.
        SW_SHOW, //  Активизирует окно и отображает его текущие размеры и позицию.
        SW_SHOWDEFAULT, // Устанавливает состояние показа, основанное на флажке SW_, определенном в структуре STARTUPINFO, 

        // переданной в функцию CreateProcess программой, которая запустила прикладную программу.
        SW_SHOWMAXIMIZED, //  Активизирует окно и отображает его как развернутое окно.
        SW_SHOWMINIMIZED, // Активизирует окно и отображает его как свернутое окно.
        SW_SHOWMINNOACTIVE, // Отображает окно как свернутое окно. Активное окно остается активным.
        SW_SHOWNA, // Отображает окно в его текущем состоянии. Активное окно остается активным.
        SW_SHOWNOACTIVATE, // Отображает окно в его самом современном размере и позиции. Активное окно остается активным.
        SW_SHOWNORMAL, //  Активизирует и отображает окно. Если окно свернуто или развернуто, Windows восстанавливает его в первоначальном размере и позиции.
        // Прикладная программа должна установить этот флажок при отображении окна впервые.
    }

    /// <summary>
    /// Настройки по-умолчанию.
    /// </summary>
    public enum EnumDefaultSetting
    {
        All,
        ComPortName,
        SendTimeout,
        ReceiveTimeout,
        ZebraTcpAddress,
        ZebraTcpPort,
        Description,
    }

    /// <summary>
    /// UI без-взаимодействия с пользователем.
    /// </summary>
    public enum EnumSilentUI
    {
        True,
        False,
    }

    /// <summary>
    /// Действие принтера Зебры.
    /// </summary>
    public enum EnumZebraAction
    {
        /// <summary>
        /// Сбросить принтер.
        /// </summary>
        Reset,
        /// <summary>
        /// Калибровать принтер.
        /// </summary>
        Calibrate,
        /// <summary>
        /// Список шрифтов.
        /// </summary>
        FontsList,
        /// <summary>
        /// Загрузить шрифты.
        /// </summary>
        LoadFonts,
        /// <summary>
        /// Очистить шрифты.
        /// </summary>
        ClearFonts,
        /// <summary>
        /// ~WC.
        /// </summary>
        WC,
        /// <summary>
        /// Загрузить картинки.
        /// </summary>
        LoadImages,
    }

    /// <summary>
    /// Строковый формат.
    /// </summary>
    public enum EnumStringFormat
    {
        AsString,
        Use1,
        Use2,
        Use3,
        Use4,
    }

    /// <summary>
    /// Количество символов для строки версии ПО.
    /// </summary>
    public enum EnumVerCountDigits
    {
        Use1,
        Use2,
        Use3,
        Use4,
    }

    /// <summary>
    /// Хранилище настроек.
    /// </summary>
    public enum EnumSettingsStorage
    {
        UseParams,
        //UseRegistry,
        UseConfig,
    }

    /// <summary>
    /// Уровень подробности строки подключения.
    /// </summary>
    public enum EnumConStringLevel
    {
        Basic,
        Low,
        Middle,
        Full,
    }

    /// <summary>
    /// Активная страница.
    /// </summary>
    public enum WpfActivePage
    {
        About,
        ChangeLog,
        PinCode,
        Home,
        Settings,
    }
}
