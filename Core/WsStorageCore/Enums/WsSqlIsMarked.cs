// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Enums;

public enum WsSqlIsMarked
{
    /// <summary>
    /// Отображать все записи (например, поле не существует в таблице).
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