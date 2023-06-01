// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол ввода цифр.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class WsXamlDigitsUserControl : WsFormBaseUserControl, IWsFormUserControl
{
    #region Public and private fields, properties, constructor

    public WsXamlDigitsUserControl() : base(WsEnumNavigationPage.PinCode)
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
        ((WsXamlDigitsPage)Page).SetupViewModel(Page.ViewModel is not WsXamlDigitsViewModel
            ? new WsXamlDigitsViewModel() : Page.ViewModel);

    #endregion
}