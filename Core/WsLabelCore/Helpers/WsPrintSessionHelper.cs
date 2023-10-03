using WsStorageCore.Tables.TableRefModels.ProductionSites;
using WsStorageCore.Tables.TableScaleFkModels.PlusTemplatesFks;
using WsStorageCore.Tables.TableScaleModels.PlusLabels;

namespace WsLabelCore.Helpers;

/// <summary>
/// User session.
/// </summary>
#nullable enable
public sealed class WsPrintSessionHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static WsPrintSessionHelper _instance;
#pragma warning restore CS8618
    public static WsPrintSessionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    private WsSqlBarCodeController BarCode => WsSqlBarCodeController.Instance;
    private WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    
    #endregion

    #region Public and private methods

    /// <summary>
    /// Проверить подключение и готовность основного принтера TSC.
    /// </summary>
    public bool CheckPrintIsConnectAndReadyTscMain(Label fieldWarning)
    {
        if (LabelSession.PluginPrintTscMain is null) return false;
        LabelSession.PluginPrintTscMain.ReopenTsc();
        // Подключение.
        if (!LabelSession.PluginPrintTscMain.IsConnected)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            string message = $"{WsLocaleCore.Print.DeviceMainIsUnavailable} {WsLocaleCore.Print.DeviceCheckConnect}";
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
    public bool CheckPrintIsConnectAndReadyZebraMain(Label fieldWarning)
    {
        if (LabelSession.PluginPrintZebraMain is null) return false;
        LabelSession.PluginPrintZebraMain.ReopenZebra();
        // Подключение.
        if (!LabelSession.PluginPrintZebraMain.IsConnected)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            string message = $"{WsLocaleCore.Print.DeviceMainIsUnavailable} {WsLocaleCore.Print.DeviceCheckConnect}";
            MdInvokeControl.SetText(fieldWarning, message);
            ContextManager.ContextItem.SaveLogError(message);
            return false;
        }
        // Готовность.
        if (!LabelSession.PluginPrintZebraMain.CheckDeviceStatusZebra())
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, 
                $"{WsLocaleCore.Print.DeviceMainCheckStatus} {LabelSession.PluginPrintZebraMain.GetDeviceStatusZebra()}");
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
            string message = $"{WsLocaleCore.Print.DeviceShippingIsUnavailable} {WsLocaleCore.Print.DeviceCheckConnect}";
            MdInvokeControl.SetText(fieldWarning, message);
            ContextManager.ContextItem.SaveLogError(message);
            return false;
        }
        // Готовность.
        //if (!LabelSession.PluginPrintTscShipping.CheckDeviceStatusTsc())
        //{
        //    MdInvokeControl.SetVisible(fieldWarning, true);
        //    MdInvokeControl.SetText(fieldWarning, 
        //        $"{WsLocaleCore.Print.DeviceShippingCheckStatus} {LabelSession.PluginPrintTscShipping.GetDeviceStatusTsc()}");
        //    ContextManager.ContextItem.SaveLogError(fieldWarning.Text);
        //    return false;
        //}
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
                $"{WsLocaleCore.Print.DeviceShippingIsUnavailable} {WsLocaleCore.Print.DeviceCheckConnect}";
            MdInvokeControl.SetText(fieldWarning, message);
            ContextManager.ContextItem.SaveLogError(message);
            return false;
        }
        // Готовность.
        if (!LabelSession.PluginPrintZebraShipping.CheckDeviceStatusZebra())
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, 
                $"{WsLocaleCore.Print.DeviceShippingCheckStatus} {LabelSession.PluginPrintZebraShipping.GetDeviceStatusZebra()}");
            ContextManager.ContextItem.SaveLogError(fieldWarning.Text);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Печать этикетки.
    /// </summary>
    public void PrintLabel(Label fieldWarning, bool isClearBuffer)
    {
        // Заказ в разработке.
        if (LabelSession.Line is { IsOrder: true }) throw new("Order under construct!");
        // Получить шаблон этикетки.
        WsSqlTemplateModel template = new WsSqlPluTemplateFkRepository().GetItemByPlu(LabelSession.PluLine.Plu).Template;
        // Проверить наличие шаблона этикетки.
        if (template.IsNew)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, WsLocaleCore.LabelPrint.PluTemplateIsNotSet);
            ContextManager.ContextItem.SaveLogError(WsLocaleCore.LabelPrint.PluTemplateIsNotSet);
            return;
        }
        // Выбор типа ПЛУ.
        switch (LabelSession.PluLine.Plu.IsCheckWeight)
        {
            case true:
                PrintLabelCore(ref template, isClearBuffer, false);
                break;
            default:
                PrintLabelCount(ref template, isClearBuffer);
                break;
        
        }
        LabelSession.PluWeighing = new();
    }

    /// <summary>
    /// Печать этикетки штучной ПЛУ.
    /// </summary>
    private void PrintLabelCount(ref WsSqlTemplateModel template, bool isClearBuffer)
    {
        byte labelsCount = LabelSession.WeighingSettings.LabelsCountMain;
        if (LabelSession.PluLine.Plu.IsCheckWeight) return;
        //template.Data = template.Data.Replace("^PQ1", $"^PQ{labelsCount}");
        for (int i = 1; i <= labelsCount; i++)
            PrintLabelCore(ref template, isClearBuffer, true);
    }

    /// <summary>
    /// Печать этикетки ПЛУ.
    /// </summary>
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
            LabelSession.PluginPrintTscMain?.SendCmdToTsc(pluLabelWithContext.PluLabel);
            LabelSession.PluginPrintZebraMain?.SendCmdToZebra(pluLabelWithContext.PluLabel.Zpl);
            // Пересоздать шаблон.
            if (isReloadTemplete)
                template = new WsSqlPluTemplateFkRepository().GetItemByPlu(LabelSession.PluLine.Plu).Template;
            // Журнал событий.
            ContextManager.ContextItem.SaveLogInformation(
                $"{WsLocaleCore.LabelPrint.LabelPrint}: {pluLabelWithContext.PluLabelContext.PluNumber} | " +
                $"{pluLabelWithContext.PluLabelContext.PluName}");
        }
        catch (Exception ex)
        {
            WsFormNavigationUtils.CatchExceptionSimple(ex);
        }
    }

    /// <summary>
    /// Создать и сохранить этикетку из шаблона.
    /// </summary>
    private (WsSqlPluLabelModel, WsSqlPluLabelContextModel) CreateAndSavePluLabel(WsSqlTemplateModel template)
    {
        // Исправление времени продукции.
        LabelSession.ProductDate = 
            new(LabelSession.ProductDate.Year, LabelSession.ProductDate.Month, LabelSession.ProductDate.Day,
                DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

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
        
        XmlDocument xmlArea = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlProductionSiteModel>(LabelSession.Area, true, true);
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
        _ = MdDataFormatUtils.PrintCmdReplaceZplResources(pluLabel.Zpl, ActionReplaceStorageMethod(pluLabel));

        // Сохранить этикетку.
        ContextManager.PluLabelRepository.Save(pluLabel);

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
                WsSqlTemplateResourceModel resource = 
                    ContextManager.SqlPluStorageMethodFkRepository.GetItemByPlu(pluLabel.PluScale.Plu).Resource;
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
        ContextManager.SqlCore.Save(barCode);
    }

    #endregion
}