// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Enums;

/// <summary>
/// Конфигурация поля IS_MARKED.
/// </summary>
public enum WsSqlIsMarked
{
    /// <summary>
    /// Отображать все записи, либо поле не существует.
    /// </summary>
    ShowAll,
    /// <summary>
    /// Отображать только актуальные записи.
    /// </summary>
    ShowOnlyActual,
    /// <summary>
    /// Отображать только скрытые записи.
    /// </summary>
    ShowOnlyHide,
}