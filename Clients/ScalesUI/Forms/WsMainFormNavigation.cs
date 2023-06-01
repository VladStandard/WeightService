// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Forms;

public partial class WsMainForm
{
    #region Public and private methods - Возврат из контролов

    /// <summary>
    /// Загрузка WinForms-контрола ожидания.
    /// </summary>
    private void LoadNavigationWaitUserControl()
    {
        // Навигация.
        WsFormNavigationUtils.NavigationUserControl.Dock = DockStyle.Fill;
        WsFormNavigationUtils.NavigationUserControl.Visible = false;
        // WinForms-контрол ожидания.
        WsFormNavigationUtils.WaitUserControl.SetupUserConrol();
        WsFormNavigationUtils.WaitUserControl.Page.ViewModel.CmdCustom.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.WaitUserControl.Page.ViewModel.CmdCustom.AddAction(ActionFinally);
        //WsFormNavigationUtils.WaitUserControl.Page.ViewModel.UpdateCommandsFromActions();
        // Настройка главной формы.
        this.SwitchResolution(Debug.IsDevelop ? WsEnumScreenResolution.Value1366x768 : WsEnumScreenResolution.FullScreen);
        CenterToScreen();
        // Добавить контрол.
        Controls.Add(WsFormNavigationUtils.NavigationUserControl);
        // Настройки главной формы.
        FormBorderStyle = Debug.IsDevelop ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
        TopMost = !Debug.IsDevelop;
    }

    /// <summary>
    /// Загрузка WinForms-контролов.
    /// </summary>
    private void LoadNavigationUserControl()
    {
        // WinForms-контрол диалога.
        WsFormNavigationUtils.DialogUserControl.SetupUserConrol();
        WsFormNavigationUtils.DialogUserControl.Page.ViewModel.CmdOk.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DialogUserControl.Page.ViewModel.CmdOk.AddAction(ActionFinally);
        WsFormNavigationUtils.DialogUserControl.Page.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DialogUserControl.Page.ViewModel.CmdYes.AddAction(ActionFinally);
        WsFormNavigationUtils.DialogUserControl.Page.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DialogUserControl.Page.ViewModel.CmdCancel.AddAction(ActionFinally);
        // WinForms-контрол ввода цифр.
        WsFormNavigationUtils.DigitsUserControl.SetupUserConrol();
        WsFormNavigationUtils.DigitsUserControl.Page.ViewModel.CmdCancel.AddAction(ReturnCancelFromPlusNesting);
        WsFormNavigationUtils.DigitsUserControl.Page.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DigitsUserControl.Page.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.DigitsUserControl.Page.ViewModel.CmdYes.AddAction(ReturnOkFromPlusNesting);
        WsFormNavigationUtils.DigitsUserControl.Page.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DigitsUserControl.Page.ViewModel.CmdYes.AddAction(ActionFinally);
        // WinForms-контрол замеса.
        WsFormNavigationUtils.KneadingUserControl.SetupUserConrol();
        WsFormNavigationUtils.KneadingUserControl.Page.ViewModel.CmdCancel.AddAction(ReturnFromKneading);
        WsFormNavigationUtils.KneadingUserControl.Page.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.KneadingUserControl.Page.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.KneadingUserControl.Page.ViewModel.CmdYes.AddAction(ReturnFromKneading);
        WsFormNavigationUtils.KneadingUserControl.Page.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.KneadingUserControl.Page.ViewModel.CmdYes.AddAction(ActionFinally);
        // WinForms-контрол смены линии.
        WsFormNavigationUtils.LinesUserControl.SetupUserConrol();
        WsFormNavigationUtils.LinesUserControl.Page.ViewModel.CmdCancel.AddAction(ReturnCancelFromLines);
        WsFormNavigationUtils.LinesUserControl.Page.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.LinesUserControl.Page.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.LinesUserControl.Page.ViewModel.CmdYes.AddAction(ReturnOkFromLines);
        WsFormNavigationUtils.LinesUserControl.Page.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.LinesUserControl.Page.ViewModel.CmdYes.AddAction(ActionFinally);
        // WinForms-контрол смены ПЛУ.
        WsFormNavigationUtils.PlusLineUserControl.SetupUserConrol();
        WsFormNavigationUtils.PlusLineUserControl.Page.ViewModel.CmdCancel.AddAction(ReturnCancelFromPlusLine);
        WsFormNavigationUtils.PlusLineUserControl.Page.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.PlusLineUserControl.Page.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.PlusLineUserControl.Page.ViewModel.CmdYes.AddAction(ReturnOkFromPlusLine);
        WsFormNavigationUtils.PlusLineUserControl.Page.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.PlusLineUserControl.Page.ViewModel.CmdYes.AddAction(ActionFinally);
        // WinForms-контрол смены вложенности ПЛУ.
        WsFormNavigationUtils.PlusNestingUserControl.SetupUserConrol();
        WsFormNavigationUtils.PlusNestingUserControl.Page.ViewModel.CmdCancel.AddAction(ReturnCancelFromPlusNesting);
        WsFormNavigationUtils.PlusNestingUserControl.Page.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.PlusNestingUserControl.Page.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.PlusNestingUserControl.Page.ViewModel.CmdYes.AddAction(ReturnOkFromPlusNesting);
        WsFormNavigationUtils.PlusNestingUserControl.Page.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.PlusNestingUserControl.Page.ViewModel.CmdYes.AddAction(ActionFinally);
    }

