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
        WsFormNavigationUtils.WaitUserControl.SetupUserConrol();
        WsFormNavigationUtils.WaitUserControl.ViewModel.CmdCustom.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.WaitUserControl.ViewModel.CmdCustom.AddAction(ActionFinally);
        // Настройка главной формы.
        CenterToScreen();
        this.SwitchResolution(Debug.IsDevelop ? WsEnumScreenResolution.Value1366x768 : WsEnumScreenResolution.FullScreen);
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
        WsFormNavigationUtils.DialogUserControl.SetupUserConrol();
        WsFormNavigationUtils.DialogUserControl.ViewModel.CmdOk.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DialogUserControl.ViewModel.CmdOk.AddAction(ActionFinally);
        WsFormNavigationUtils.DialogUserControl.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DialogUserControl.ViewModel.CmdYes.AddAction(ActionFinally);
        WsFormNavigationUtils.DialogUserControl.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DialogUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        // WinForms-контрол ввода цифр.
        WsFormNavigationUtils.DigitsUserControl.SetupUserConrol();
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
            $"{(LabelSession.Line.WorkShop is null ? LocaleCore.Table.FieldEmpty : LabelSession.Area.Name)}" +
            $"{Environment.NewLine}{LocaleCore.Table.Number}: {LabelSession.Line.Number} | {LabelSession.Line.Description}");
        // Задать текст вложенности ПЛУ.
        MdInvokeControl.SetText(ButtonPluNestingFk, LabelSession.ViewPluNesting.GetSmartName());
        // Задать текст веса тары.
        MdInvokeControl.SetText(fieldTareWeight, LabelSession.PluLine.IsNew
                ? $"0,000 {LocaleCore.Scales.WeightUnitKg}"
                : $"{LabelSession.ViewPluNesting.TareWeight:0.000} {LocaleCore.Scales.WeightUnitKg}");
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
            case WsXamlDialogUserControl dialogUserControl:
                dialogUserControl.SetupUserConrol();
                break;
            case WsXamlDigitsUserControl digitsUserControl:
                digitsUserControl.SetupUserConrol();
                break;
            case WsXamlLinesUserControl linesUserControl:
                linesUserControl.SetupUserConrol();
                break;
            case WsXamlKneadingUserControl kneadingUserControl:
                kneadingUserControl.SetupUserConrol();
                break;
            case WsXamlPlusLinesUserControl plusLinesUserControl:
                plusLinesUserControl.SetupUserConrol();
                break;
            case WsXamlPlusNestingUserControl plusNestingUserControl:
                plusNestingUserControl.SetupUserConrol();
                break;
            case WsXamlWaitUserControl waitUserControl:
                waitUserControl.SetupUserConrol();
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
                $"{LocaleCore.Scales.QuestionCloseApp}?", true, WsEnumLogType.Question, 
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
        WsFormNavigationUtils.NavigateToExistsWait(ShowFormUserControl, LocaleCore.Scales.AppExit, LocaleCore.Scales.AppExitDescription);
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
        ContextManager.ContextItem.SaveLogMemory(UserSession.PluginMemory.GetMemorySizeAppMb(), UserSession.PluginMemory.GetMemorySizeFreeMb());
        ContextManager.ContextItem.SaveLogInformation(
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
    /// <returns></returns>
    private bool ActionCheckPluIsNew()
    {
        if (LabelSession.PluLine.Plu.IsNew)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Table.FieldPluIsNotSelected);
            ContextManager.ContextItem.SaveLogError(LocaleCore.Table.FieldPluIsNotSelected);
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
                $"{LocaleCore.Scales.QuestionRunApp} ScalesTerminal?",
                true, WsEnumLogType.Question, WsEnumDialogType.CancelYes, new() { ActionFinally, ActionYes });
            void ActionYes()
            {
                // Запустить процесс.
                if (File.Exists(LocaleData.Paths.ScalesTerminal))
                {
                    UserSession.PluginMassa.Close();
                    WsProcHelper.Instance.Run(LocaleData.Paths.ScalesTerminal, string.Empty, false, ProcessWindowStyle.Normal, true);
                    PluginMassaExecute();
                    ActionFinally();
                }
                else
                {
                    // Навигация в контрол диалога Ок.
                    WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl,
                        LocaleCore.Scales.ProgramNotFound(LocaleData.Paths.ScalesTerminal), true,
                        WsEnumLogType.Error, WsEnumDialogType.Ok, new() { ActionFinally });
                    ContextManager.ContextItem.SaveLogError(LocaleCore.Scales.ProgramNotFound(LocaleData.Paths.ScalesTerminal));
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
                WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl, LocaleCore.Scales.PluNotSelectWeight,
                    true, WsEnumLogType.Warning, WsEnumDialogType.Ok, new() { ActionFinally });
                return;
            }
            if (!UserSession.PluginMassa.MassaDevice.IsOpenPort)
            {
                // Навигация в контрол диалога Ок.
                WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl, LocaleCore.Scales.MassaIsNotRespond,
                    true, WsEnumLogType.Warning, WsEnumDialogType.Ok, new() { ActionFinally });
                return;
            }
            // Навигация в контрол диалога Отмена/Да.
            WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl,
                LocaleCore.Scales.QuestionPerformOperation, true, WsEnumLogType.Question,
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
                if (!UserSession.CheckPrintIsConnectAndReadyTscMain(fieldWarning))
                    return;
            if (LabelSession.PluginPrintZebraMain is not null)
                if (!UserSession.CheckPrintIsConnectAndReadyZebraMain(fieldWarning))
                    return;
            // Проверить подключение и готовность транспортного принтера.
            if (LabelSession.Line.IsShipping && LabelSession.PluginPrintTscShipping is not null)
                if (!UserSession.CheckPrintIsConnectAndReadyTscShipping(fieldWarning))
                    return;
            if (LabelSession.Line.IsShipping && LabelSession.PluginPrintZebraShipping is not null)
                if (!UserSession.CheckPrintIsConnectAndReadyZebraShipping(fieldWarning))
                    return;
        }

        // Печать этикетки.
        UserSession.PrintLabel(fieldWarning, false);
    }

    #endregion
}