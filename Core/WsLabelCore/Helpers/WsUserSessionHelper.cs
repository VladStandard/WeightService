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

    private WsDebugHelper Debug => WsDebugHelper.Instance;
    private WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    private WsSqlBarCodeController BarCode => WsSqlBarCodeController.Instance;
    public WsPluginLabelsHelper PluginLabels => WsPluginLabelsHelper.Instance;
    public WsPluginMassaHelper PluginMassa => WsPluginMassaHelper.Instance;
    public WsPluginMemoryHelper PluginMemory => WsPluginMemoryHelper.Instance;
    private WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
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
    /// Проверить подключение и готовность основного принтера TSC.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckPrintIsConnectAndReadyTscMain(Label fieldWarning)
    {
        if (LabelSession.PluginPrintTscMain is null) return false;
        LabelSession.PluginPrintTscMain.ReopenTsc();
        // Подключение.
        if (!LabelSession.PluginPrintTscMain.IsConnected)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            string message = $"{LocaleCore.Print.DeviceMainIsUnavailable} {LocaleCore.Print.DeviceCheckConnect}";
            MdInvokeControl.SetText(fieldWarning, message);
            ContextManager.ContextItem.SaveLogError(message);
            return false;
        }
        // Готовность.
        //if (!LabelSession.PluginPrintTscMain.CheckDeviceStatusTsc())
        //{
        //    MdInvokeControl.SetVisible(fieldWarning, true);
        //    MdInvokeControl.SetText(fieldWarning, 
        //        $"{LocaleCore.Print.DeviceMainCheckStatus} {LabelSession.PluginPrintTscMain.GetDeviceStatusTsc()}");
        //    ContextManager.ContextItem.SaveLogError(fieldWarning.Text);
        //    return false;
        //}
        return true;
    }

    /// <summary>
    /// Проверить подключение и готовность основного принтера ZEBRA.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckPrintIsConnectAndReadyZebraMain(Label fieldWarning)
    {
        if (LabelSession.PluginPrintZebraMain is null) return false;
        LabelSession.PluginPrintZebraMain.ReopenZebra();
        // Подключение.
        if (!LabelSession.PluginPrintZebraMain.IsConnected)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            string message = $"{LocaleCore.Print.DeviceMainIsUnavailable} {LocaleCore.Print.DeviceCheckConnect}";
            MdInvokeControl.SetText(fieldWarning, message);
            ContextManager.ContextItem.SaveLogError(message);
            return false;
        }
        // Готовность.
        if (!LabelSession.PluginPrintZebraMain.CheckDeviceStatusZebra())
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, 
                $"{LocaleCore.Print.DeviceMainCheckStatus} {LabelSession.PluginPrintZebraMain.GetDeviceStatusZebra()}");
            ContextManager.ContextItem.SaveLogError(fieldWarning.Text);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить подключение и готовность транспортного принтера TSC.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckPrintIsConnectAndReadyTscShipping(Label fieldWarning)
    {
        if (LabelSession.PluginPrintTscShipping is null) return false;
        LabelSession.PluginPrintTscShipping.ReopenTsc();
        // Подключение.
        if (!LabelSession.PluginPrintTscShipping.IsConnected)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            string message = $"{LocaleCore.Print.DeviceShippingIsUnavailable} {LocaleCore.Print.DeviceCheckConnect}";
            MdInvokeControl.SetText(fieldWarning, message);
            ContextManager.ContextItem.SaveLogError(message);
            return false;
        }
        // Готовность.
        if (!LabelSession.PluginPrintTscShipping.CheckDeviceStatusTsc())
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, 
                $"{LocaleCore.Print.DeviceShippingCheckStatus} {LabelSession.PluginPrintTscShipping.GetDeviceStatusTsc()}");
            ContextManager.ContextItem.SaveLogError(fieldWarning.Text);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить подключение и готовность транспортного принтера ZEBRA.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckPrintIsConnectAndReadyZebraShipping(Label fieldWarning)
    {
        if (LabelSession.PluginPrintZebraShipping is null) return false;
        LabelSession.PluginPrintZebraShipping.ReopenZebra();
        // Подключение.
        if (!LabelSession.PluginPrintZebraShipping.IsConnected)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            string message = 
                $"{LocaleCore.Print.DeviceShippingIsUnavailable} {LocaleCore.Print.DeviceCheckConnect}";
            MdInvokeControl.SetText(fieldWarning, message);
            ContextManager.ContextItem.SaveLogError(message);
            return false;
        }
        // Готовность.
        if (!LabelSession.PluginPrintZebraShipping.CheckDeviceStatusZebra())
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, 
                $"{LocaleCore.Print.DeviceShippingCheckStatus} {LabelSession.PluginPrintZebraShipping.GetDeviceStatusZebra()}");
            ContextManager.ContextItem.SaveLogError(fieldWarning.Text);
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
    /// <param name="fieldWarning"></param>
    /// <param name="isClearBuffer"></param>
    public void PrintLabel(Label fieldWarning, bool isClearBuffer)
    {
        // Заказ в разработке.
        if (LabelSession.Line is { IsOrder: true }) throw new("Order under construct!");
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
                PrintLabelCore(ref template, isClearBuffer, false);
                break;
            // Штучная ПЛУ.
            default:
                PrintLabelCount(ref template, isClearBuffer);
                break;
        
        }
        LabelSession.PluWeighing = new();
    }

    /// <summary>
    /// Печать этикетки штучной ПЛУ.
    /// </summary>
    /// <param name="template"></param>
    /// <param name="isClearBuffer"></param>
    private void PrintLabelCount(ref WsSqlTemplateModel template, bool isClearBuffer)
    {
        byte labelsCount = LabelSession.WeighingSettings.LabelsCountMain;
        // Шаблон с указанием кол-ва штучной продукции.
        if (template.Data.Contains("^PQ1") && !LabelSession.PluLine.Plu.IsCheckWeight)
        {
            // Без инкремента счётчика печати штучной продукции.
            if (!LabelSession.IsIncrementCounter)
            {
                // Изменить кол-во этикеток.
                template.Data = template.Data.Replace("^PQ1", $"^PQ{labelsCount}");
                // Печать этикетки ПЛУ.
                PrintLabelCore(ref template, isClearBuffer, false);
            }
            // Инкремент счётчика печати штучной продукции.
            else
            {
                // Цикл по штучным этикеткам.
                for (int i = 1; i <= labelsCount; i++)
                {
                    // Печать этикетки ПЛУ.
                    PrintLabelCore(ref template, isClearBuffer, true);
                }
            }
        }
        // Шаблон без указания кол-ва.
        else
        {
            // Цикл по штучным этикеткам.
            for (int i = 1; i <= labelsCount; i++)
            {
                // Печать этикетки ПЛУ.
                PrintLabelCore(ref template, isClearBuffer, true);
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
        WsFormNavigationUtils.NavigateToNewDialog(showNavigation, LocaleCore.Print.QuestionUseFakeData,
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
            PluginMassa.WeightNet = StrUtils.NextDecimal(LabelSession.ViewPluNesting.WeightMin, LabelSession.ViewPluNesting.WeightMax);
            PluginMassa.IsWeightNetFake = true;
            returnPreparePrint.Invoke();
        }
    }

    /// <summary>
    /// Печать этикетки ПЛУ.
    /// </summary>
    /// <param name="template"></param>
    /// <param name="isClearBuffer"></param>
    /// <param name="isReloadTemplete"></param>
    private void PrintLabelCore(ref WsSqlTemplateModel template, bool isClearBuffer, bool isReloadTemplete)
    {
        try
        {
            // #WS-T-710 Печать этикеток. Исправление счётчика этикеток
            if (!LabelSession.PluLine.Line.Number.Equals(LabelSession.Line.Number))
                LabelSession.PluLine.Line = LabelSession.Line;
            // Инкремент счётчика этикеток.
            LabelSession.AddLineCounter();
            // Создать и сохранить этикетку из шаблона.
            (WsSqlPluLabelModel PluLabel, WsSqlPluLabelContextModel PluLabelContext) pluLabelWithContext = 
                CreateAndSavePluLabel(template);
            // Создать ШК из этикетки.
            CreateAndSaveBarCodes(pluLabelWithContext.PluLabel, pluLabelWithContext.PluLabelContext);
            // Очистить буфер печати.
            if (isClearBuffer)
            {
                LabelSession.PluginPrintTscMain?.ClearPrintBuffer();
                LabelSession.PluginPrintZebraMain?.ClearPrintBuffer();
                if (LabelSession.Line.IsShipping)
                {
                    LabelSession.PluginPrintTscShipping?.ClearPrintBuffer();
                    LabelSession.PluginPrintZebraShipping?.ClearPrintBuffer();
                }
            }
            // TODO: исправить здесь
            //// Отправить команду в принтер.
            //if (Debug.IsDevelop)
            //{
            //    // Навигация в контрол диалога Отмена/Да.
            //    WsFormNavigationUtils.NavigateToNewDialog(showNavigation,
            //        LocaleCore.Print.QuestionPrintSendCmd, true, WsEnumLogType.Question, WsEnumDialogType.CancelYes,
            //        new() { () => { }, ActionYes });
            //    void ActionYes()
            //    {
            //        // Отправить команду в принтер.
            //        LabelSession.PluginPrintMain.SendCmd(pluLabelWithContext.PluLabel);
            //    }
            //}
            //else
            //{
            //    // Отправить команду в принтер.
            //    LabelSession.PluginPrintMain.SendCmd(pluLabelWithContext.PluLabel);
            //}
            // Отправить команду в принтер.
            LabelSession.PluginPrintTscMain?.SendCmd(pluLabelWithContext.PluLabel);
            LabelSession.PluginPrintZebraMain?.SendCmd(pluLabelWithContext.PluLabel);
            // Пересоздать шаблон.
            if (isReloadTemplete)
                template = ContextManager.ContextItem.GetItemTemplateNotNullable(LabelSession.PluLine);
            // Журнал событий.
            ContextManager.ContextItem.SaveLogInformation(
                $"{LocaleCore.Scales.LabelPrint}: {pluLabelWithContext.PluLabelContext.PluNumber} | " +
                $"{pluLabelWithContext.PluLabelContext.PluName}");
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
    /// Создать и сохранить этикетку из шаблона.
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    private (WsSqlPluLabelModel, WsSqlPluLabelContextModel) CreateAndSavePluLabel(WsSqlTemplateModel template)
    {
        WsSqlPluLabelModel pluLabel = new() { PluWeighing = LabelSession.PluWeighing, PluScale = LabelSession.PluLine, 
            ProductDt = LabelSession.ProductDate };

        // Раскомментировать при повторной ошибке сериализации.
        //string str = WsDataFormatUtils.SerializeAsXmlString<WsSqlScaleModel>(LabelSession.Line, true, true);
        //pluLabel.Xml = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlScaleModel>(LabelSession.Line, true, true);
        //str = WsDataFormatUtils.SerializeAsXmlString<WsSqlPluScaleModel>(LabelSession.PluLine, true, true);
        //pluLabel.Xml = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlPluScaleModel>(LabelSession.PluLine, true, true);
        //str = WsDataFormatUtils.SerializeAsXmlString<WsSqlPluWeighingModel>(LabelSession.PluWeighing, true, true);
        //pluLabel.Xml = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlPluWeighingModel>(LabelSession.PluWeighing, true, true);
        //str = WsDataFormatUtils.SerializeAsXmlString<WsSqlPluLabelModel>(pluLabel, true, true);

        pluLabel.Xml = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlPluLabelModel>(pluLabel, true, true);
        
        XmlDocument xmlArea = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlProductionFacilityModel>(LabelSession.Area, true, true);
        pluLabel.Xml = WsDataFormatUtils.XmlMerge(pluLabel.Xml, xmlArea);

        WsSqlPluLabelContextModel pluLabelContext = new(pluLabel, LabelSession.ViewPluNesting, pluLabel.PluScale, 
            LabelSession.Area, LabelSession.PluWeighing);
        XmlDocument xmlLabelContext = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlPluLabelContextModel>
            (pluLabelContext, true, true);
        pluLabel.Xml = WsDataFormatUtils.XmlMerge(pluLabel.Xml, xmlLabelContext);

        // Патч шаблона:
        template.Data = template.Data.Replace("PluLabelModel", nameof(WsSqlPluLabelModel));
        template.Data = template.Data.Replace("PluLabelContextModel", nameof(WsSqlPluLabelContextModel));

        pluLabel.Zpl = WsDataFormatUtils.XsltTransformation(template.Data, pluLabel.Xml.OuterXml);
        pluLabel.Zpl = WsDataFormatUtils.XmlReplaceNextLine(pluLabel.Zpl);
        pluLabel.Zpl = ZplUtils.ConvertStringToHex(pluLabel.Zpl);
        // Заменить zpl-ресурсы из таблицы ресурсов шаблонов.
        _ = DataFormatUtils.PrintCmdReplaceZplResources(pluLabel.Zpl, ActionReplaceStorageMethod(pluLabel));

        // Сохранить этикетку.
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
    /// Создать ШК из этикетки.
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