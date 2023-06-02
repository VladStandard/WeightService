// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Common;

/// <summary>
/// Страница навигации.
/// </summary>
public enum WsEnumNavigationPage
{
    /// <summary>
    /// Поумолчанию.
    /// </summary>
    Default,
    /// <summary>
    /// Диалог.
    /// </summary>
    Dialog,
    /// <summary>
    /// Ввод цифр.
    /// </summary>
    Digit,
    /// <summary>
    /// Смена замеса.
    /// </summary>
    Kneading,
    /// <summary>
    /// Смена линии.
    /// </summary>
    Line,
    /// <summary>
    /// Ввод цифр.
    /// </summary>
    PinCode,
    /// <summary>
    /// Смена ПЛУ линии.
    /// </summary>
    PlusLine,
    /// <summary>
    /// Смена взвешивания ПЛУ.
    /// </summary>
    PlusNesting,
    /// <summary>
    /// Страница ожидания.
    /// </summary>
    Wait,
}