// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

/// <summary>
/// Менеджер локализации.
/// </summary>
public sealed class WsLocalizationManager : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public WsLocalizationLabelPrint LabelPrint { get; } = new();

    public WsLocalizationManager()
    {
        LabelPrint.SetLocale(Locale);
        LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Сменить язык.
    /// </summary>
    /// <param name="language"></param>
    public override void SetLanguage(WsEnumLanguage language)
    {
        base.SetLanguage(language);
        LabelPrint.SetLanguage(language);
    }

    #endregion
}