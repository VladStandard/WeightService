namespace ScalesUI.Forms;

public partial class MainForm
{
    #region Public and private methods - Смена линии

    /// <summary>
    /// Сменить линию.
    /// </summary>
    private void ActionSwitchLine(object sender, EventArgs e)
    {
        LoadNavigationLine();
        FormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            ResetWarning();
            ContextCache.Load(SqlEnumTableName.Areas);
            ContextCache.Load(SqlEnumTableName.Lines);
            FormNavigationUtils.NavigateToExistsLines(ShowFormUserControl);
        });
    }

    /// <summary>
    /// Загрузить WinForms-контрол смены линии.
    /// </summary>
    private void LoadNavigationLine()
    {
        if (FormNavigationUtils.IsLoadLines) return;
        FormNavigationUtils.IsLoadLines = true;

        FormNavigationUtils.LinesUserControl.SetupUserControl();
        FormNavigationUtils.LinesUserControl.ViewModel.CmdCancel.AddAction(FormNavigationUtils.ActionBackFromNavigation);
        FormNavigationUtils.LinesUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        FormNavigationUtils.LinesUserControl.ViewModel.CmdYes.AddAction(ReturnOkFromLines);
        FormNavigationUtils.LinesUserControl.ViewModel.CmdYes.AddAction(FormNavigationUtils.ActionBackFromNavigation);
        FormNavigationUtils.LinesUserControl.ViewModel.CmdYes.AddAction(ActionFinally);
    }
    
    private void ReturnOkFromLines()
    {
        LabelSession.SetSessionForLabelPrintCustom(
            FormNavigationUtils.LinesUserControl.ViewModel.Line,
            FormNavigationUtils.LinesUserControl.ViewModel.ProductionSite);
    }

    #endregion
}
