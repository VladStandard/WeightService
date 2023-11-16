namespace ScalesUI.Forms;

public partial class WsMainForm
{
    #region Public and private methods - Смена линии

    /// <summary>
    /// Сменить линию.
    /// </summary>
    private void ActionSwitchLine(object sender, EventArgs e)
    {
        LoadNavigationLine();
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            ResetWarning();
            ContextCache.Load(SqlEnumTableName.Areas);
            ContextCache.Load(SqlEnumTableName.Lines);
            WsFormNavigationUtils.NavigateToExistsLines(ShowFormUserControl);
        });
    }

    /// <summary>
    /// Загрузить WinForms-контрол смены линии.
    /// </summary>
    private void LoadNavigationLine()
    {
        if (WsFormNavigationUtils.IsLoadLines) return;
        WsFormNavigationUtils.IsLoadLines = true;

        WsFormNavigationUtils.LinesUserControl.SetupUserControl();
        WsFormNavigationUtils.LinesUserControl.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.LinesUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.LinesUserControl.ViewModel.CmdYes.AddAction(ReturnOkFromLines);
        WsFormNavigationUtils.LinesUserControl.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.LinesUserControl.ViewModel.CmdYes.AddAction(ActionFinally);
    }
    
    private void ReturnOkFromLines()
    {
        LabelSession.SetSessionForLabelPrintCustom(
            WsFormNavigationUtils.LinesUserControl.ViewModel.Line,
            WsFormNavigationUtils.LinesUserControl.ViewModel.ProductionSite);
    }

    #endregion
}
