// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

/// <summary>
/// Локализация.
/// </summary>
public class WsLocalizationModel
{
    #region Public and private fields, properties, constructor

    public Loc Locale { get; } = new();
    public WsLocaleLabelPrint LabelPrint = new();

    public WsLocalizationModel()
    {
        //Collection<string> languages = Loc.Instance.AvailableLanguages;
        SetLanguage(WsEnumLanguage.Russian);
    }

    #endregion

    #region Public and private methods

    public void SetLanguage(WsEnumLanguage language)
    {
        switch (language)
        {
            case WsEnumLanguage.Arabic:
                break;
            case WsEnumLanguage.Bengali:
                break;
            case WsEnumLanguage.Chinese:
                break;
            case WsEnumLanguage.French:
                break;
            case WsEnumLanguage.German:
                break;
            case WsEnumLanguage.Gujarati:
                break;
            case WsEnumLanguage.Hindi:
                break;
            case WsEnumLanguage.Italian:
                break;
            case WsEnumLanguage.Japanese:
                break;
            case WsEnumLanguage.Javanese:
                break;
            case WsEnumLanguage.Kannada:
                break;
            case WsEnumLanguage.Korean:
                break;
            case WsEnumLanguage.Malayalam:
                break;
            case WsEnumLanguage.Marathi:
                break;
            case WsEnumLanguage.Pashto:
                break;
            case WsEnumLanguage.Persian:
                break;
            case WsEnumLanguage.Polish:
                break;
            case WsEnumLanguage.Portuguese:
                break;
            case WsEnumLanguage.Punjabi:
                break;
            case WsEnumLanguage.Russian:
                Locale.CurrentLanguage = "ru";
                break;
            case WsEnumLanguage.Spanish:
                break;
            case WsEnumLanguage.Tamil:
                break;
            case WsEnumLanguage.Telugu:
                break;
            case WsEnumLanguage.Thai:
                break;
            case WsEnumLanguage.Turkish:
                break;
            case WsEnumLanguage.Urdu:
                break;
            case WsEnumLanguage.Vietnamese:
                break;
            case WsEnumLanguage.English:
            default:
                Locale.CurrentLanguage = "en";
                break;
        }
    }

    #endregion
}