// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Forms;

public partial class WsMainForm
{
    #region Public and private methods

    /// <summary>
    /// Загрузить WinForms-контрол ожидания.
    /// </summary>
    private void LoadNavigationWaitUserControl()
    {
        // Навигация.
        WsFormNavigationUtils.NavigationUserControl.Dock = DockStyle.Fill;
        WsFormNavigationUtils.NavigationUserControl.Visible = false;
        // WinForms-контрол ожидания.
        WsFormNavigationUtils.WaitUserControl.SetupUserControl();
        WsFormNavigationUtils.WaitUserControl.ViewModel.CmdCustom.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.WaitUserControl.ViewModel.CmdCustom.AddAction(ActionFinally);
        // Настройка главной формы.
        CenterToScreen();
        // Применить настройки устройства.
        ReturnOkFromDeviceSettings();
        // Добавить контрол.
        Controls.Add(WsFormNavigationUtils.NavigationUserControl);
        // Настройки главной формы.
        FormBorderStyle = Debug.IsDevelop ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
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
            $"{(LabelSession.Line.WorkShop is null ? WsLocaleCore.Table.FieldEmpty : LabelSession.Area.Name)}" +
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
            case WsXamlWaitUserControl waitUserControl:
                waitUserControl.SetupUserControl();
                break;
            default:
                throw new ArgumentException(nameof(userControl));
        }
        // Приводит к задвоенному вызову MouseDownExt.
        //System.Windows.Forms.Application.DoEvents();
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
        // Приводит к задвоенному вызову MouseDownExt.
        //System.Windows.Forms.Application.DoEvents();
    }

    /// <summary>
    /// Закрыть программу.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionClose(object sender, EventArgs e)
    {
        // Redirect into MainForm_FormClosing.
        Close();
    }

    /// <summary>
    /// Закрытие программы.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (IsMagicClose) return;
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            // Сброс предупреждения.
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
        ActionFinally();
        UserSession.StopwatchMain.Restart();
        // Навигация в контрол ожидания.
        WsFormNavigationUtils.NavigateToExistsWait(ShowFormUserControl, WsLocaleCore.LabelPrint.AppExit, WsLocaleCore.LabelPrint.AppExitDescription);
        // Планировщик.
        WsScheduler.Close();
        // Плагины.
        UserSession.PluginsClose();
        // Шрифты.
        FontsSettings.Close();
        // Завершить хуки мышки.
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
    /// <returns></returns>
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
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionScalesTerminal(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            // Навигация в контрол диалога Отмена/Да.
            WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl,
                $"{WsLocaleCore.LabelPrint.QuestionRunApp} ScalesTerminal?",
                true, WsEnumLogType.Question, WsEnumDialogType.CancelYes, new() { ActionFinally, ActionYes });
            void ActionYes()
            {
                // Запустить процесс.
                if (File.Exists(WsLocalizationUtils.AppScalesTerminal))
                {
                    UserSession.PluginMassa.Close();
                    WsProcHelper.Instance.Run(WsLocalizationUtils.AppScalesTerminal, string.Empty, false, ProcessWindowStyle.Normal, true);
                    PluginMassaExecute();
                    ActionFinally();
                }
                else
                {
                    // Навигация в контрол диалога Ок.
                    WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl,
                        WsLocaleCore.LabelPrint.ProgramNotFound(WsLocalizationUtils.AppScalesTerminal), true,
                        WsEnumLogType.Error, WsEnumDialogType.Ok, new() { ActionFinally });
                    ContextManager.ContextItem.SaveLogError(WsLocaleCore.LabelPrint.ProgramNotFound(WsLocalizationUtils.AppScalesTerminal));
                }
            }
        });
    }

    /// <summary>
    /// Инициализировать весовую платформу Масса-К.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionScalesInit(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            if (!LabelSession.PluLine.Plu.IsCheckWeight)
            {
                // Навигация в контрол диалога Ок.
                WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl, WsLocaleCore.LabelPrint.PluNotSelectWeight,
                    true, WsEnumLogType.Warning, WsEnumDialogType.Ok, new() { ActionFinally });
                return;
            }
            if (!UserSession.PluginMassa.MassaDevice.IsOpenPort)
            {
                // Навигация в контрол диалога Ок.
                WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl, WsLocaleCore.LabelPrint.MassaIsNotRespond,
                    true, WsEnumLogType.Warning, WsEnumDialogType.Ok, new() { ActionFinally });
                return;
            }
            // Навигация в контрол диалога Отмена/Да.
            WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl,
                WsLocaleCore.LabelPrint.QuestionPerformOperation, true, WsEnumLogType.Question,
                WsEnumDialogType.CancelYes, new() { () => { }, ActionYes });
            void ActionYes()
            {
                // Fix negative weight.
                //if (UserSession.PluginMassa.WeightNet < 0)
                //{
                //    UserSession.PluginMassa.ResetMassa();
                //}
                // Проверить наличие весовой платформы Масса-К.
                if (!UserSession.CheckWeightMassaDeviceExists()) return;
                LabelSession.SetPluLine();

                PluginMassaExecute();
                UserSession.PluginMassa.GetInit();
            }
        });
    }

    /// <summary>
    /// Новая паллета.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionNewPallet(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            LabelSession.NewPallet();
        });
    }

    /// <summary>
    /// Сброс предупреждения.
    /// </summary>
    private void ResetWarning()
    {
        MdInvokeControl.SetText(fieldWarning, string.Empty);
        MdInvokeControl.SetVisible(fieldWarning, false);
    }

    /// <summary>
    /// Подготовка к печати.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionPreparePrint(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            // Проверить наличие ПЛУ.
            if (!UserSession.CheckPluIsEmpty(fieldWarning)) return;
            // Проверить наличие вложенности ПЛУ.
            if (!UserSession.CheckViewPluNesting(LabelSession.PluLine.Plu, fieldWarning)) return;
            // Проверить наличие весовой платформы Масса-К.
            if (!UserSession.CheckWeightMassaDeviceExists()) return;
            // Проверить стабилизацию весовой платформы Масса-К.
            if (!UserSession.CheckWeightMassaIsStable(fieldWarning)) return;
            // Проверить ГТИН ПЛУ.
            if (!UserSession.CheckPluGtin(fieldWarning)) return;
            // Использовать фейк-данные для веса ПЛУ.
            //UserSession.SetPluWeighingFakeForDevelop(ShowFormUserControl, ActionPreparePrintStep2);
            // Проверить отрицательный вес.
            if (!UserSession.CheckWeightIsNegative(fieldWarning)) { ActionFinally(); return; }
            // Создать новое взвешивание ПЛУ.
            UserSession.NewPluWeighing();
            // Проверить границы веса.
            if (!UserSession.CheckWeightThresholds(fieldWarning)) { ActionFinally(); return; }
            // Печать этикетки.
            ActionPrintLabel(true);
        });
    }

    /// <summary>
    /// Печать этикетки.
    /// </summary>
    /// <param name="isCheckPrinter">Проверить подключение и готовность принтера</param>
    /// <returns></returns>
    private void ActionPrintLabel(bool isCheckPrinter)
    {
        if (isCheckPrinter)
        {
            // Проверить подключение и готовность основного принтера.
            if (LabelSession.PluginPrintTscMain is not null)
                if (!PrintSession.CheckPrintIsConnectAndReadyTscMain(fieldWarning))
                    return;
            if (LabelSession.PluginPrintZebraMain is not null)
                if (!PrintSession.CheckPrintIsConnectAndReadyZebraMain(fieldWarning))
                    return;
            // Проверить подключение и готовность транспортного принтера.
            if (LabelSession.Line.IsShipping && LabelSession.PluginPrintTscShipping is not null)
                if (!PrintSession.CheckPrintIsConnectAndReadyTscShipping(fieldWarning))
                    return;
            if (LabelSession.Line.IsShipping && LabelSession.PluginPrintZebraShipping is not null)
                if (!PrintSession.CheckPrintIsConnectAndReadyZebraShipping(fieldWarning))
                    return;
        }

        // Печать этикетки.
        PrintSession.PrintLabel(fieldWarning, false);
    }

    #endregion
}
