namespace WsLabelCore.ViewModels;

/// <summary>
/// XAML модель представления замеса.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsXamlKneadingViewModel : WsXamlBaseViewModel, IWsViewModel
{
    #region Public and private fields, properties, constructor

    public WsXamlKneadingViewModel()
    {
        FormUserControl = WsEnumNavigationPage.Kneading;
    }

    #endregion
}