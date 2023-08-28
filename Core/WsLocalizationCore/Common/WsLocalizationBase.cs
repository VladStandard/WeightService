namespace WsLocalizationCore.Common;

public class WsLocalizationBase : ObservableObject
{
    #region Public and private fields, properties, constructor

    public Loc Locale { get; private set; } = new();
    private WsEnumLanguage _lang;
    public virtual WsEnumLanguage Lang { get => _lang; set { _lang = value; SetLanguage(_lang); } }

    public WsLocalizationBase()
    {
        Lang = WsEnumLanguage.Russian;
    }

    #endregion

    #region Public and private methods
    
    public void SetLocale(Loc loc)
    {
        Locale = loc;
    }
    
    private void SetLanguage(WsEnumLanguage language)
    {
        switch (Lang = language)
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