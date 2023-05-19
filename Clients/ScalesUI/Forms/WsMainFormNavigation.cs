// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Forms;

public partial class WsMainForm
{
    #region Public and private methods - Возврат из контролов

    /// <summary>
    /// Предзагрузка навигации.
    /// </summary>
    private void PreLoadNavigation()
    {
        // Обновить кэш.
        UserSession.RefreshCache();
        // Навигация.
        WsWinFormNavigationUtils.NavigationUserControl = new();
        WsWinFormNavigationUtils.LayoutPanelMain = layoutPanelMain;
        //WsWinFormNavigationUtils.NavigationUserControl.ViewModel.ActionReturnOk = WsWinFormNavigationUtils.ReturnBackDefault;
        //WsWinFormNavigationUtils.NavigationUserControl.ViewModel.ActionReturnOk += ActionFinally;
        //WsWinFormNavigationUtils.NavigationUserControl.ViewModel.ActionReturnCancel = WsWinFormNavigationUtils.ReturnBackDefault;
        //WsWinFormNavigationUtils.NavigationUserControl.ViewModel.ActionReturnCancel += ActionFinally;
        //WsWinFormNavigationUtils.NavigationUserControl.ViewModel.ActionReturnFinally = () => { };  // Sorry, but this is it.
        //WsWinFormNavigationUtils.NavigationUserControl.Parent = this;
        Controls.Add(WsWinFormNavigationUtils.NavigationUserControl);
        WsWinFormNavigationUtils.NavigationUserControl.Dock = DockStyle.Fill;
        // Ожидание.
        WsWinFormNavigationUtils.WaitUserControl = new();
        WsWinFormNavigationUtils.WaitUserControl.ViewModel.ActionReturnOk = ReturnFromWait;
        WsWinFormNavigationUtils.WaitUserControl.ViewModel.ActionReturnOk += WsWinFormNavigationUtils.ReturnBackDefault;
        WsWinFormNavigationUtils.WaitUserControl.ViewModel.ActionReturnOk += ActionFinally;
        WsWinFormNavigationUtils.WaitUserControl.ViewModel.ActionReturnCancel = ReturnFromWait;
        WsWinFormNavigationUtils.WaitUserControl.ViewModel.ActionReturnCancel += WsWinFormNavigationUtils.ReturnBackDefault;
        WsWinFormNavigationUtils.WaitUserControl.ViewModel.ActionReturnCancel += ActionFinally;
        // Замес.
        WsWinFormNavigationUtils.KneadingUserControl = new();
        WsWinFormNavigationUtils.KneadingUserControl.ViewModel.ActionReturnOk = ReturnFromKneading;
        WsWinFormNavigationUtils.KneadingUserControl.ViewModel.ActionReturnOk += WsWinFormNavigationUtils.ReturnBackDefault;
        WsWinFormNavigationUtils.KneadingUserControl.ViewModel.ActionReturnOk += ActionFinally;
        WsWinFormNavigationUtils.KneadingUserControl.ViewModel.ActionReturnCancel = ReturnFromKneading;
        WsWinFormNavigationUtils.KneadingUserControl.ViewModel.ActionReturnCancel += WsWinFormNavigationUtils.ReturnBackDefault;
        WsWinFormNavigationUtils.KneadingUserControl.ViewModel.ActionReturnCancel += ActionFinally;
        // Ещё.
        WsWinFormNavigationUtils.MoreUserControl = new();
        WsWinFormNavigationUtils.MoreUserControl.ViewModel.ActionReturnOk = ReturnFromMore;
        WsWinFormNavigationUtils.MoreUserControl.ViewModel.ActionReturnOk += WsWinFormNavigationUtils.ReturnBackDefault;
        WsWinFormNavigationUtils.MoreUserControl.ViewModel.ActionReturnOk += ActionFinally;
        WsWinFormNavigationUtils.MoreUserControl.ViewModel.ActionReturnCancel = ReturnFromMore;
        WsWinFormNavigationUtils.MoreUserControl.ViewModel.ActionReturnCancel += WsWinFormNavigationUtils.ReturnBackDefault;
        WsWinFormNavigationUtils.MoreUserControl.ViewModel.ActionReturnCancel += ActionFinally;
        // Смена линии.
        WsWinFormNavigationUtils.LinesUserControl = new();
        WsWinFormNavigationUtils.LinesUserControl.ViewModel.ActionReturnOk = ReturnOkFromLines;
        WsWinFormNavigationUtils.LinesUserControl.ViewModel.ActionReturnOk += WsWinFormNavigationUtils.ReturnBackDefault;
        WsWinFormNavigationUtils.LinesUserControl.ViewModel.ActionReturnOk += ActionFinally;
        WsWinFormNavigationUtils.LinesUserControl.ViewModel.ActionReturnCancel = ReturnCancelFromLines;
        WsWinFormNavigationUtils.LinesUserControl.ViewModel.ActionReturnCancel += WsWinFormNavigationUtils.ReturnBackDefault;
        WsWinFormNavigationUtils.LinesUserControl.ViewModel.ActionReturnCancel += ActionFinally;
        // Смена ПЛУ.
        WsWinFormNavigationUtils.PlusUserControl = new();
        WsWinFormNavigationUtils.PlusUserControl.ViewModel.ActionReturnOk = ReturnOkFromPlus;
        WsWinFormNavigationUtils.PlusUserControl.ViewModel.ActionReturnOk += WsWinFormNavigationUtils.ReturnBackDefault;
        WsWinFormNavigationUtils.PlusUserControl.ViewModel.ActionReturnOk += ActionFinally;
        WsWinFormNavigationUtils.PlusUserControl.ViewModel.ActionReturnCancel = ReturnCancelFromPlus;
        WsWinFormNavigationUtils.PlusUserControl.ViewModel.ActionReturnCancel += WsWinFormNavigationUtils.ReturnBackDefault;
        WsWinFormNavigationUtils.PlusUserControl.ViewModel.ActionReturnCancel += ActionFinally;
        // Смена вложенности ПЛУ.
        WsWinFormNavigationUtils.PlusNestingUserControl = new();
        WsWinFormNavigationUtils.PlusNestingUserControl.ViewModel.ActionReturnOk = ReturnOkFromPlusNesting;
        WsWinFormNavigationUtils.PlusNestingUserControl.ViewModel.ActionReturnOk += WsWinFormNavigationUtils.ReturnBackDefault;
        WsWinFormNavigationUtils.PlusNestingUserControl.ViewModel.ActionReturnOk += ActionFinally;
        WsWinFormNavigationUtils.PlusNestingUserControl.ViewModel.ActionReturnCancel = ReturnCancelFromPlusNesting;
        WsWinFormNavigationUtils.PlusNestingUserControl.ViewModel.ActionReturnCancel += WsWinFormNavigationUtils.ReturnBackDefault;
        WsWinFormNavigationUtils.PlusNestingUserControl.ViewModel.ActionReturnCancel += ActionFinally;
    }

