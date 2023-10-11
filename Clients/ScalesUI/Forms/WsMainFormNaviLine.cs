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
            // Сброс предупреждения.
            ResetWarning();
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.Areas);
            ContextCache.Load(WsSqlEnumTableName.Lines);
            // Навигация в контрол линии.
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
        LabelSession.SetSessionForLabelPrint(ShowFormUserControl,
            WsFormNavigationUtils.LinesUserControl.ViewModel.Line.IdentityValueId,
            WsFormNavigationUtils.LinesUserControl.ViewModel.ProductionSite);
    }

    #endregion
}
