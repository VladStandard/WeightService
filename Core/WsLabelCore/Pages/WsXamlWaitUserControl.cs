// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол ожидания.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public partial class WsXamlWaitUserControl : WsFormBaseUserControl, IWsFormUserControl
{
    #region Public and private fields, properties, constructor

    public WsXamlWaitUserControl() : base(WsEnumNavigationPage.Wait)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => Page.ViewModel.ToString();

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public void SetupUserConrol() =>
        ((WsXamlWaitPage)Page).SetupViewModel(Page.ViewModel is not WsXamlWaitViewModel
            ? new WsXamlWaitViewModel() : Page.ViewModel);

    #endregion
}