    private void ReturnFromWait()
    {
        //
    }

    /// <summary>
    /// Возврат из контрола смены замеса.
    /// </summary>
    private void ReturnFromKneading()
    {
        using WsNumberInputForm numberInputForm = new() { InputValue = 0 };
        DialogResult result = numberInputForm.ShowDialog(this);
        numberInputForm.Close();
        if (result == DialogResult.OK)
            UserSession.WeighingSettings.Kneading = (byte)numberInputForm.InputValue;
    }

    /// <summary>
    /// Возврат из контрола смены ещё.
    /// </summary>
    private void ReturnFromMore() => UserSession.PluginMassa.Execute();

    /// <summary>
    /// Возврат ОК из контрола смены ПЛУ.
    /// </summary>
    private void ReturnOkFromPlus()
    {
        UserSession.WeighingSettings.Kneading = 1;
        UserSession.ProductDate = DateTime.Now;
        UserSession.NewPallet();
        MdInvokeControl.SetVisible(labelNettoWeight, UserSession.PluScale.Plu.IsCheckWeight);
        MdInvokeControl.SetVisible(fieldNettoWeight, UserSession.PluScale.Plu.IsCheckWeight);
        ActionMore(null, null);
    }

    /// <summary>
    /// Возврат Отмена из контрола смены ПЛУ.
    /// </summary>
    private void ReturnCancelFromPlus() =>
        UserSession.PluScale = UserSession.ContextManager.AccessItem.GetItemNewEmpty<WsSqlPluScaleModel>();

