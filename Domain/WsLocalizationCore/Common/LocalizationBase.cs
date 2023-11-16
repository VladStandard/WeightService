namespace WsLocalizationCore.Common;

public class LocalizationBase : ObservableObject
{
    #region Public and private fields, properties, constructor

    public Loc Locale { get; private set; } = new();
    private EnumLanguage _lang;
    public virtual EnumLanguage Lang { get => _lang; set { _lang = value; SetLanguage(_lang); } }

    public LocalizationBase()
    {
        Lang = EnumLanguage.Russian;
    }

    #endregion

    #region Public and private methods
    
    public void SetLocale(Loc loc)
    {
        Locale = loc;
    }
    
    private void SetLanguage(EnumLanguage language)
    {
        switch (Lang = language)
        {
            case EnumLanguage.Russian:
                Locale.CurrentLanguage = "ru";
                break;
            case EnumLanguage.English:
            default:
                Locale.CurrentLanguage = "en";
                break;
        }
    }

    #endregion
}