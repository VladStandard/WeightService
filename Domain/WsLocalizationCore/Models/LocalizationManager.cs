namespace WsLocalizationCore.Models;

[DebuggerDisplay("{ToString()}")]
public sealed class LocalizationManager : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public LocalizationLabelPrint LabelPrint { get; } = new();
    public LocalizationTests Tests { get; } = new();
    private EnumLanguage _lang;
    public override EnumLanguage Lang
    {
        get => _lang;
        set
        {
            _lang = value;
            base.Lang = value;
            LabelPrint.Lang = value;
        }
    }

    public LocalizationManager()
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