    /// <summary>
    /// Возврат из контрола смены замеса.
    /// </summary>
    private void ReturnFromKneading()
    {
        using WsFormDigitsForm numberInputForm = new() { InputValue = 0 };
        DialogResult result = numberInputForm.ShowDialog(this);
        numberInputForm.Close();
        if (result == DialogResult.OK)
            LabelSession.WeighingSettings.Kneading = (byte)numberInputForm.InputValue;
        UserSession.PluginMassa.Execute();
    }

    /// <summary>
    /// Возврат из контрола смены ещё.
    /// </summary>
    private void ReturnFromMore() => UserSession.PluginMassa.Execute();

    /// <summary>
    /// Возврат ОК из контрола смены ПЛУ.
    /// </summary>
    private void ReturnOkFromPlusLine()
    {
        LabelSession.SetPluLine(((WsXamlPlusLineViewModel)WsFormNavigationUtils.PlusLineUserControl.Page.ViewModel).PluLine);

        LabelSession.WeighingSettings.Kneading = 1;
        LabelSession.ProductDate = DateTime.Now;
        LabelSession.NewPallet();
        MdInvokeControl.SetVisible(labelNettoWeight, LabelSession.PluLine.Plu.IsCheckWeight);
        MdInvokeControl.SetVisible(fieldNettoWeight, LabelSession.PluLine.Plu.IsCheckWeight);
        ActionKneading(null, null);
    }

    /// <summary>
    /// Возврат Отмена из контрола смены ПЛУ.
    /// </summary>
    private void ReturnCancelFromPlusLine() => LabelSession.SetPluLine();

    /// <summary>
    /// Возврат ОК из контрола смены вложенности ПЛУ.
    /// </summary>
    private void ReturnOkFromPlusNesting() =>
        LabelSession.SetViewPluNesting(((WsXamlPlusNestingViewModel)WsFormNavigationUtils.PlusNestingUserControl.Page.ViewModel).PluNesting);

    /// <summary>
    /// Возврат Отмена из контрола смены вложенности ПЛУ.
    /// </summary>
    private void ReturnCancelFromPlusNesting()
    {
        //
    }

    /// <summary>
    /// Возврат ОК из контрола смены линии.
    /// </summary>
    private void ReturnOkFromLines()
    {
        LabelSession.SetSessionForLabelPrint(ShowFormUserControl,
            ((WsXamlLinesViewModel)WsFormNavigationUtils.LinesUserControl.Page.ViewModel).Line.IdentityValueId,
            ((WsXamlLinesViewModel)WsFormNavigationUtils.LinesUserControl.Page.ViewModel).Area);
        ActionKneading(null, null);
    }

    /// <summary>
    /// Возврат Отмена из контрола смены линии.
    /// </summary>
    private void ReturnCancelFromLines()
    {
        ActionKneading(null, null);
    }

    #endregion

    #region Public and private methods - действия

    private void ActionFinally()
    {
        MdInvokeControl.Select(ButtonPrint);
        // LoadLocalizationDynamic(Lang.Russian);
        LocaleCore.Lang = LocaleData.Lang = Lang.Russian;
        string area = LabelSession.Line.WorkShop is null
            ? LocaleCore.Table.FieldEmpty : LabelSession.Area.Name;
        MdInvokeControl.SetText(ButtonLine, $"{area}{Environment.NewLine}{LocaleCore.Table.Number}: {LabelSession.Line.Number} | {LabelSession.Line.Description}");
        MdInvokeControl.SetText(ButtonPluNestingFk, LabelSession.ViewPluNesting.GetSmartName());
        MdInvokeControl.SetText(fieldPackageWeight, LabelSession.PluLine.IsNotNew
                ? $"{LabelSession.ViewPluNesting.TareWeight:0.000} {LocaleCore.Scales.WeightUnitKg}"
                : $"0,000 {LocaleCore.Scales.WeightUnitKg}");
        // Скрыть навигацию.
        HideFormUserControl();
    }

