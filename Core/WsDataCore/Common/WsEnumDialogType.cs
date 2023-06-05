// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Common;

/// <summary>
/// Тип диалога.
/// </summary>
public enum WsEnumDialogType
{
    /// <summary>
    /// По-умолчанию.
    /// </summary>
    Default,
    /// <summary>
    /// Отмена/Да.
    /// </summary>
    CancelYes,
    /// <summary>
    /// Нет/Да.
    /// </summary>
    NoYes,
    /// <summary>
    /// Ок.
    /// </summary>
    Ok,
}