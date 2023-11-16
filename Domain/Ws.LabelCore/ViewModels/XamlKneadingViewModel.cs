namespace Ws.LabelCore.ViewModels;

/// <summary>
/// XAML модель представления замеса.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class XamlKneadingViewModel : XamlBaseViewModel, IViewModel
{
    #region Public and private fields, properties, constructor

    public XamlKneadingViewModel()
    {
        FormUserControl = EnumNavigationPage.Kneading;
    }

    #endregion
}