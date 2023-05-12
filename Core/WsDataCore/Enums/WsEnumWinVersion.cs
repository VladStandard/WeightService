// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Enums;

public enum WsEnumWinVersion
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
    Win10x64 = 3
}