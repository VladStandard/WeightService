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
            case WsEnumLanguage.Russian:
                Locale.CurrentLanguage = "ru";
                break;
            case WsEnumLanguage.English:
            default:
                Locale.CurrentLanguage = "en";
                break;
        }
    }

    #endregion
}