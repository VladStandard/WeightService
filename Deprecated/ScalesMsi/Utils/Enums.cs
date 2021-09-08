// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Diagnostics.CodeAnalysis;

namespace ScalesMsi.Utils
{
    /// <summary>
    /// Поддерживаемые языки.
    /// </summary>
    internal enum EnumLocalization
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
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal  enum EnumWinVersion
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
    internal  enum EnumWinProvider
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
    internal  enum EnumStringTemplate
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
    /// UI без-взаимодействия с пользователем.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal  enum EnumSilentUI
    {
        True,
        False,
    }

    /// <summary>
    /// Строковый формат.
    /// </summary>
    internal  enum EnumStringFormat
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
    internal  enum EnumVerCountDigits
    {
        Use1,
        Use2,
        Use3,
        Use4,
    }

    /// <summary>
    /// Хранилище настроек.
    /// </summary>
    internal  enum EnumSettingsStorage
    {
        UseParams,
        //UseRegistry,
        UseConfig,
    }

    /// <summary>
    /// Уровень подробности строки подключения.
    /// </summary>
    internal  enum EnumConStringLevel
    {
        Basic,
        Low,
        Middle,
        Full,
    }

    /// <summary>
    /// Компоненты ПО.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum EnumComponents
    {
        ScalesUI,
        ScalesUIBinaries,
        ScalesUIDocs,
        MassaDriver,
        ScalesUIManuals,
        TapangaMaha,
        LabelPrint,
    }
}