    /// <summary>
    /// Отобразить контрол.
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
        System.Windows.Forms.Application.DoEvents();
    }

    /// <summary>
    /// Скрыть контрол.
    /// </summary>
    private void HideFormUserControl()
    {
        MdInvokeControl.SetVisible(WsFormNavigationUtils.NavigationUserControl, false);
        MdInvokeControl.SetVisible(layoutPanelMain, true);
        layoutPanelMain.Refresh();
        System.Windows.Forms.Application.DoEvents();
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
            WsFormNavigationUtils.NavigateToMessageUserControlCancelYes(ShowFormUserControl,
                $"{LocaleCore.Scales.QuestionCloseApp}?", true, WsEnumLogType.Question,
                ActionCloseCancel, ActionCloseYes);
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
        WsFormNavigationUtils.NavigateToWaitUserControl(ShowFormUserControl, LocaleCore.Scales.AppExit, LocaleCore.Scales.AppExitDescription);
        // Планировщик.
        WsScheduler.Close();
        // Плагины.
        UserSession.PluginsClose();
        // Шрифты.
        FontsSettings.Close();
        // Хуки мышки.
        KeyboardMouseEvents.MouseDownExt -= MouseDownExt;
        KeyboardMouseEvents.Dispose();
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
    /// Сменить линию.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionSwitchLine(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            // Обновить кэш.
            ContextCache.Load(WsSqlTableName.Areas);
            ContextCache.Load(WsSqlTableName.Lines);
            // Навигация в контрол линии.
            WsFormNavigationUtils.NavigateToLinesUserControl(ShowFormUserControl);
        });
    }

    /// <summary>
    /// Проверить наличие ПЛУ.
    /// </summary>
    /// <returns></returns>
    private bool ActionCheckPluIdentityIsNew()
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
            WsFormNavigationUtils.NavigateToMessageUserControlCancelYes(ShowFormUserControl,
                $"{LocaleCore.Scales.QuestionRunApp} ScalesTerminal?",
                true, WsEnumLogType.Question, () => { }, ActionYes);
            void ActionYes()
            {
                // Run app.
                if (File.Exists(LocaleData.Paths.ScalesTerminal))
                {
                    UserSession.PluginMassa.Close();
                    Proc.Run(LocaleData.Paths.ScalesTerminal, string.Empty, false, ProcessWindowStyle.Normal, true);
                    UserSession.PluginMassa.Execute();
                }
                else
                {
                    // Навигация в контрол диалога Ок.
                    WsFormNavigationUtils.NavigateToMessageUserControlOk(ShowFormUserControl,
                        LocaleCore.Scales.ProgramNotFound(LocaleData.Paths.ScalesTerminal), true,
                        WsEnumLogType.Error);
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
                WsFormNavigationUtils.NavigateToMessageUserControlOk(ShowFormUserControl, LocaleCore.Scales.PluNotSelectWeight,
                    true, WsEnumLogType.Warning);
                return;
            }
            if (!UserSession.PluginMassa.MassaDevice.IsOpenPort)
            {
                // Навигация в контрол диалога Ок.
                WsFormNavigationUtils.NavigateToMessageUserControlOk(ShowFormUserControl, LocaleCore.Scales.MassaIsNotRespond,
                    true, WsEnumLogType.Warning);
                return;
            }
            // Навигация в контрол диалога Отмена/Да.
            WsFormNavigationUtils.NavigateToMessageUserControlCancelYes(ShowFormUserControl,
                LocaleCore.Scales.QuestionPerformOperation, true, WsEnumLogType.Question, () => { }, ActionYes);
            void ActionYes()
            {
                // Fix negative weight.
                //if (UserSession.PluginMassa.WeightNet < 0)
                //{
                //    UserSession.PluginMassa.ResetMassa();
                //}
                // Проверить наличие весовой платформы Масса-К.
                if (IsSkipDialogs || Debug.IsRelease)
                    UserSession.CheckWeightMassaDeviceExists();
                LabelSession.SetPluLine();

                UserSession.PluginMassa.Execute();
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
    /// Сменить ПЛУ.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionSwitchPlu(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            MdInvokeControl.SetVisible(labelNettoWeight, false);
            MdInvokeControl.SetVisible(fieldNettoWeight, false);
            LabelSession.SetPluLine();
            // Навигация в контрол смены ПЛУ линии.
            WsFormNavigationUtils.NavigateToPlusLineUserControl(ShowFormUserControl);
        });
    }

    /// <summary>
    /// Сменить замес.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionKneading(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            if (LabelSession.PluLine.IsNew)
            {
                // Навигация в контрол диалога Ок.
                WsFormNavigationUtils.NavigateToMessageUserControlOk(ShowFormUserControl, LocaleCore.Scales.PluNotSelect,
                    true, WsEnumLogType.Warning);
                return;
            }
            // Сброс предупреждения.
            ResetWarning();
            // Навигация в WinForms-контрол замеса.
            WsFormNavigationUtils.NavigateToKneadingUserControl(ShowFormUserControl);
        });
    }

    /// <summary>
    /// Сброс предупреждения.
    /// </summary>
    private void ResetWarning() => MdInvokeControl.SetText(fieldWarning, string.Empty);

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
            // Инкремент счётчика этикеток.
            UserSession.AddScaleCounter();
            // Проверить наличие ПЛУ.
            if (!UserSession.CheckPluIsEmpty(fieldWarning)) return;
            // Проверить наличие вложенности ПЛУ.
            if (!UserSession.CheckViewPluNesting(LabelSession.PluLine.Plu, fieldWarning)) return;
            // Проверить наличие весовой платформы Масса-К.
            if (IsSkipDialogs || Debug.IsRelease)
                if (!UserSession.CheckWeightMassaDeviceExists()) return;
            // Проверить стабилизацию весовой платформы Масса-К.
            if (IsSkipDialogs || Debug.IsRelease)
                if (!UserSession.CheckWeightMassaIsStable(fieldWarning)) return;
            // Проверить ГТИН ПЛУ.
            if (!UserSession.CheckPluGtin(fieldWarning)) return;
            // Задать фейк данные веса ПЛУ для режима разработки.
            if (!IsSkipDialogs && Debug.IsDevelop)
                UserSession.SetPluWeighingFakeForDevelop(ShowFormUserControl);
            // Проверить отрицательный вес.
            if (!UserSession.CheckWeightIsNegative(fieldWarning)) return;
            // Создать новое взвешивание ПЛУ.
            UserSession.NewPluWeighing();
            // Проверить границы веса.
            if (!UserSession.CheckWeightThresholds(fieldWarning)) return;
            // Проверить подключение принтера.
            if (!IsSkipDialogs && Debug.IsDevelop)
            {
                // Навигация в контрол диалога Отмена/Да.
                WsFormNavigationUtils.NavigateToMessageUserControlCancelYes(ShowFormUserControl,
                    LocaleCore.Print.QuestionPrintCheckAccess, true, WsEnumLogType.Question, ActionNo, ActionYes);
                void ActionNo()
                {
                    // Печать этикетки.
                    UserSession.PrintLabel(ShowFormUserControl, fieldWarning, false);
                }
                void ActionYes()
                {
                    // Проверить подключение и готовность принтеров.
                    if (ActionPreparePrintCheckPrinters())
                        // Печать этикетки.
                        UserSession.PrintLabel(ShowFormUserControl, fieldWarning, false);
                }
            }
            else
            {
                // Проверить подключение и готовность принтеров.
                if (ActionPreparePrintCheckPrinters())
                    // Печать этикетки.
                    UserSession.PrintLabel(ShowFormUserControl, fieldWarning, false);
            }
        });
        UserSession.PluginMassa.IsWeightNetFake = false;
    }

    /// <summary>
    /// Проверить подключение и готовность принтеров.
    /// </summary>
    private bool ActionPreparePrintCheckPrinters()
    {
        // Проверить подключение и готовность основного принтера.
        if (!UserSession.CheckPrintIsConnectAndReady(fieldWarning, LabelSession.PluginPrintMain, true))
            return false;
        // Проверить подключение и готовность транспортного принтера.
        if (LabelSession.Line.IsShipping)
            if (!UserSession.CheckPrintIsConnectAndReady(fieldWarning, LabelSession.PluginPrintShipping, false))
                return false;
        return true;
    }

    /// <summary>
    /// Сменить вложенность ПЛУ.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionSwitchPluNesting(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            // Проверить наличие ПЛУ.
            if (!ActionCheckPluIdentityIsNew()) return;
            // Обновить кэш.
            ContextCache.Load(WsSqlTableName.ViewPlusNesting);
            // Навигация в контрол смены вложенности ПЛУ.
            WsFormNavigationUtils.NavigateToPlusNestingUserControl(ShowFormUserControl);
        });
    }

    #endregion
}