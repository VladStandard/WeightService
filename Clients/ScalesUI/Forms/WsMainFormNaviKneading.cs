namespace ScalesUI.Forms;

public partial class WsMainForm
{
    #region Public and private methods - Возврат из контролов

    /// <summary>
    /// Сменить замес.
    /// </summary>
    private void ActionKneading(object sender, EventArgs e)
    {
        LoadNavigationKneading();
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            ResetWarning();
            if (!UserSession.CheckPluIsEmpty(fieldWarning)) return;
            // Навигация в WinForms-контрол замеса.
            WsFormNavigationUtils.NavigateToExistsKneading(ShowFormUserControl);
        });
    }

    /// <summary>
    /// Загрузить WinForms-контрол смены замеса.
    /// </summary>
    private void LoadNavigationKneading()
    {
        if (WsFormNavigationUtils.IsLoadKneading) return;
        WsFormNavigationUtils.IsLoadKneading = true;

        WsFormNavigationUtils.KneadingUserControl.SetupUserControl();
        WsFormNavigationUtils.KneadingUserControl.ViewModel.CmdCancel.AddAction(PluginMassaExecute);
        WsFormNavigationUtils.KneadingUserControl.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.KneadingUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.KneadingUserControl.ViewModel.CmdYes.AddAction(PluginMassaExecute);
        WsFormNavigationUtils.KneadingUserControl.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.KneadingUserControl.ViewModel.CmdYes.AddAction(ActionFinally);
    }

    #endregion
}
