namespace ScalesUI.Forms;

public partial class WsMainForm
{
    #region Public and private methods
    
    private void SetupNavigationUserControl()
    {
        WsFormNavigationUtils.NavigationUserControl.Dock = DockStyle.Fill;
        WsFormNavigationUtils.NavigationUserControl.Visible = false;
        CenterToScreen();
        ReturnOkFromDeviceSettings();
        
        Controls.Add(WsFormNavigationUtils.NavigationUserControl);
        FormBorderStyle = FormBorderStyle.None;
        TopMost = !Debug.IsDevelop;
    }

    /// <summary>
    /// Загрузить WinForms-контролы.
    /// </summary>
    private void LoadNavigationUserControl()
    {
        // WinForms-контрол диалога.
        WsFormNavigationUtils.DialogUserControl.SetupUserControl();
        WsFormNavigationUtils.DialogUserControl.ViewModel.CmdOk.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DialogUserControl.ViewModel.CmdOk.AddAction(ActionFinally);
        WsFormNavigationUtils.DialogUserControl.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DialogUserControl.ViewModel.CmdYes.AddAction(ActionFinally);
        WsFormNavigationUtils.DialogUserControl.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DialogUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        // WinForms-контрол ввода цифр.
        WsFormNavigationUtils.DigitsUserControl.SetupUserControl();
        WsFormNavigationUtils.DigitsUserControl.ViewModel.CmdCancel.AddAction(ReturnCancelFromPlusNesting);
        WsFormNavigationUtils.DigitsUserControl.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DigitsUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.DigitsUserControl.ViewModel.CmdYes.AddAction(ReturnOkFromPlusNesting);
        WsFormNavigationUtils.DigitsUserControl.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DigitsUserControl.ViewModel.CmdYes.AddAction(ActionFinally);
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
        // Выделить кнопку печати.
        MdInvokeControl.Select(ButtonPrint);
        // Задать текст линии.
        MdInvokeControl.SetText(ButtonLine, 
            $"{LabelSession.Area.Name}" +
            $"{Environment.NewLine}{WsLocaleCore.Table.Number}: {LabelSession.Line.Number} | {LabelSession.Line.Description}");
        // Задать текст вложенности ПЛУ.
        MdInvokeControl.SetText(ButtonPluNestingFk, LabelSession.ViewPluNesting.GetSmartName());
        // Задать текст веса тары.
        MdInvokeControl.SetText(fieldTareWeight, LabelSession.PluLine.IsNew
                ? $"0,000 {WsLocaleCore.LabelPrint.WeightUnitKg}"
                : $"{LabelSession.ViewPluNesting.TareWeight:0.000} {WsLocaleCore.LabelPrint.WeightUnitKg}");
        // Скрыть WinForms-контрол навигации.
        HideFormUserControl();
    }

    /// <summary>
    /// Отобразить WinForms-контрол навигации.
    /// </summary>
    private void ShowFormUserControl(WsFormBaseUserControl userControl, string title)
    {
        MdInvokeControl.SetVisible(layoutPanelMain, false);
        WsFormNavigationUtils.NavigationUserControl.SetTitle(title);
        MdInvokeControl.SetVisible(WsFormNavigationUtils.NavigationUserControl, true);
        userControl.Dock = DockStyle.Fill;
        MdInvokeControl.SetVisible(userControl, true);
        switch (userControl)
        {
            case WsXamlDeviceSettingsUserControl deviceSettingsUserControl:
                deviceSettingsUserControl.SetupUserControl();
                break;
            case WsXamlDialogUserControl dialogUserControl:
                dialogUserControl.SetupUserControl();
                break;
            case WsXamlDigitsUserControl digitsUserControl:
                digitsUserControl.SetupUserControl();
                break;
            case WsXamlLinesUserControl linesUserControl:
                linesUserControl.SetupUserControl();
                break;
            case WsXamlKneadingUserControl kneadingUserControl:
                kneadingUserControl.SetupUserControl();
                break;
            case WsXamlPlusLinesUserControl plusLinesUserControl:
                plusLinesUserControl.SetupUserControl();
                break;
            case WsXamlPlusNestingUserControl plusNestingUserControl:
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
        WsFormNavigationUtils.ClearNewDialogs();
        MdInvokeControl.SetVisible(WsFormNavigationUtils.NavigationUserControl, false);
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
        WsFormNavigationUtils.ClearNewDialogs();
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            ResetWarning();
            // Навигация в контрол диалога Отмена/Да.
            WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl,
                $"{WsLocaleCore.LabelPrint.QuestionCloseApp}?", true, WsEnumLogType.Question, 
                WsEnumDialogType.CancelYes, new() { ActionCloseCancel, ActionCloseYes});
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
        WsScheduler.Close();
        UserSession.PluginsClose();
        FontsSettings.Close();
        MouseUnsubscribe();
        // Логи.
        UserSession.StopwatchMain.Stop();
        ContextManager.LogMemoryRepository.Save(UserSession.PluginMemory.GetMemorySizeAppMb(), UserSession.PluginMemory.GetMemorySizeFreeMb());
        ContextManager.ContextItem.SaveLogInformation(
            WsLocaleData.Program.IsClosed + Environment.NewLine + $"{WsLocaleData.Program.TimeSpent}: {UserSession.StopwatchMain.Elapsed}.");
        // Магический флаг.
        IsMagicClose = true;
        Close();
    }

    private void ActionCloseAfterNotLine()
    {
        UserSession.StopwatchMain.Restart();
        // Планировщик.
        WsScheduler.Close();
        // Плагины.
        UserSession.PluginsClose();
        // Шрифты.
        FontsSettings.Close();
        // Логи.
        UserSession.StopwatchMain.Stop();
        ContextManager.LogMemoryRepository.Save(UserSession.PluginMemory.GetMemorySizeAppMb(), UserSession.PluginMemory.GetMemorySizeFreeMb());
        ContextManager.ContextItem.SaveLogInformation(
        WsLocaleData.Program.IsClosed + Environment.NewLine + $"{WsLocaleData.Program.TimeSpent}: {UserSession.StopwatchMain.Elapsed}.");
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
        if (LabelSession.PluLine.Plu.IsNew)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, WsLocaleCore.Table.FieldPluIsNotSelected);
            ContextManager.ContextItem.SaveLogWarning(WsLocaleCore.Table.FieldPluIsNotSelected);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Запустить ПО.
    /// </summary>
    private void ActionScalesTerminal(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            ResetWarning();
            WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl,
                $"{WsLocaleCore.LabelPrint.QuestionRunApp} ScalesTerminal?",
                true, WsEnumLogType.Question, WsEnumDialogType.CancelYes, new() { ActionFinally, ActionYes });
            void ActionYes()
            {
                if (File.Exists(WsLocalizationUtils.AppScalesTerminal))
                {
                    UserSession.PluginMassa.Dispose();
                    WsProcHelper.Instance.Run(WsLocalizationUtils.AppScalesTerminal, string.Empty, false, ProcessWindowStyle.Normal, true);
                    PluginMassaExecute();
                    ActionFinally();
                }
                else
                {
                    WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl,
                        WsLocaleCore.LabelPrint.ProgramNotFound(WsLocalizationUtils.AppScalesTerminal), true,
                        WsEnumLogType.Error, WsEnumDialogType.Ok, new() { ActionFinally });
                    ContextManager.ContextItem.SaveLogError(WsLocaleCore.LabelPrint.ProgramNotFound(WsLocalizationUtils.AppScalesTerminal));
                }
            }
        });
    }
    
    private void ActionNewPallet(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
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
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
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
