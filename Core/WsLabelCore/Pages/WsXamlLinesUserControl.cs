// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол смены линии.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class WsXamlLinesUserControl : WsFormBaseUserControl, IWsFormUserControl
{
    #region Public and private fields, properties, constructor

    public WsXamlLinesUserControl() : base(WsEnumNavigationPage.Line)
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
        ((WsXamlLinesPage)Page).SetupViewModel(Page.ViewModel is not WsXamlLinesViewModel
            ? new WsXamlLinesViewModel() : Page.ViewModel);

    #endregion
}