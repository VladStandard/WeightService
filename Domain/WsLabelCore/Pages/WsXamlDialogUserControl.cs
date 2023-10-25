namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол диалога.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class WsXamlDialogUserControl : WsFormBaseUserControl, IWsFormUserControl
{
    #region Public and private fields, properties, constructor

    public WsXamlDialogViewModel ViewModel => Page.ViewModel as WsXamlDialogViewModel ?? new();
    public WsXamlDialogUserControl() : base(WsEnumNavigationPage.Dialog)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Настроить WinForms-контрол.
    /// </summary>
    public void SetupUserControl() => ((WsXamlDialogPage)Page).SetupViewModel(ViewModel);

    #endregion
}