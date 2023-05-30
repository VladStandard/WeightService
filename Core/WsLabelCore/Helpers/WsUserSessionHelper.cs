// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Helpers;
#nullable enable
/// <summary>
/// User session.
/// </summary>
public sealed class WsUserSessionHelper //: BaseViewModel
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static WsUserSessionHelper _instance;
#pragma warning restore CS8618
    public static WsUserSessionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private DebugHelper Debug => DebugHelper.Instance;
    private WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    private WsSqlBarCodeController BarCode => WsSqlBarCodeController.Instance;
    public WsPluginLabelsHelper PluginLabels => WsPluginLabelsHelper.Instance;
    public WsPluginMassaHelper PluginMassa => WsPluginMassaHelper.Instance;
    public WsPluginMemoryHelper PluginMemory => WsPluginMemoryHelper.Instance;
    private WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    public WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    public Stopwatch StopwatchMain { get; set; } = new();
    
    #endregion

    #region Public and private methods

    public void PluginsClose()
    {
        PluginMemory.Close();
        PluginMassa.Close();
        LabelSession.PluginPrintMain.Close();
        LabelSession.PluginPrintShipping.Close();
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
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.PluNotSelect);
            ContextManager.ContextItem.SaveLogError(LocaleCore.Scales.PluNotSelect);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить наличие весовой платформы Масса-К.
    /// </summary>
    /// <returns></returns>
    public bool CheckWeightMassaDeviceExists() => LabelSession.PluLine is { IsNew: false, Plu.IsCheckWeight: false } || true;

    /// <summary>
    /// Проверить стабилизацию весовой платформы Масса-К.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckWeightMassaIsStable(Label fieldWarning)
    {
        if (LabelSession.PluLine.Plu.IsCheckWeight && !PluginMassa.IsStable)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, $"{LocaleCore.Scales.MassaIsNotCalc} {LocaleCore.Scales.MassaWaitStable}");
            ContextManager.ContextItem.SaveLogWarning($"{LocaleCore.Scales.MassaIsNotCalc} {LocaleCore.Scales.MassaWaitStable}");
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
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.PluGtinIsNotSet);
            ContextManager.ContextItem.SaveLogError(LocaleCore.Scales.PluGtinIsNotSet);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить подключение и готовность принтера.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <param name="managerPrint"></param>
    /// <param name="isMain"></param>
    /// <returns></returns>
    public bool CheckPrintIsConnectAndReady(Label fieldWarning, WsPluginPrintModel managerPrint, bool isMain)
    {
        // Подключение.
        if (managerPrint.Printer.PingStatus != IPStatus.Success)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            string message = isMain
                ? $"{LocaleCore.Print.DeviceMainIsUnavailable} {LocaleCore.Print.DeviceCheckConnect}"
                : $"{LocaleCore.Print.DeviceShippingIsUnavailable} {LocaleCore.Print.DeviceCheckConnect}";
            MdInvokeControl.SetText(fieldWarning, message);
            ContextManager.ContextItem.SaveLogError(message);
            return false;
        }
        // Готовность.
        if (!managerPrint.CheckDeviceStatus())
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            string message = isMain
                ? $"{LocaleCore.Print.DeviceMainCheckStatus} {managerPrint.GetDeviceStatus()}"
                : $"{LocaleCore.Print.DeviceShippingCheckStatus} {managerPrint.GetDeviceStatus()}";
            MdInvokeControl.SetText(fieldWarning, message);
            ContextManager.ContextItem.SaveLogError(message);
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
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.CheckWeightIsZero);
            ContextManager.ContextItem.SaveLogError(LocaleCore.Scales.CheckWeightIsZero);
            return false;
        }

        decimal weight = PluginMassa.WeightNet - (LabelSession.PluLine.IsNew ? 0 : LabelSession.ViewPluNesting.TareWeight);
        if (weight < LocaleCore.Scales.MassaThresholdValue || weight < LocaleCore.Scales.MassaThresholdPositive)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.CheckWeightThreshold(weight));
            ContextManager.ContextItem.SaveLogError(LocaleCore.Scales.CheckWeightThreshold(weight));
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
        if (weight > LocaleCore.Scales.MassaThresholdValue)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.CheckWeightThreshold(weight));
            ContextManager.ContextItem.SaveLogError(LocaleCore.Scales.CheckWeightThreshold(weight));
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
                    string message = LocaleCore.Scales.CheckWeightThresholds(LabelSession.PluWeighing.NettoWeight,
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
    /// Печать этикетки.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="fieldWarning"></param>
    /// <param name="isClearBuffer"></param>
    public void PrintLabel(Action<WsFormBaseUserControl, string> showNavigation, Label fieldWarning, bool isClearBuffer)
    {
        if (LabelSession.Line is { IsOrder: true })
            throw new("Order under construct!");

        // #WS-T-710 Печать этикеток. Исправление счётчика этикеток
        LabelSession.PluLine.Line = LabelSession.Line;
        // Получить шаблон этикетки.
        WsSqlTemplateModel template = ContextManager.ContextItem.GetItemTemplateNotNullable(LabelSession.PluLine);
        // Проверить наличие шаблона этикетки.
        if (template.IsNew)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.PluTemplateNotSet);
            ContextManager.ContextItem.SaveLogError(LocaleCore.Scales.PluTemplateNotSet);
            return;
        }
        // Выбор типа ПЛУ.
        switch (LabelSession.PluLine.Plu.IsCheckWeight)
        {
            // Весовая ПЛУ.
            case true:
                PrintLabelCore(showNavigation, template, isClearBuffer);
                break;
            // Штучная ПЛУ.
            default:
                PrintLabelCount(showNavigation, template, isClearBuffer);
                break;
        
        }

        LabelSession.PluWeighing = new();
    }

    /// <summary>
    /// Инкремент счётчика этикеток.
    /// </summary>
    public void AddScaleCounter()
    {
        LabelSession.Line.Counter++;
        ContextManager.AccessItem.Update(LabelSession.Line);
    }

    /// <summary>
    /// Печать этикетки штучной ПЛУ.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="template"></param>
    /// <param name="isClearBuffer"></param>
    private void PrintLabelCount(Action<WsFormBaseUserControl, string> showNavigation, WsSqlTemplateModel template, bool isClearBuffer)
    {
        // Шаблон с указанием кол-ва и не весовой продукт.
        if (template.Data.Contains("^PQ1") && !LabelSession.PluLine.Plu.IsCheckWeight)
        {
            // Изменить кол-во этикеток.
            if (LabelSession.WeighingSettings.LabelsCountMain > 1)
                template.Data = template.Data.Replace("^PQ1", $"^PQ{LabelSession.WeighingSettings.LabelsCountMain}");
            // Печать этикетки ПЛУ.
            PrintLabelCore(showNavigation, template, isClearBuffer);
        }
        // Шаблон без указания кол-ва.
        else
        {
            for (int i = LabelSession.PluginPrintMain.LabelPrintedCount; i <= LabelSession.WeighingSettings.LabelsCountMain; i++)
            {
                // Печать этикетки ПЛУ.
                PrintLabelCore(showNavigation, template, isClearBuffer);
            }
        }
    }

    /// <summary>
    /// Создать новое взвешивание ПЛУ.
    /// </summary>
    public void NewPluWeighing()
    {
        WsSqlProductSeriesModel productSeries = ContextManager.ContextItem.GetItemProductSeriesNotNullable(
            LabelSession.PluLine.Line);

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
    /// Задать фейк данные веса ПЛУ для режима разработки.
    /// </summary>
    /// <param name="showNavigation"></param>
    public void SetPluWeighingFakeForDevelop(Action<WsFormBaseUserControl, string> showNavigation)
    {
        if (!LabelSession.PluLine.Plu.IsCheckWeight) return;
        if (PluginMassa.WeightNet > 0) return;
        // Навигация в контрол сообщений.
        WsFormNavigationUtils.NavigateToMessageUserControlCancelYes(showNavigation, LocaleCore.Print.QuestionUseFakeData,
            true, WsEnumLogType.Question, () => { }, ActionYes);
        void ActionYes()
        {
            PluginMassa.WeightNet = StrUtils.NextDecimal(LabelSession.ViewPluNesting.WeightMin, LabelSession.ViewPluNesting.WeightMax);
            PluginMassa.IsWeightNetFake = true;
        }
    }

    /// <summary>
    /// Печать этикетки ПЛУ.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="template"></param>
    /// <param name="isClearBuffer"></param>
    private void PrintLabelCore(Action<WsFormBaseUserControl, string> showNavigation, WsSqlTemplateModel template, bool isClearBuffer)
    {
        try
        {
            (WsSqlPluLabelModel PluLabel, WsSqlPluLabelContextModel PluLabelContext) pluLabelWithContext = 
                CreateAndSavePluLabel(template);
            CreateAndSaveBarCodes(pluLabelWithContext.PluLabel, pluLabelWithContext.PluLabelContext);

            // Print.
            if (isClearBuffer)
            {
                LabelSession.PluginPrintMain.ClearPrintBuffer();
                if (LabelSession.Line.IsShipping)
                    LabelSession.PluginPrintShipping.ClearPrintBuffer();
            }

            // Send cmd to the print.
            if (Debug.IsDevelop)
            {
                // Навигация в контрол сообщений.
                WsFormNavigationUtils.NavigateToMessageUserControlCancelYes(showNavigation,
                    LocaleCore.Print.QuestionPrintSendCmd, true, WsEnumLogType.Question,
                    () => { }, ActionYes);
                void ActionYes()
                {
                    // Send cmd to the print.
                    LabelSession.PluginPrintMain.SendCmd(pluLabelWithContext.PluLabel);
                }
            }
            else
            {
                // Send cmd to the print.
                LabelSession.PluginPrintMain.SendCmd(pluLabelWithContext.PluLabel);
            }
        }
        catch (Exception ex)
        {
            WsFormNavigationUtils.CatchExceptionSimple(ex);
        }
    }

    /// <summary>
    /// Save or update weighing products.
    /// </summary>
    private void SaveOrUpdatePluWeighing()
    {
        if (!LabelSession.PluWeighing.PluScale.Plu.IsCheckWeight) return;

        if (LabelSession.PluWeighing.IsNew)
            ContextManager.AccessItem.Save(LabelSession.PluWeighing);
        else
            ContextManager.AccessItem.Update(LabelSession.PluWeighing);
    }

    /// <summary>
    /// Create PluLabel from Template.
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    private (WsSqlPluLabelModel, WsSqlPluLabelContextModel) CreateAndSavePluLabel(WsSqlTemplateModel template)
    {
        WsSqlPluLabelModel pluLabel = new() { PluWeighing = LabelSession.PluWeighing, PluScale = LabelSession.PluLine, 
            ProductDt = LabelSession.ProductDate };
        //pluLabel.PluWeighing.PluScale = PluScale;
        //pluLabel.PluWeighing.PluScale.Scale = Scale;
        //pluLabel.PluScale.Scale = Scale;

        // Пригодится при повторной ошибке сериализации.
        //string str = WsDataFormatUtils.SerializeAsXmlString<WsSqlScaleModel>(Scale, true, true);
        //pluLabel.Xml = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlScaleModel>(Scale, true, true);
        //str = WsDataFormatUtils.SerializeAsXmlString<WsSqlPluScaleModel>(PluScale, true, true);
        //pluLabel.Xml = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlPluScaleModel>(PluScale, true, true);
        //str = WsDataFormatUtils.SerializeAsXmlString<WsSqlPluWeighingModel>(PluWeighing, true, true);
        //pluLabel.Xml = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlPluWeighingModel>(PluWeighing, true, true);
        //str = WsDataFormatUtils.SerializeAsXmlString<WsSqlPluLabelModel>(pluLabel, true, true);

        pluLabel.Xml = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlPluLabelModel>(pluLabel, true, true);
        
        XmlDocument xmlArea = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlProductionFacilityModel>(LabelSession.Area, true, true);
        pluLabel.Xml = WsDataFormatUtils.XmlMerge(pluLabel.Xml, xmlArea);

        WsSqlPluLabelContextModel pluLabelContext = new(pluLabel, LabelSession.ViewPluNesting, pluLabel.PluScale, 
            LabelSession.Area, LabelSession.PluWeighing);
        XmlDocument xmlLabelContext = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlPluLabelContextModel>(pluLabelContext, true, true);
        pluLabel.Xml = WsDataFormatUtils.XmlMerge(pluLabel.Xml, xmlLabelContext);

        // Патч шаблона: PluLabelContextModel -> WsPluLabelContextModel
        template.Data = template.Data.Replace("PluLabelModel", nameof(WsSqlPluLabelModel));
        template.Data = template.Data.Replace("PluLabelContextModel", nameof(WsSqlPluLabelContextModel));

        pluLabel.Zpl = WsDataFormatUtils.XsltTransformation(template.Data, pluLabel.Xml.OuterXml);
        pluLabel.Zpl = WsDataFormatUtils.XmlReplaceNextLine(pluLabel.Zpl);
        pluLabel.Zpl = ZplUtils.ConvertStringToHex(pluLabel.Zpl);
        _ = DataFormatUtils.PrintCmdReplaceZplResources(pluLabel.Zpl, ActionReplaceStorageMethod(pluLabel));

        // Save.
        ContextManager.AccessItem.Save(pluLabel);

        return (pluLabel, pluLabelContext);
    }

    /// <summary>
    /// Replace temperature storage method.
    /// </summary>
    /// <param name="pluLabel"></param>
    /// <returns></returns>
    private Action<string> ActionReplaceStorageMethod(WsSqlPluLabelModel pluLabel) =>
        zpl =>
        {
            // Patch for using table `PLUS_STORAGE_METHODS_FK`.
            if (ContextCache.ViewPlusStorageMethods.Any() && zpl.Contains("[@PLUS_STORAGE_METHODS_FK]"))
            {
                WsSqlTemplateResourceModel resource = ContextManager.ContextPlusStorages.GetItemResource(pluLabel.PluScale.Plu);
                string resourceHex = ZplUtils.ConvertStringToHex(resource.Data.ValueUnicode);
                zpl = zpl.Replace("[@PLUS_STORAGE_METHODS_FK]", resourceHex);
            }
            pluLabel.Zpl = zpl;
        };

    /// <summary>
    /// Create BarCode from PluLabel.
    /// </summary>
    /// <param name="pluLabel"></param>
    /// <param name="pluLabelContext"></param>
    private void CreateAndSaveBarCodes(WsSqlPluLabelModel pluLabel, WsSqlPluLabelContextModel pluLabelContext)
    {
        WsSqlBarCodeModel barCode = new(pluLabel);
        BarCode.SetBarCodeTop(barCode, pluLabelContext);
        BarCode.SetBarCodeRight(barCode, pluLabelContext);
        BarCode.SetBarCodeBottom(barCode, pluLabelContext);
        ContextManager.AccessItem.Save(barCode);
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
        MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.PluPackageNotSelect);
        ContextManager.ContextItem.SaveLogError(LocaleCore.Scales.PluPackageNotSelect);
        return false;
    }

    #endregion
}