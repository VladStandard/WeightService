// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

/// <summary>
/// Контрол смены вложенности ПЛУ.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class WsFormPlusNestingUserControl : WsFormBaseUserControl, IWsFormUserControl
{
    #region Public and private fields, properties, constructor

    public WsFormPlusNestingUserControl() : base(WsEnumFormUserControl.PlusNesting)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => Page.ViewModel.ToString();

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public void SetupUserConrol()
    {
        ((WsXamlPlusNestingPage)Page).SetupViewModel(Page.ViewModel is not WsXamlPlusNestingViewModel 
            ? new WsXamlPlusNestingViewModel() : Page.ViewModel);

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            ((WsXamlPlusNestingViewModel)Page.ViewModel).PluNesting = LabelSession.ViewPluNesting;
            ((WsXamlPlusNestingViewModel)Page.ViewModel).PlusNestings = ContextManager.ContextView.GetListViewPlusNesting((ushort)LabelSession.PluLine.Plu.Number);
        });
    }

    #endregion
}