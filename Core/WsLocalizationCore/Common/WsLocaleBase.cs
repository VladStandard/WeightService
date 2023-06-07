// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Common;

/// <summary>
/// Base class for localization.
/// </summary>
public class WsLocaleBase : INotifyPropertyChanged
{
    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    //private void OnPropertyChanged([CallerMemberName] string memberName = "")
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
    //}

    #endregion

    #region Public and private fields, properties, constructor

    public Loc Locale { get; set; } = Loc.Instance;
    private WsEnumLanguage _lang;
    public WsEnumLanguage Lang { get => _lang; set { _lang = value; SetLanguage(_lang); } }

    protected WsLocaleBase()
    {
        Lang = WsEnumLanguage.Russian;
    }

    #endregion

    #region Public and private methods

    private void SetLanguage(WsEnumLanguage language)
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