using Ws.StorageCore.Enums;
namespace ScalesUI.Forms;

public partial class MainForm
{
    private SqlContextItemHelper ContextItem => SqlContextItemHelper.Instance;
    
    #region Public and private methods
    
    private void SetupNavigationUserControl()
    {
        FormNavigationUtils.NavigationUserControl.Dock = DockStyle.Fill;
        FormNavigationUtils.NavigationUserControl.Visible = false;
        CenterToScreen();
        
        Controls.Add(FormNavigationUtils.NavigationUserControl);
        FormBorderStyle = FormBorderStyle.None;
        TopMost = !Debug.IsDevelop;
    }

    /// <summary>
    /// Загрузить WinForms-контролы.
    /// </summary>
    private void LoadNavigationUserControl()
    {
        // WinForms-контрол диалога.
        FormNavigationUtils.DialogUserControl.SetupUserControl();
        FormNavigationUtils.DialogUserControl.ViewModel.CmdOk.AddAction(FormNavigationUtils.ActionBackFromNavigation);
        FormNavigationUtils.DialogUserControl.ViewModel.CmdOk.AddAction(ActionFinally);
        FormNavigationUtils.DialogUserControl.ViewModel.CmdYes.AddAction(FormNavigationUtils.ActionBackFromNavigation);
        FormNavigationUtils.DialogUserControl.ViewModel.CmdYes.AddAction(ActionFinally);
        FormNavigationUtils.DialogUserControl.ViewModel.CmdCancel.AddAction(FormNavigationUtils.ActionBackFromNavigation);
        FormNavigationUtils.DialogUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        // WinForms-контрол ввода цифр.
        FormNavigationUtils.DigitsUserControl.SetupUserControl();
        FormNavigationUtils.DigitsUserControl.ViewModel.CmdCancel.AddAction(ReturnCancelFromPlusNesting);
        FormNavigationUtils.DigitsUserControl.ViewModel.CmdCancel.AddAction(FormNavigationUtils.ActionBackFromNavigation);
        FormNavigationUtils.DigitsUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        FormNavigationUtils.DigitsUserControl.ViewModel.CmdYes.AddAction(ReturnOkFromPlusNesting);
        FormNavigationUtils.DigitsUserControl.ViewModel.CmdYes.AddAction(FormNavigationUtils.ActionBackFromNavigation);
        FormNavigationUtils.DigitsUserControl.ViewModel.CmdYes.AddAction(ActionFinally);
    }

    /// <summary>
    /// Запустить опрос весов.
    /// </summary>
    private void PluginMassaExecute() => UserSession.PluginMassa.Execute();

    /// <summary>
    /// Финальное действие после выполнения дествия кнопок.
    /// </summary>
    private void ActionFinally()
    {
        MdInvokeControl.Select(ButtonPrint);
        // Задать текст линии.
        MdInvokeControl.SetText(ButtonLine, 
            $"{LabelSession.Area.Name}" +
            $"{Environment.NewLine}{LocaleCore.Table.Number}: {LabelSession.Line.Number} | {LabelSession.Line.Description}");
        // Задать текст вложенности ПЛУ.
        MdInvokeControl.SetText(ButtonPluNestingFk, LabelSession.ViewPluNesting.GetSmartName());
        // Задать текст веса тары.
        MdInvokeControl.SetText(fieldTareWeight, LabelSession.PluLine.IsNew
                ? $"0,000 {LocaleCore.LabelPrint.WeightUnitKg}"
                : $"{LabelSession.ViewPluNesting.TareWeight:0.000} {LocaleCore.LabelPrint.WeightUnitKg}");
        // Скрыть WinForms-контрол навигации.
        HideFormUserControl();
    }

    /// <summary>
    /// Отобразить WinForms-контрол навигации.
    /// </summary>
    private void ShowFormUserControl(FormBaseUserControl userControl, string title)
    {
        MdInvokeControl.SetVisible(layoutPanelMain, false);
        FormNavigationUtils.NavigationUserControl.SetTitle(title);
        MdInvokeControl.SetVisible(FormNavigationUtils.NavigationUserControl, true);
        userControl.Dock = DockStyle.Fill;
        MdInvokeControl.SetVisible(userControl, true);
        switch (userControl)
        {
            case XamlDialogUserControl dialogUserControl:
                dialogUserControl.SetupUserControl();
                break;
            case XamlDigitsUserControl digitsUserControl:
                digitsUserControl.SetupUserControl();
                break;
            case XamlLinesUserControl linesUserControl:
                linesUserControl.SetupUserControl();
                break;
            case XamlKneadingUserControl kneadingUserControl:
                kneadingUserControl.SetupUserControl();
                break;
            case XamlPlusLinesUserControl plusLinesUserControl:
                plusLinesUserControl.SetupUserControl();
                break;
            case XamlPlusNestingUserControl plusNestingUserControl:
                plusNestingUserControl.SetupUserControl();
                break;
            default:
                throw new ArgumentException(nameof(userControl));
        }
    }

    /// <summary>
    /// Скрыть WinForms-контрол навигации.
    /// </summary>
    private void HideFormUserControl()
    {
        FormNavigationUtils.ClearNewDialogs();
        MdInvokeControl.SetVisible(FormNavigationUtils.NavigationUserControl, false);
        MdInvokeControl.SetVisible(layoutPanelMain, true);
        layoutPanelMain.Refresh();
    }

