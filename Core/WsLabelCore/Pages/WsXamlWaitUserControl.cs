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