// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол диалога.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class WsXamlDialogUserControl : WsFormBaseUserControl, IWsFormUserControl
{
    #region Public and private fields, properties, constructor

    public WsXamlDialogUserControl() : base(WsEnumNavigationPage.Dialog)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => Page.ViewModel.ToString();

    /// <summary>
    /// Настроить WinForms-контрол.
    /// </summary>
    public void SetupUserConrol() =>
        ((WsXamlDialogPage)Page).SetupViewModel(Page.ViewModel is not WsXamlDialogViewModel
            ? new WsXamlDialogViewModel() : Page.ViewModel);

    #endregion
}