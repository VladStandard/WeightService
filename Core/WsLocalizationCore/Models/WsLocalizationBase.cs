// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

/// <summary>
/// Base class for localization.
/// </summary>
public class WsLocalizationBase
{
    public Lang Lang { get; set; } = Lang.Russian;
}