    /// <summary>
    /// Закрыть программу.
    /// </summary>
    private void ActionClose(object sender, EventArgs e)
    {
        Close();
    }

    /// <summary>
    /// Закрытие программы.
    /// </summary>
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (IsMagicClose) return;
        FormNavigationUtils.ClearNewDialogs();
        FormNavigationUtils.ActionTryCatch(() =>
        {
            ResetWarning();
            // Навигация в контрол диалога Отмена/Да.
            FormNavigationUtils.NavigateToNewDialog(ShowFormUserControl,
                $"{LocaleCore.LabelPrint.QuestionCloseApp}?", false, LogTypeEnum.Info, 
                EnumDialogType.CancelYes, new() { ActionCloseCancel, ActionCloseYes});
            e.Cancel = true;
        });
    }

    /// <summary>
    /// Возврат Да из контрола закрытия.
    /// </summary>
    private void ActionCloseYes()
    {
        UserSession.StopwatchMain.Restart();
        ActionFinally();
        UserSession.PluginsClose();
        FontsSettings.Close();
        MouseUnsubscribe();
        // Логи.
        UserSession.StopwatchMain.Stop();
        ContextItem.SaveLogInformation(
            LocaleData.Program.IsClosed + Environment.NewLine + $"{LocaleData.Program.TimeSpent}: {UserSession.StopwatchMain.Elapsed}.");
        // Магический флаг.
        IsMagicClose = true;
        Close();
    }

    private void ActionCloseAfterNotLine()
    {
        UserSession.StopwatchMain.Restart();
        // Плагины.
        UserSession.PluginsClose();
        // Шрифты.
        FontsSettings.Close();
        // Логи.
        UserSession.StopwatchMain.Stop();
        ContextItem.SaveLogInformation(
        LocaleData.Program.IsClosed + Environment.NewLine + $"{LocaleData.Program.TimeSpent}: {UserSession.StopwatchMain.Elapsed}.");
        // Магический флаг.
        IsMagicClose = true;
        Close();
    }

    /// <summary>
    /// Возврат Отмена из контрола закрытия.
    /// </summary>
    private void ActionCloseCancel()
    {
        IsMagicClose = false;
        ActionFinally();
    }

    /// <summary>
    /// Проверить наличие ПЛУ.
    /// </summary>
    private bool ActionCheckPluIsNew()
    {
        if (!LabelSession.PluLine.Plu.IsNew)
            return true;
        MdInvokeControl.SetVisible(fieldWarning, true);
        MdInvokeControl.SetText(fieldWarning, LocaleCore.Table.FieldPluIsNotSelected);
        ContextItem.SaveLogWarning(LocaleCore.Table.FieldPluIsNotSelected);
        return false;
    }

    /// <summary>
    /// Запустить ПО.
    /// </summary>
    private void ActionScalesTerminal(object sender, EventArgs e)
    {
        FormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            ResetWarning();
            FormNavigationUtils.NavigateToNewDialog(ShowFormUserControl,
                $"{LocaleCore.LabelPrint.QuestionRunApp} ScalesTerminal?",
                false, LogTypeEnum.Info, EnumDialogType.CancelYes, new() { ActionFinally, ActionYes });
            void ActionYes()
            {
                if (File.Exists(LocalizationUtils.AppScalesTerminal))
                {
                    UserSession.PluginMassa.Dispose();
                    ProcHelper.Instance.Run(LocalizationUtils.AppScalesTerminal, string.Empty, false, ProcessWindowStyle.Normal, true);
                    PluginMassaExecute();
                    ActionFinally();
                }
                else
                {
                    FormNavigationUtils.NavigateToNewDialog(ShowFormUserControl,
                        LocaleCore.LabelPrint.ProgramNotFound(LocalizationUtils.AppScalesTerminal), true,
                        LogTypeEnum.Error, EnumDialogType.Ok, new() { ActionFinally });
                    ContextItem.SaveLogError(LocaleCore.LabelPrint.ProgramNotFound(LocalizationUtils.AppScalesTerminal));
                }
            }
        });
    }
    
    private void ActionNewPallet(object sender, EventArgs e)
    {
        FormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            LabelSession.NewPallet();
        });
    }
    
    private void ResetWarning()
    {
        MdInvokeControl.SetText(fieldWarning, string.Empty);
        MdInvokeControl.SetVisible(fieldWarning, false);
    }
    
    private void ActionPreparePrint(object sender, EventArgs e)
    {
        FormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            ResetWarning();
            if (!UserSession.CheckPluIsEmpty(fieldWarning)) return;
            if (!UserSession.CheckViewPluNesting(LabelSession.PluLine.Plu, fieldWarning)) return;
            if (!UserSession.CheckWeightMassaDeviceExists()) return;
            if (!UserSession.CheckWeightMassaIsStable(fieldWarning)) return;
            if (!UserSession.CheckWeight(fieldWarning)) { ActionFinally(); return; }
            UserSession.NewPluWeighing();
            ActionPrintLabel();
        });
    }
    
    private void ActionPrintLabel()
    {
        if (LabelSession.PluginPrintTscMain is not null)
            if (!PrintSession.CheckPrintIsConnectAndReadyTscMain(fieldWarning))
                return;
        if (LabelSession.PluginPrintZebraMain is not null)
            if (!PrintSession.CheckPrintIsConnectAndReadyZebraMain(fieldWarning))
                return;
        PrintSession.PrintLabel(fieldWarning, false);
    }

    #endregion
}
