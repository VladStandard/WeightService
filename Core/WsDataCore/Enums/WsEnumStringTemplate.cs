// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Enums;

public enum WsEnumStringTemplate
{
    /// <summary>
    /// "text" or = "text".
    /// </summary>
    Equals = 0,

    /// <summary>
    /// "*text*" or like "%text%".
    /// </summary>
    Contains = 1,

    /// <summary>
    /// "text*" or like "text%".
    /// </summary>
    StartsWith = 2,

    /// <summary>
    /// "*text" or like "%text".
    /// </summary>
    EndsWith = 3
}