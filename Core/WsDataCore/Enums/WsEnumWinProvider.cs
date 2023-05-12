// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Enums;

public enum WsEnumWinProvider
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
    Wmi = 6
}