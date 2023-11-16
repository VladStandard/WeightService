namespace ScalesUI.Forms;

public partial class MainForm
{
    #region Public and private methods - Возврат из контролов

    /// <summary>
    /// Сменить замес.
    /// </summary>
    private void ActionKneading(object sender, EventArgs e)
    {
        LoadNavigationKneading();
        FormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            ResetWarning();
            if (!UserSession.CheckPluIsEmpty(fieldWarning)) return;
            // Навигация в WinForms-контрол замеса.
            FormNavigationUtils.NavigateToExistsKneading(ShowFormUserControl);
        });
    }

    /// <summary>
    /// Загрузить WinForms-контрол смены замеса.
    /// </summary>
    private void LoadNavigationKneading()
    {
        if (FormNavigationUtils.IsLoadKneading) return;
        FormNavigationUtils.IsLoadKneading = true;

        FormNavigationUtils.KneadingUserControl.SetupUserControl();
        FormNavigationUtils.KneadingUserControl.ViewModel.CmdCancel.AddAction(PluginMassaExecute);
        FormNavigationUtils.KneadingUserControl.ViewModel.CmdCancel.AddAction(FormNavigationUtils.ActionBackFromNavigation);
        FormNavigationUtils.KneadingUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        FormNavigationUtils.KneadingUserControl.ViewModel.CmdYes.AddAction(PluginMassaExecute);
        FormNavigationUtils.KneadingUserControl.ViewModel.CmdYes.AddAction(FormNavigationUtils.ActionBackFromNavigation);
        FormNavigationUtils.KneadingUserControl.ViewModel.CmdYes.AddAction(ActionFinally);
    }

    #endregion
}
