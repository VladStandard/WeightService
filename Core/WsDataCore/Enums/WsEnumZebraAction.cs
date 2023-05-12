// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Enums;

/// <summary>
/// Действие принтера Зебры.
/// </summary>
public enum WsEnumZebraAction
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
    LoadImages
}