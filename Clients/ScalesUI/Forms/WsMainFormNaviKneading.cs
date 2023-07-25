// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Forms;

public partial class WsMainForm
{
    #region Public and private methods - Возврат из контролов

    /// <summary>
    /// Сменить замес.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionKneading(object sender, EventArgs e)
    {
        // Загрузить WinForms-контрол смены замеса.
        LoadNavigationKneading();
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            // Проверить наличие ПЛУ.
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