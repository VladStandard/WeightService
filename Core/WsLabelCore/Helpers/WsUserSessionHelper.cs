namespace WsLabelCore.Helpers;

/// <summary>
/// User session.
/// </summary>
#nullable enable
public sealed class WsUserSessionHelper //: BaseViewModel
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static WsUserSessionHelper _instance;
#pragma warning restore CS8618
    public static WsUserSessionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private WsDebugHelper Debug => WsDebugHelper.Instance;
    private WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    public WsPluginLabelsHelper PluginLabels => WsPluginLabelsHelper.Instance;
    public WsPluginMassaHelper PluginMassa => WsPluginMassaHelper.Instance;
    public WsPluginMemoryHelper PluginMemory => WsPluginMemoryHelper.Instance;
    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    public Stopwatch StopwatchMain { get; set; } = new();
    
    #endregion

    #region Public and private methods

    public void PluginsClose()
    {
        PluginMemory.Close();
        PluginMassa.Close();
        LabelSession.PluginPrintTscMain?.Close();
        LabelSession.PluginPrintZebraMain?.Close();
        LabelSession.PluginPrintTscShipping?.Close();
        LabelSession.PluginPrintZebraShipping?.Close();
        PluginLabels.Close();
    }

    /// <summary>
    /// Проверить наличие ПЛУ.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckPluIsEmpty(Label fieldWarning)
    {
        if (LabelSession.PluLine.IsNew)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, WsLocaleCore.LabelPrint.PluNotSelect);
            ContextManager.ContextItem.SaveLogWarning(WsLocaleCore.LabelPrint.PluNotSelect);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить наличие весовой платформы Масса-К.
    /// </summary>
    /// <returns></returns>
    public bool CheckWeightMassaDeviceExists()
    {
        if (Debug.IsDevelop) return true;
        return LabelSession.PluLine is { IsNew: false, Plu.IsCheckWeight: false } || true;
    }

    /// <summary>
    /// Проверить стабилизацию весовой платформы Масса-К.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckWeightMassaIsStable(Label fieldWarning)
    {
        if (Debug.IsDevelop) return true;
        if (LabelSession.PluLine.Plu.IsCheckWeight && !PluginMassa.IsStable)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, $"{WsLocaleCore.LabelPrint.MassaIsNotCalc} {WsLocaleCore.LabelPrint.MassaWaitStable}.");
            ContextManager.ContextItem.SaveLogWarning($"{WsLocaleCore.LabelPrint.MassaIsNotCalc} {WsLocaleCore.LabelPrint.MassaWaitStable}.");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить ГТИН ПЛУ.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckPluGtin(Label fieldWarning)
    {
        if (string.IsNullOrEmpty(LabelSession.PluLine.Plu.Gtin))
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, WsLocaleCore.LabelPrint.PluGtinIsNotSet);
            ContextManager.ContextItem.SaveLogError(WsLocaleCore.LabelPrint.PluGtinIsNotSet);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить отрицательный вес.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckWeightIsNegative(Label fieldWarning)
    {
        if (PluginMassa.IsWeightNetFake) return true;
        if (!LabelSession.PluLine.Plu.IsCheckWeight) return true;

        if (PluginMassa.WeightNet <= 0)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, WsLocaleCore.LabelPrint.CheckWeightIsZero);
            ContextManager.ContextItem.SaveLogWarning(WsLocaleCore.LabelPrint.CheckWeightIsZero);
            return false;
        }

        decimal weight = PluginMassa.WeightNet - (LabelSession.PluLine.IsNew ? 0 : LabelSession.ViewPluNesting.TareWeight);
        if (weight < WsLocalizationUtils.MassaThresholdValue || weight < WsLocalizationUtils.MassaThresholdPositive)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, WsLocaleCore.LabelPrint.CheckWeightThreshold(weight));
            ContextManager.ContextItem.SaveLogWarning(WsLocaleCore.LabelPrint.CheckWeightThreshold(weight));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить положительный вес.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckWeightIsPositive(Label fieldWarning)
    {
        if (!LabelSession.PluLine.Plu.IsCheckWeight) return true;

        decimal weight = PluginMassa.WeightNet - (LabelSession.PluLine.IsNew ? 0 : LabelSession.ViewPluNesting.TareWeight);
        if (weight > WsLocalizationUtils.MassaThresholdValue)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, WsLocaleCore.LabelPrint.CheckWeightThreshold(weight));
            ContextManager.ContextItem.SaveLogError(WsLocaleCore.LabelPrint.CheckWeightThreshold(weight));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить границы веса.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckWeightThresholds(Label fieldWarning)
    {
        if (PluginMassa.IsWeightNetFake) return true;
        if (!LabelSession.PluLine.Plu.IsCheckWeight) return true;

        if (LabelSession.ViewPluNesting is { WeightNom: > 0, WeightMin: not 0, WeightMax: not 0 })
        {
            if (!(LabelSession.PluWeighing.NettoWeight >= LabelSession.ViewPluNesting.WeightMin && LabelSession.PluWeighing.NettoWeight <=
                    LabelSession.ViewPluNesting.WeightMax))
            {
                if (LabelSession.PluWeighing.IsNotNew)
                {
                    MdInvokeControl.SetVisible(fieldWarning, true);
                    string message = WsLocaleCore.LabelPrint.CheckWeightThresholds(LabelSession.PluWeighing.NettoWeight,
                        LabelSession.PluLine.IsNew ? 0 : LabelSession.ViewPluNesting.WeightMax,
                        LabelSession.PluLine.IsNew ? 0 : LabelSession.ViewPluNesting.WeightNom,
                        LabelSession.PluLine.IsNew ? 0 : LabelSession.ViewPluNesting.WeightMin);
                    MdInvokeControl.SetText(fieldWarning, message);
                    ContextManager.ContextItem.SaveLogError(message);
                }
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Создать новое взвешивание ПЛУ.
    /// </summary>
    public void NewPluWeighing()
    {
        WsSqlProductSeriesModel productSeries =
            new WsSqlProductSeriesRepository().GetItemByLineNotClose(LabelSession.PluLine.Line);

        LabelSession.PluWeighing = new()
        {
            PluScale = LabelSession.PluLine,
            Kneading = LabelSession.WeighingSettings.Kneading,
            NettoWeight = LabelSession.PluLine.Plu.IsCheckWeight ? PluginMassa.WeightNet - LabelSession.ViewPluNesting.TareWeight 
                : LabelSession.ViewPluNesting.WeightNom,
            WeightTare = LabelSession.ViewPluNesting.TareWeight,
            Series = productSeries,
        };

        // Save or update weighing products.
        SaveOrUpdatePluWeighing();
    }

    /// <summary>
    /// Использовать фейк-данные для веса ПЛУ.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="returnPreparePrint"></param>
    public void SetPluWeighingFakeForDevelop(Action<WsFormBaseUserControl, string> showNavigation, Action returnPreparePrint)
    {
        if (Debug.IsSkipDialogs) { ActionCancel(); return; }
        if (Debug.IsRelease) { ActionCancel(); return; }
        if (!LabelSession.PluLine.Plu.IsCheckWeight) { ActionCancel(); return; }
        if (PluginMassa.WeightNet > 0) { ActionCancel(); return; }

        // Навигация в новый WinForms-контрол диалога.
        WsFormNavigationUtils.NavigateToNewDialog(showNavigation, WsLocaleCore.Print.QuestionUseFakeData,
            true, WsEnumLogType.Question, WsEnumDialogType.CancelYes, new() { ActionCancel, ActionYes });
        void ActionCancel()
        {
            if (PluginMassa.IsWeightNetFake)
            {
                PluginMassa.IsWeightNetFake = false;
                PluginMassa.WeightNet = 0;
            }
            returnPreparePrint.Invoke();
        }
        void ActionYes()
        {
            PluginMassa.WeightNet = WsStrUtils.NextDecimal(LabelSession.ViewPluNesting.WeightMin, LabelSession.ViewPluNesting.WeightMax);
            PluginMassa.IsWeightNetFake = true;
            returnPreparePrint.Invoke();
        }
    }

    /// <summary>
    /// Save or update weighing products.
    /// </summary>
    private void SaveOrUpdatePluWeighing()
    {
        if (!LabelSession.PluWeighing.PluScale.Plu.IsCheckWeight) return;

        if (LabelSession.PluWeighing.IsNew)
            ContextManager.SqlCore.Save(LabelSession.PluWeighing);
        else
            ContextManager.SqlCore.Update(LabelSession.PluWeighing);
    }

    /// <summary>
    /// Проверить наличие вложенности ПЛУ.
    /// </summary>
    /// <param name="plu"></param>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckViewPluNesting(WsSqlPluModel plu, Label fieldWarning)
    {
        if (LabelSession.ViewPluNesting.PluNumber.Equals((ushort)plu.Number)) return true;

        MdInvokeControl.SetVisible(fieldWarning, true);
        MdInvokeControl.SetText(fieldWarning, WsLocaleCore.LabelPrint.PluPackageNotSelect);
        ContextManager.ContextItem.SaveLogError(WsLocaleCore.LabelPrint.PluPackageNotSelect);
        return false;
    }

    #endregion
}