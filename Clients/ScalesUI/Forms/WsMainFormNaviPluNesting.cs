namespace ScalesUI.Forms;

public partial class WsMainForm
{
    #region Public and private methods - Возврат из контролов

    /// <summary>
    /// Сменить вложенность ПЛУ.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionSwitchPluNesting(object sender, EventArgs e)
    {
        // Загрузить WinForms-контрол смены вложенности ПЛУ.
        LoadNavigationPlusNesting();
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            // Проверить наличие ПЛУ.
            if (!ActionCheckPluIsNew()) return;
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.ViewPlusNesting);
            // Навигация в контрол смены вложенности ПЛУ.
            WsFormNavigationUtils.NavigateToExistsPlusNesting(ShowFormUserControl);
        });
    }

    /// <summary>
    /// Загрузить WinForms-контрол смены вложенности ПЛУ.
    /// </summary>
    private void LoadNavigationPlusNesting()
    {
        if (WsFormNavigationUtils.IsLoadPlusNesting) return;
        WsFormNavigationUtils.IsLoadPlusNesting = true;

        WsFormNavigationUtils.PlusNestingUserControl.SetupUserControl();
        WsFormNavigationUtils.PlusNestingUserControl.ViewModel.CmdCancel.AddAction(ReturnCancelFromPlusNesting);
        WsFormNavigationUtils.PlusNestingUserControl.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.PlusNestingUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.PlusNestingUserControl.ViewModel.CmdYes.AddAction(ReturnOkFromPlusNesting);
        WsFormNavigationUtils.PlusNestingUserControl.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.PlusNestingUserControl.ViewModel.CmdYes.AddAction(ActionFinally);
    }

    /// <summary>
    /// Возврат ОК из контрола смены вложенности ПЛУ.
    /// </summary>
    private void ReturnOkFromPlusNesting() =>
        LabelSession.SetViewPluNesting(((WsXamlPlusNestingViewModel)WsFormNavigationUtils.PlusNestingUserControl.ViewModel).PluNesting);

    /// <summary>
    /// Возврат Отмена из контрола смены вложенности ПЛУ.
    /// </summary>
    private void ReturnCancelFromPlusNesting()
    {
        //
    }

    #endregion
}
