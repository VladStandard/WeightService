// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Common;

/// <summary>
/// Базовый класс локализации.
/// </summary>
public class WsLocalizationBase : INotifyPropertyChanged
{
    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region Public and private fields, properties, constructor

    //public Loc Locale { get; private set; } = Loc.Instance;
    public Loc Locale { get; private set; } = new();
    private WsEnumLanguage _lang;
    public WsEnumLanguage Lang { get => _lang; set { _lang = value; SetLanguage(_lang); } }

    public WsLocalizationBase()
    {
        Lang = WsEnumLanguage.Russian;
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Задать локаль.
    /// </summary>
    /// <param name="loc"></param>
    public void SetLocale(Loc loc)
    {
        Locale = loc;
    }

    /// <summary>
    /// Сменить язык.
    /// </summary>
    /// <param name="language"></param>
    public virtual void SetLanguage(WsEnumLanguage language)
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