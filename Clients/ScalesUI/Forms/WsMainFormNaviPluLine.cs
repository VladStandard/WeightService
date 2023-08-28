namespace ScalesUI.Forms;

public partial class WsMainForm
{
    #region Public and private methods - Возврат из контролов

    /// <summary>
    /// Сменить ПЛУ линии.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionSwitchPluLine(object sender, EventArgs e)
    {
        // Загрузить WinForms-контрол смены ПЛУ линии.
        LoadNavigationPlusLine();
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            MdInvokeControl.SetVisible(labelNettoWeight, false);
            MdInvokeControl.SetVisible(fieldNettoWeight, false);
            LabelSession.SetPluLine();
            // Навигация в контрол смены ПЛУ линии.
            WsFormNavigationUtils.NavigateToExistsPlusLine(ShowFormUserControl);
        });
    }
    
    /// <summary>
     /// Загрузить WinForms-контрол смены ПЛУ линии.
     /// </summary>
    private void LoadNavigationPlusLine()
    {
        if (WsFormNavigationUtils.IsLoadPlusLine) return;
        WsFormNavigationUtils.IsLoadPlusLine = true;

        WsFormNavigationUtils.PlusLineUserControl.SetupUserControl();
        WsFormNavigationUtils.PlusLineUserControl.ViewModel.CmdCancel.AddAction(ReturnCancelFromPlusLine);
        WsFormNavigationUtils.PlusLineUserControl.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.PlusLineUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.PlusLineUserControl.ViewModel.CmdYes.AddAction(ReturnOkFromPlusLine);
        WsFormNavigationUtils.PlusLineUserControl.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.PlusLineUserControl.ViewModel.CmdYes.AddAction(() => { ActionKneading(null, null); });
    }

    /// <summary>
    /// Возврат ОК из контрола смены ПЛУ.
    /// </summary>
    private void ReturnOkFromPlusLine()
    {
        LabelSession.SetPluLine(((WsXamlPlusLineViewModel)WsFormNavigationUtils.PlusLineUserControl.ViewModel).PluLine);

        LabelSession.WeighingSettings.Kneading = 1;
        LabelSession.ProductDate = DateTime.Now;
        LabelSession.NewPallet();
        MdInvokeControl.SetVisible(labelNettoWeight, LabelSession.PluLine.Plu.IsCheckWeight);
        MdInvokeControl.SetVisible(fieldNettoWeight, LabelSession.PluLine.Plu.IsCheckWeight);
    }

    /// <summary>
    /// Возврат Отмена из контрола смены ПЛУ.
    /// </summary>
    private void ReturnCancelFromPlusLine() => LabelSession.SetPluLine();

    #endregion
}
