// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Enums;

/// <summary>
/// Страница/контрол навигации.
/// </summary>
public enum WsNavigationPage
{
    /// <summary>
    /// Страница ожидания.
    /// </summary>
    Wait,
    /// <summary>
    /// Страница смены замеса.
    /// </summary>
    Kneading,
    /// <summary>
    /// Страница смены линии.
    /// </summary>
    Lines,
    /// <summary>
    /// Страница смены ПЛУ линии.
    /// </summary>
    PlusLine,
    /// <summary>
    /// Страница смены взвешивания ПЛУ.
    /// </summary>
    PlusNesting,
    /// <summary>
    /// Страница ещё.
    /// </summary>
    More,
    /// <summary>
    /// Страница сообщения.
    /// </summary>
    MessageBox,
}