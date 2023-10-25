namespace WsLabelCore.ViewModels;

/// <summary>
/// XAML модель представления ввода цифр.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public class WsXamlDigitsViewModel : WsXamlBaseViewModel, IWsViewModel
{
    #region Public and private fields, properties, constructor

    public WsXamlDigitsViewModel()
    {
        FormUserControl = WsEnumNavigationPage.Digit;
    }

    #endregion
}