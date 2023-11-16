namespace ScalesUI.Forms;

public partial class MainForm
{
    #region Public and private methods - Возврат из контролов

    /// <summary>
    /// Сменить ПЛУ линии.
    /// </summary>
    private void ActionSwitchPluLine(object sender, EventArgs e)
    {
        // Загрузить WinForms-контрол смены ПЛУ линии.
        LoadNavigationPlusLine();
        FormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            MdInvokeControl.SetVisible(labelNettoWeight, false);
            MdInvokeControl.SetVisible(fieldNettoWeight, false);
            LabelSession.SetPluLine();
            // Навигация в контрол смены ПЛУ линии.
            FormNavigationUtils.NavigateToExistsPlusLine(ShowFormUserControl);
        });
    }
    
    /// <summary>
     /// Загрузить WinForms-контрол смены ПЛУ линии.
     /// </summary>
    private void LoadNavigationPlusLine()
    {
        if (FormNavigationUtils.IsLoadPlusLine) return;
        FormNavigationUtils.IsLoadPlusLine = true;

        FormNavigationUtils.PlusLineUserControl.SetupUserControl();
        FormNavigationUtils.PlusLineUserControl.ViewModel.CmdCancel.AddAction(ReturnCancelFromPlusLine);
        FormNavigationUtils.PlusLineUserControl.ViewModel.CmdCancel.AddAction(FormNavigationUtils.ActionBackFromNavigation);
        FormNavigationUtils.PlusLineUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        FormNavigationUtils.PlusLineUserControl.ViewModel.CmdYes.AddAction(ReturnOkFromPlusLine);
        FormNavigationUtils.PlusLineUserControl.ViewModel.CmdYes.AddAction(FormNavigationUtils.ActionBackFromNavigation);
        FormNavigationUtils.PlusLineUserControl.ViewModel.CmdYes.AddAction(() => { ActionKneading(null, null); });
    }

    /// <summary>
    /// Возврат ОК из контрола смены ПЛУ.
    /// </summary>
    private void ReturnOkFromPlusLine()
    {
        LabelSession.SetPluLine(FormNavigationUtils.PlusLineUserControl.ViewModel.PluLine);

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
