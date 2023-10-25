namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол смены вложенности ПЛУ.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class WsXamlPlusNestingUserControl : WsFormBaseUserControl, IWsFormUserControl
{
    #region Public and private fields, properties, constructor

    public WsXamlPlusNestingViewModel ViewModel => Page.ViewModel as WsXamlPlusNestingViewModel ?? new();

    public WsXamlPlusNestingUserControl() : base(WsEnumNavigationPage.PlusNesting)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public void SetupUserControl() => ((WsXamlPlusNestingPage)Page).SetupViewModel(ViewModel);

    #endregion
}