namespace ScalesUI.Forms;

public partial class MainForm
{
    #region Public and private methods - Возврат из контролов

    /// <summary>
    /// Сменить вложенность ПЛУ.
    /// </summary>
    private void ActionSwitchPluNesting(object sender, EventArgs e)
    {
        // Загрузить WinForms-контрол смены вложенности ПЛУ.
        LoadNavigationPlusNesting();
        FormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            // Проверить наличие ПЛУ.
            if (!ActionCheckPluIsNew()) return;
            // Обновить кэш.
            ContextCache.Load(SqlEnumTableName.ViewPlusNesting);
            // Навигация в контрол смены вложенности ПЛУ.
            FormNavigationUtils.NavigateToExistsPlusNesting(ShowFormUserControl);
        });
    }

    /// <summary>
    /// Загрузить WinForms-контрол смены вложенности ПЛУ.
    /// </summary>
    private void LoadNavigationPlusNesting()
    {
        if (FormNavigationUtils.IsLoadPlusNesting) return;
        FormNavigationUtils.IsLoadPlusNesting = true;

        FormNavigationUtils.PlusNestingUserControl.SetupUserControl();
        FormNavigationUtils.PlusNestingUserControl.ViewModel.CmdCancel.AddAction(ReturnCancelFromPlusNesting);
        FormNavigationUtils.PlusNestingUserControl.ViewModel.CmdCancel.AddAction(FormNavigationUtils.ActionBackFromNavigation);
        FormNavigationUtils.PlusNestingUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        FormNavigationUtils.PlusNestingUserControl.ViewModel.CmdYes.AddAction(ReturnOkFromPlusNesting);
        FormNavigationUtils.PlusNestingUserControl.ViewModel.CmdYes.AddAction(FormNavigationUtils.ActionBackFromNavigation);
        FormNavigationUtils.PlusNestingUserControl.ViewModel.CmdYes.AddAction(ActionFinally);
    }

    /// <summary>
    /// Возврат ОК из контрола смены вложенности ПЛУ.
    /// </summary>
    private void ReturnOkFromPlusNesting() =>
        LabelSession.SetViewPluNesting(FormNavigationUtils.PlusNestingUserControl.ViewModel.PluNesting);

    /// <summary>
    /// Возврат Отмена из контрола смены вложенности ПЛУ.
    /// </summary>
    private void ReturnCancelFromPlusNesting()
    {
        //
    }

    #endregion
}
