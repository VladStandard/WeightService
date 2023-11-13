using WsStorageCore.Entities.SchemaRef1c.Plus;

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
    
    private WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    private WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    public WsPluginLabelsHelper PluginLabels => WsPluginLabelsHelper.Instance;
    public WsPluginMassaHelper PluginMassa => WsPluginMassaHelper.Instance;
    public WsPluginMemoryHelper PluginMemory => WsPluginMemoryHelper.Instance;
    public Stopwatch StopwatchMain { get; set; } = new();
    
    #endregion

    #region Public and private methods

    public void PluginsClose()
    {
        PluginMemory.Dispose();
        PluginMassa.Dispose();
        LabelSession.PluginPrintTscMain?.Dispose();
        LabelSession.PluginPrintZebraMain?.Dispose();
        PluginLabels.Dispose();
    }

    /// <summary>
    /// Проверить наличие ПЛУ.
    /// </summary>
    public bool CheckPluIsEmpty(Label fieldWarning)
    {
        if (LabelSession.PluLine.IsNew)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, WsLocaleCore.LabelPrint.PluNotSelect);
            ContextItem.SaveLogWarning(WsLocaleCore.LabelPrint.PluNotSelect);
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
        return LabelSession.PluLine is { IsNew: false, Plu.IsCheckWeight: false } || true;
    }

    /// <summary>
    /// Проверить стабилизацию весовой платформы Масса-К.
    /// </summary>
    public bool CheckWeightMassaIsStable(Label fieldWarning)
    {
        if (!LabelSession.PluLine.Plu.IsCheckWeight || PluginMassa.IsStable)
            return true;
        MdInvokeControl.SetVisible(fieldWarning, true);
        MdInvokeControl.SetText(fieldWarning, $"{WsLocaleCore.LabelPrint.MassaIsNotCalc} {WsLocaleCore.LabelPrint.MassaWaitStable}.");
        ContextItem.SaveLogWarning($"{WsLocaleCore.LabelPrint.MassaIsNotCalc} {WsLocaleCore.LabelPrint.MassaWaitStable}.");
        return false;
    }
    
    public bool CheckWeight(Label fieldWarning)
    {
        if (!LabelSession.PluLine.Plu.IsCheckWeight) return true;

        if (PluginMassa.WeightNet <= 0)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, WsLocaleCore.LabelPrint.CheckWeightIsZero);
            ContextItem.SaveLogWarning(WsLocaleCore.LabelPrint.CheckWeightIsZero);
            return false;
        }

        decimal weight = PluginMassa.WeightNet - (LabelSession.PluLine.IsNew ? 0 : LabelSession.ViewPluNesting.TareWeight);
        if (weight is < WsLocalizationUtils.MassaThresholdValue or < WsLocalizationUtils.MassaThresholdPositive)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, WsLocaleCore.LabelPrint.CheckWeightThreshold(weight));
            ContextItem.SaveLogWarning(WsLocaleCore.LabelPrint.CheckWeightThreshold(weight));
            return false;
        }
        
        if (LabelSession.ViewPluNesting is not { WeightNom: > 0, WeightMin: not 0, WeightMax: not 0 })
            return true;

        decimal weightMax = LabelSession.ViewPluNesting.WeightMax;
        decimal weightMin = LabelSession.ViewPluNesting.WeightMin;
        decimal weightNom = LabelSession.ViewPluNesting.WeightNom;
        
        if (PluginMassa.WeightNet >= weightMin && PluginMassa.WeightNet <= weightMax)
            return true;
        
        MdInvokeControl.SetVisible(fieldWarning, true);
        string message = WsLocaleCore.LabelPrint.CheckWeightThresholds(PluginMassa.WeightNet, 
            weightMax, weightNom, weightMin);
        MdInvokeControl.SetText(fieldWarning, message);
        ContextItem.SaveLogError(message);
        return false;
    }

    /// <summary>
    /// Создать новое взвешивание ПЛУ.
    /// </summary>
    public void NewPluWeighing()
    {
        LabelSession.PluWeighing = new()
        {
            PluScale = LabelSession.PluLine,
            Kneading = (short)LabelSession.WeighingSettings.Kneading,
            NettoWeight = LabelSession.PluLine.Plu.IsCheckWeight ? PluginMassa.WeightNet - LabelSession.ViewPluNesting.TareWeight 
                : LabelSession.ViewPluNesting.WeightNom,
            WeightTare = LabelSession.ViewPluNesting.TareWeight,
        };

        // Save or update weighing products.
        SaveOrUpdatePluWeighing();
    }

    /// <summary>
    /// Save or update weighing products.
    /// </summary>
    private void SaveOrUpdatePluWeighing()
    {
        if (!LabelSession.PluWeighing.PluScale.Plu.IsCheckWeight) return;

        if (LabelSession.PluWeighing.IsNew)
            SqlCore.Save(LabelSession.PluWeighing);
        else
            SqlCore.Update(LabelSession.PluWeighing);
    }

    /// <summary>
    /// Проверить наличие вложенности ПЛУ.
    /// </summary>
    public bool CheckViewPluNesting(WsSqlPluEntity plu, Label fieldWarning)
    {
        if (LabelSession.ViewPluNesting.PluNumber.Equals((ushort)plu.Number)) return true;

        MdInvokeControl.SetVisible(fieldWarning, true);
        MdInvokeControl.SetText(fieldWarning, WsLocaleCore.LabelPrint.PluPackageNotSelect);
        ContextItem.SaveLogError(WsLocaleCore.LabelPrint.PluPackageNotSelect);
        return false;
    }

    #endregion
}