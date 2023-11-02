using PrinterCore.Utils;
using PrinterCore.Zpl;
using WsStorageCore.Entities.SchemaScale.BarCodes;
using WsStorageCore.Entities.SchemaScale.PlusLabels;
using WsStorageCore.Entities.SchemaScale.PlusStorageMethods;
using WsStorageCore.Entities.SchemaScale.PlusStorageMethodsFks;
using WsStorageCore.Entities.SchemaScale.PlusTemplatesFks;
using WsStorageCore.Entities.SchemaScale.Templates;
using WsStorageCore.Entities.SchemaScale.TemplatesResources;
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
    
    public bool CheckPrintIsConnectAndReadyTscMain(Label fieldWarning)
    {
        if (LabelSession.PluginPrintTscMain is null) return false;
        LabelSession.PluginPrintTscMain.ReopenTsc();
        if (LabelSession.PluginPrintTscMain.IsConnected)
            return true;
        MdInvokeControl.SetVisible(fieldWarning, true);
        string message = $"{WsLocaleCore.Print.DeviceMainIsUnavailable} {WsLocaleCore.Print.DeviceCheckConnect}";
        MdInvokeControl.SetText(fieldWarning, message);
        ContextManager.ContextItem.SaveLogError(message);
        return false;
    }
    
    public bool CheckPrintIsConnectAndReadyZebraMain(Label fieldWarning)
    {
        if (LabelSession.PluginPrintZebraMain is null) return false;
        if (LabelSession.PluginPrintZebraMain.CheckDeviceStatusZebra())
            return true;
        MdInvokeControl.SetVisible(fieldWarning, true);
        MdInvokeControl.SetText(fieldWarning, 
        $"{WsLocaleCore.Print.DeviceMainCheckStatus} {LabelSession.PluginPrintZebraMain.GetDeviceStatusZebra()}");
        ContextManager.ContextItem.SaveLogError(fieldWarning.Text);
        return false;
    }

    /// <summary>
    /// Печать этикетки.
    /// </summary>
    public void PrintLabel(Label fieldWarning, bool isClearBuffer)
    {
        // Получить шаблон этикетки.
        WsSqlTemplateEntity template = new WsSqlPluTemplateFkRepository().GetItemByPlu(LabelSession.PluLine.Plu).Template;
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
    private void PrintLabelCount(ref WsSqlTemplateEntity template, bool isClearBuffer)
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
    private void PrintLabelCore(ref WsSqlTemplateEntity template, bool isClearBuffer, bool isReloadTemplate)
    {
        try
        {
            if (!LabelSession.PluLine.Line.Number.Equals(LabelSession.Line.Number))
                LabelSession.PluLine.Line = LabelSession.Line;
            // Инкремент счётчика этикеток.
            LabelSession.AddLineCounter();
            // Создать и сохранить этикетку из шаблона.
            (WsSqlPluLabelEntity PluLabel, WsSqlPluLabelContextModel PluLabelContext) pluLabelWithContext = 
                CreateAndSavePluLabel(template);
            // Создать ШК из этикетки.
            CreateAndSaveBarCodes(pluLabelWithContext.PluLabel, pluLabelWithContext.PluLabelContext);
            // Очистить буфер печати.
            if (isClearBuffer)
            {
                LabelSession.PluginPrintTscMain?.ClearPrintBuffer();
                // LabelSession.PluginPrintZebraMain?.ClearPrintBuffer();
            }
            // Отправить команду в принтер.
            LabelSession.PluginPrintTscMain?.SendCmdToTsc(pluLabelWithContext.PluLabel);
            LabelSession.PluginPrintZebraMain?.SendCmdToZebra(pluLabelWithContext.PluLabel.Zpl);
            
            // Пересоздать шаблон.
            if (isReloadTemplate)
                template = new WsSqlPluTemplateFkRepository().GetItemByPlu(LabelSession.PluLine.Plu).Template;
        }
        catch (Exception ex)
        {
            WsFormNavigationUtils.CatchExceptionSimple(ex);
        }
    }

    /// <summary>
    /// Создать и сохранить этикетку из шаблона.
    /// </summary>
    private (WsSqlPluLabelEntity, WsSqlPluLabelContextModel) CreateAndSavePluLabel(WsSqlTemplateEntity template)
    {
        // Исправление времени продукции.
        LabelSession.ProductDate = 
            new(LabelSession.ProductDate.Year, LabelSession.ProductDate.Month, LabelSession.ProductDate.Day,
                DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

        WsSqlPluLabelEntity pluLabel = new() { PluWeighing = LabelSession.PluWeighing, PluScale = LabelSession.PluLine, 
            ProductDt = LabelSession.ProductDate };
        
        WsSqlPluLabelContextModel pluLabelContext = new(pluLabel, LabelSession.ViewPluNesting, pluLabel.PluScale, 
            LabelSession.Area, LabelSession.PluWeighing);
        XmlDocument xmlLabelContext = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlPluLabelContextModel>
            (pluLabelContext, true, true);
        
        template.Data = template.Data.Replace("PluLabelContextModel", nameof(WsSqlPluLabelContextModel));
        
        pluLabel.Zpl = WsDataFormatUtils.XsltTransformation(template.Data, xmlLabelContext.OuterXml);
        pluLabel.Zpl = WsDataFormatUtils.XmlReplaceNextLine(pluLabel.Zpl);
        pluLabel.Zpl = ZplUtils.ConvertStringToHex(pluLabel.Zpl);
        // Заменить zpl-ресурсы из таблицы ресурсов шаблонов.
        _ = MdDataFormatUtils.PrintCmdReplaceZplResources(pluLabel.Zpl, ActionReplaceStorageMethod(pluLabel));

        // Сохранить этикетку.
        new WsSqlPluLabelRepository().Save(pluLabel);

        return (pluLabel, pluLabelContext);
    }

    /// <summary>
    /// Replace temperature storage method.
    /// </summary>
    private Action<string> ActionReplaceStorageMethod(WsSqlPluLabelEntity pluLabel) =>
        zpl =>
        {
            if (ContextCache.ViewPlusStorageMethods.Any() && zpl.Contains("[@PLUS_STORAGE_METHODS_FK]"))
            {
                WsSqlTemplateResourceEntity resource = new WsSqlPluStorageMethodFkRepository().GetItemByPlu(pluLabel.PluScale.Plu).Resource;
                string resourceHex = ZplUtils.ConvertStringToHex(resource.Data.ValueUnicode);
                zpl = zpl.Replace("[@PLUS_STORAGE_METHODS_FK]", resourceHex);
            }
            pluLabel.Zpl = zpl;
        };

    /// <summary>
    /// Создать ШК из этикетки.
    /// </summary>
    private void CreateAndSaveBarCodes(WsSqlPluLabelEntity pluLabel, WsSqlPluLabelContextModel pluLabelContext)
    {
        WsSqlBarCodeEntity barCode = new(pluLabel);
        BarCode.SetBarCodeTop(barCode, pluLabelContext);
        BarCode.SetBarCodeRight(barCode, pluLabelContext);
        BarCode.SetBarCodeBottom(barCode, pluLabelContext);
        ContextManager.SqlCore.Save(barCode);
    }

    #endregion
}