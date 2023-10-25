namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол ввода цифр.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class WsXamlDigitsUserControl : WsFormBaseUserControl, IWsFormUserControl
{
    #region Public and private fields, properties, constructor

    public WsXamlDigitsViewModel ViewModel => Page.ViewModel as WsXamlDigitsViewModel ?? new();
    public WsXamlDigitsUserControl() : base(WsEnumNavigationPage.Digit)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public void SetupUserControl() => 
        ((WsXamlDigitsPage)Page).SetupViewModel(ViewModel);

    #endregion
}