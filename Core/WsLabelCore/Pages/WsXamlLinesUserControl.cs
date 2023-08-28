namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол смены линии.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class WsXamlLinesUserControl : WsFormBaseUserControl, IWsFormUserControl
{
    #region Public and private fields, properties, constructor

    public WsXamlLinesViewModel ViewModel => Page.ViewModel as WsXamlLinesViewModel ?? new();

    public WsXamlLinesUserControl() : base(WsEnumNavigationPage.Line)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public void SetupUserControl() => ((WsXamlLinesPage)Page).SetupViewModel(ViewModel);

    #endregion
}