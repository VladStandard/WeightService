namespace WsLocalizationCore.Models;

[DebuggerDisplay("{ToString()}")]
public sealed class WsLocalizationManager : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public WsLocalizationLabelPrint LabelPrint { get; } = new();
    public WsLocalizationTests Tests { get; } = new();
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
        Tests.SetLocale(Locale);
        LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
    }

    #endregion

    #region Public and private methods

    public override string ToString() => $"{Lang} | {nameof(LabelPrint)}: {LabelPrint}";

    #endregion
}