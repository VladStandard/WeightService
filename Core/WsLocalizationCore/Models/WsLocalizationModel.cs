// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

/// <summary>
/// Локализация.
/// </summary>
public class WsLocalizationModel : WsLocaleBase
{
    #region Public and private fields, properties, constructor

    public WsLocaleLabelPrint LabelPrint { get; } = new();

    public WsLocalizationModel()
    {
        LabelPrint.Lang = Lang;
        LabelPrint.Locale = Locale;
        LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
        LocalizationLoader.Instance.AddDirectory(@"Locales");
    }

    #endregion
}