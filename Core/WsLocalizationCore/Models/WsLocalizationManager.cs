// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
namespace WsLocalizationCore.Models;

/// <summary>
/// Менеджер локализации.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public sealed class WsLocalizationManager : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public WsLocalizationLabelPrint LabelPrint { get; } = new();
    private WsEnumLanguage _lang;
    public override WsEnumLanguage Lang
    {
        get => _lang;
        set
        {
            _lang = value;
            base.Lang = value;
            LabelPrint.Lang = value;
        }
    }

    public WsLocalizationManager()
    {
        LabelPrint.SetLocale(Locale);
        LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
    }

    #endregion

    #region Public and private methods

    public override string ToString() => $"{Lang} | {nameof(LabelPrint)}: {LabelPrint}";

    #endregion
}