namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол ожидания.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public partial class WsXamlWaitUserControl : WsFormBaseUserControl, IWsFormUserControl
{
    #region Public and private fields, properties, constructor

    public WsXamlWaitViewModel ViewModel => Page.ViewModel as WsXamlWaitViewModel ?? new();

    public WsXamlWaitUserControl() : base(WsEnumNavigationPage.Wait)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public void SetupUserControl() => ((WsXamlWaitPage)Page).SetupViewModel(ViewModel);

    #endregion
}