    /// <summary>
    /// Возврат ОК из контрола смены вложенности ПЛУ.
    /// </summary>
    private void ReturnOkFromPlusNesting()
    {
        UserSession.ViewPluNesting = WsWinFormNavigationUtils.PlusNestingUserControl.ViewModel.PluNesting;
    }

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
        UserSession.SetMain(WsWinFormNavigationUtils.LinesUserControl.ViewModel.Line.IdentityValueId, WsWinFormNavigationUtils.LinesUserControl.ViewModel.Area);
        ActionMore(null, null);
    }

    /// <summary>
    /// Возврат Отмена из контрола смены линии.
    /// </summary>
    private void ReturnCancelFromLines()
    {
        ActionMore(null, null);
    }

    #endregion

    #region Public and private methods - действия

    private void ActionFinally()
    {
        MdInvokeControl.Select(ButtonPrint);
        // LoadLocalizationDynamic(Lang.Russian);
        LocaleCore.Lang = LocaleData.Lang = Lang.Russian;
        string area = UserSession.Scale.WorkShop is null
            ? LocaleCore.Table.FieldEmpty : UserSession.ProductionFacility.Name;
        MdInvokeControl.SetText(ButtonLine,
            $"{UserSession.Scale.Description} | {UserSession.Scale.Number}{Environment.NewLine}{area}");
        //if (UserSession.PluNestingView.ItemView.IsNew)
        //    MdInvokeControl.SetText(ButtonPluNestingFk, LocaleCore.Table.FieldPackageIsNotSelected);
        //else
        MdInvokeControl.SetText(ButtonPluNestingFk, UserSession.ViewPluNesting.GetSmartName());
        MdInvokeControl.SetText(fieldPackageWeight,
            UserSession.PluScale.IsNotNew
                ? $"{UserSession.ViewPluNesting.TareWeight:0.000} {LocaleCore.Scales.WeightUnitKg}"
                : $"0,000 {LocaleCore.Scales.WeightUnitKg}");
        WsSqlTemplateModel template = UserSession.ContextManager.ContextItem.GetItemTemplateNotNullable(UserSession.PluScale);
        MdInvokeControl.SetText(fieldTemplateValue, template.Title);

        // Отобразить main control.
        WsWinFormNavigationUtils.NavigationUserControl.Visible = false;
        WsWinFormNavigationUtils.LayoutPanelMain.Visible = true;
        System.Windows.Forms.Application.DoEvents();
    }

    /// <summary>
    /// Закрыть программу.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionClose(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(this, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            WsWinFormNavigationUtils.NavigateToOperationControl($"{LocaleCore.Scales.QuestionCloseApp}?",
                true, WsEnumLogType.Question,
                new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
                ActionOk, () => { });
            ActionFinally();
            bool isOk = false;
            void ActionOk() => isOk = true;
            if (!isOk) return;
            // See the MainForm_FormClosing() method.
            Close();
        });
    }

    /// <summary>
    /// Сменить линию.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionSwitchLine(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(this, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            // Обновить кэш.
            UserSession.ContextCache.Load(WsSqlTableName.ProductionFacilities);
            UserSession.ContextCache.Load(WsSqlTableName.Scales);
            // Навигация.
            WsWinFormNavigationUtils.NavigateToControlLines();
        });
    }

    /// <summary>
    /// Проверить наличие ПЛУ.
    /// </summary>
    /// <returns></returns>
    private bool ActionCheckPluIdentityIsNew()
    {
        if (UserSession.PluScale.Plu.IsNew)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Table.FieldPluIsNotSelected);
            UserSession.ContextManager.ContextItem.SaveLogError(LocaleCore.Table.FieldPluIsNotSelected);
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
        WsWinFormNavigationUtils.ActionTryCatch(this, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            WsWinFormNavigationUtils.NavigateToOperationControl($"{LocaleCore.Scales.QuestionRunApp} ScalesTerminal?",
                true, WsEnumLogType.Question,
                new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
                ActionOk, () => { });
            bool isOk = false;
            void ActionOk() => isOk = true;
            if (!isOk) return;

            // Run app.
            if (File.Exists(LocaleData.Paths.ScalesTerminal))
            {
                UserSession.PluginMassa.Close();
                Proc.Run(LocaleData.Paths.ScalesTerminal, string.Empty, false, ProcessWindowStyle.Normal, true);
                UserSession.PluginMassa.Execute();
            }
            else
            {
                WsWinFormNavigationUtils.NavigateToOperationControl(LocaleCore.Scales.ProgramNotFound(
                        LocaleData.Paths.ScalesTerminal), true, WsEnumLogType.Warning,
                    new() { ButtonOkVisibility = Visibility.Visible },
                    () => { }, () => { });
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
        WsWinFormNavigationUtils.ActionTryCatch(this, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            if (!UserSession.PluScale.Plu.IsCheckWeight)
            {
                WsWinFormNavigationUtils.NavigateToOperationControl(LocaleCore.Scales.PluNotSelectWeight, true, WsEnumLogType.Warning,
                    new() { ButtonOkVisibility = Visibility.Visible },
                    () => { }, () => { });
                return;
            }
            if (!UserSession.PluginMassa.MassaDevice.IsOpenPort)
            {
                WsWinFormNavigationUtils.NavigateToOperationControl(LocaleCore.Scales.MassaIsNotRespond, true, WsEnumLogType.Warning,
                    new() { ButtonOkVisibility = Visibility.Visible },
                    () => { }, () => { });
                return;
            }

            WsWinFormNavigationUtils.NavigateToOperationControl(
                LocaleCore.Scales.QuestionPerformOperation, true, WsEnumLogType.Question,
                new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
                ActionOk, () => { });
            bool isOk = false;
            void ActionOk() => isOk = true;
            if (isOk) return;

            // Fix negative weight.
            //if (UserSession.PluginMassa.WeightNet < 0)
            //{
            //    UserSession.PluginMassa.ResetMassa();
            //}
            // Проверить наличие весовой платформы Масса-К.
            if (IsSkipDialogs || Debug.IsRelease)
                UserSession.CheckWeightMassaDeviceExists();
            UserSession.PluScale = new();

            UserSession.PluginMassa.Execute();
            UserSession.PluginMassa.GetInit();
        });
    }

    /// <summary>
    /// Новая паллета.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionNewPallet(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(this, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            UserSession.NewPallet();
        });
    }

    /// <summary>
    /// Сменить замес.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionKneading(object sender, EventArgs e) => 
        WsWinFormNavigationUtils.ActionTryCatch(this, WsWinFormNavigationUtils.NavigateToControlMore);

    /// <summary>
    /// Сменить ПЛУ.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionSwitchPlu(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(this, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            MdInvokeControl.SetVisible(labelNettoWeight, false);
            MdInvokeControl.SetVisible(fieldNettoWeight, false);
            UserSession.PluScale = UserSession.ContextManager.AccessItem.GetItemNewEmpty<WsSqlPluScaleModel>();
            // Навигация.
            WsWinFormNavigationUtils.NavigateToControlPlus();
        });
    }

    /// <summary>
    /// Ещё.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionMore(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(this, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            if (UserSession.PluScale.IsNew)
            {
                WsWinFormNavigationUtils.NavigateToOperationControl(LocaleCore.Scales.PluNotSelect, true, WsEnumLogType.Warning,
                    new() { ButtonOkVisibility = Visibility.Visible },
                    () => { }, () => { });
                return;
            }
            // Навигация.
            WsWinFormNavigationUtils.NavigateToControlMore();
        });
    }

    /// <summary>
    /// Сброс предупреждения.
    /// </summary>
    private void ResetWarning()
    {
        MdInvokeControl.SetVisible(fieldWarning, false);
        MdInvokeControl.SetText(fieldWarning, string.Empty);
    }

    /// <summary>
    /// Подготовка к печати.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionPreparePrint(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(this, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            // Инкремент счётчика этикеток.
            UserSession.AddScaleCounter();
            // Проверить наличие ПЛУ.
            if (!UserSession.CheckPluIsEmpty(fieldWarning)) return;
            // Проверить наличие вложенности ПЛУ.
            if (!UserSession.SetAndCheckListViewPlusNesting(UserSession.PluScale.Plu, fieldWarning)) return;
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
                UserSession.SetPluWeighingFakeForDevelop();
            // Проверить отрицательный вес.
            if (!UserSession.CheckWeightIsNegative(fieldWarning)) return;
            // Создать новое взвешивание ПЛУ.
            UserSession.NewPluWeighing();
            // Проверить границы веса.
            if (!UserSession.CheckWeightThresholds(fieldWarning)) return;
            // Проверить подключение принтера.
            bool isSkipPrintCheckAccess = false;
            if (!IsSkipDialogs && Debug.IsDevelop)
            {
                WsWinFormNavigationUtils.NavigateToOperationControl(
                    LocaleCore.Print.QuestionPrintCheckAccess, true, WsEnumLogType.Question,
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
                    ActionOk, ActionCancel);
                void ActionOk()
                {
                    isSkipPrintCheckAccess = false;
                }
                void ActionCancel()
                {
                    isSkipPrintCheckAccess = true;
                }
            }
            if (!isSkipPrintCheckAccess)
            {
                // Проверить подключение и готовность основного принтера.
                if (!UserSession.CheckPrintIsConnectAndReady(fieldWarning, UserSession.PluginPrintMain, true)) return;
                // Проверить подключение и готовность транспортного принтера.
                if (UserSession.Scale.IsShipping)
                    if (!UserSession.CheckPrintIsConnectAndReady(fieldWarning, UserSession.PluginPrintShipping, false)) return;
            }
            // Печать этикетки.
            UserSession.PrintLabel(fieldWarning, false);
        });
        UserSession.PluginMassa.IsWeightNetFake = false;
    }

    /// <summary>
    /// Сменить вложенность ПЛУ.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionSwitchPluNesting(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(this, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            // Проверить наличие ПЛУ.
            if (!ActionCheckPluIdentityIsNew()) return;
            // Обновить кэш.
            UserSession.ContextCache.Load(WsSqlTableName.ViewPlusNesting);
            // Навигация.
            WsWinFormNavigationUtils.NavigateToControlPlusNesting();
        });
    }

    #endregion
}