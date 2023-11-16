using Ws.LabelCore.Utils;
using Ws.PrinterCore.Utils;
using Ws.PrinterCore.Zpl;
using Ws.StorageCore.Entities.SchemaScale.BarCodes;
using Ws.StorageCore.Entities.SchemaScale.PlusLabels;
using Ws.StorageCore.Entities.SchemaScale.PlusStorageMethodsFks;
using Ws.StorageCore.Entities.SchemaScale.PlusTemplatesFks;
using Ws.StorageCore.Entities.SchemaScale.Templates;
using Ws.StorageCore.Entities.SchemaScale.TemplatesResources;

namespace Ws.LabelCore.Helpers;

/// <summary>
/// User session.
/// </summary>
#nullable enable
public sealed class PrintSessionHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static PrintSessionHelper _instance;
#pragma warning restore CS8618
    public static PrintSessionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private SqlContextItemHelper ContextItem => SqlContextItemHelper.Instance;
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
    private LabelSessionHelper LabelSession => LabelSessionHelper.Instance;
    private SqlBarCodeController BarCode => SqlBarCodeController.Instance;
    private SqlContextCacheHelper ContextCache => SqlContextCacheHelper.Instance;
    
    #endregion

    #region Public and private methods
    
    public bool CheckPrintIsConnectAndReadyTscMain(Label fieldWarning)
    {
        if (LabelSession.PluginPrintTscMain is null) return false;
        LabelSession.PluginPrintTscMain.ReopenTsc();
        if (LabelSession.PluginPrintTscMain.IsConnected)
            return true;
        MdInvokeControl.SetVisible(fieldWarning, true);
        string message = $"{LocaleCore.Print.DeviceMainIsUnavailable} {LocaleCore.Print.DeviceCheckConnect}";
        MdInvokeControl.SetText(fieldWarning, message);
        ContextItem.SaveLogError(message);
        return false;
    }
    
    public bool CheckPrintIsConnectAndReadyZebraMain(Label fieldWarning)
    {
        if (LabelSession.PluginPrintZebraMain is null) return false;
        if (LabelSession.PluginPrintZebraMain.CheckDeviceStatusZebra())
            return true;
        MdInvokeControl.SetVisible(fieldWarning, true);
        MdInvokeControl.SetText(fieldWarning, 
        $"{LocaleCore.Print.DeviceMainCheckStatus} {LabelSession.PluginPrintZebraMain.GetDeviceStatusZebra()}");
        ContextItem.SaveLogError(fieldWarning.Text);
        return false;
    }

    /// <summary>
    /// Печать этикетки.
    /// </summary>
    public void PrintLabel(Label fieldWarning, bool isClearBuffer)
    {
        // Получить шаблон этикетки.
        SqlTemplateEntity template = new SqlPluTemplateFkRepository().GetItemByPlu(LabelSession.PluLine.Plu).Template;
        // Проверить наличие шаблона этикетки.
        if (template.IsNew)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.LabelPrint.PluTemplateIsNotSet);
            ContextItem.SaveLogError(LocaleCore.LabelPrint.PluTemplateIsNotSet);
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
    private void PrintLabelCount(ref SqlTemplateEntity template, bool isClearBuffer)
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
    private void PrintLabelCore(ref SqlTemplateEntity template, bool isClearBuffer, bool isReloadTemplate)
    {
        try
        {
            if (!LabelSession.PluLine.Line.Number.Equals(LabelSession.Line.Number))
                LabelSession.PluLine.Line = LabelSession.Line;
            // Инкремент счётчика этикеток.
            LabelSession.AddLineCounter();
            // Создать и сохранить этикетку из шаблона.
            (SqlPluLabelEntity PluLabel, SqlPluLabelContextModel PluLabelContext) pluLabelWithContext = 
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
                template = new SqlPluTemplateFkRepository().GetItemByPlu(LabelSession.PluLine.Plu).Template;
        }
        catch (Exception ex)
        {
            FormNavigationUtils.CatchExceptionSimple(ex);
        }
    }

    /// <summary>
    /// Создать и сохранить этикетку из шаблона.
    /// </summary>
    private (SqlPluLabelEntity, SqlPluLabelContextModel) CreateAndSavePluLabel(SqlTemplateEntity template)
    {
        // Исправление времени продукции.
        LabelSession.ProductDate = 
            new(LabelSession.ProductDate.Year, LabelSession.ProductDate.Month, LabelSession.ProductDate.Day,
                DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

        SqlPluLabelEntity pluLabel = new() { PluWeighing = LabelSession.PluWeighing, PluScale = LabelSession.PluLine, 
            ProductDt = LabelSession.ProductDate };
        
        SqlPluLabelContextModel pluLabelContext = new(pluLabel, LabelSession.ViewPluNesting, pluLabel.PluScale, 
            LabelSession.Area, LabelSession.PluWeighing);
        XmlDocument xmlLabelContext = DataFormatUtils.SerializeAsXmlDocument<SqlPluLabelContextModel>
            (pluLabelContext, true, true);
        
        template.Data = template.Data.Replace("PluLabelContextModel", nameof(SqlPluLabelContextModel));
        
        pluLabel.Zpl = DataFormatUtils.XsltTransformation(template.Data, xmlLabelContext.OuterXml);
        pluLabel.Zpl = DataFormatUtils.XmlReplaceNextLine(pluLabel.Zpl);
        pluLabel.Zpl = ZplUtils.ConvertStringToHex(pluLabel.Zpl);
        // Заменить zpl-ресурсы из таблицы ресурсов шаблонов.
        _ = MdDataFormatUtils.PrintCmdReplaceZplResources(pluLabel.Zpl, ActionReplaceStorageMethod(pluLabel));

        // Сохранить этикетку.
        new SqlPluLabelRepository().Save(pluLabel);

        return (pluLabel, pluLabelContext);
    }

    /// <summary>
    /// Replace temperature storage method.
    /// </summary>
    private Action<string> ActionReplaceStorageMethod(SqlPluLabelEntity pluLabel) =>
        zpl =>
        {
            if (ContextCache.ViewPlusStorageMethods.Any() && zpl.Contains("[@PLUS_STORAGE_METHODS_FK]"))
            {
                SqlTemplateResourceEntity resource = new SqlPluStorageMethodFkRepository().GetItemByPlu(pluLabel.PluScale.Plu).Resource;
                string resourceHex = ZplUtils.ConvertStringToHex(resource.Data.ValueUnicode);
                zpl = zpl.Replace("[@PLUS_STORAGE_METHODS_FK]", resourceHex);
            }
            pluLabel.Zpl = zpl;
        };

    /// <summary>
    /// Создать ШК из этикетки.
    /// </summary>
    private void CreateAndSaveBarCodes(SqlPluLabelEntity pluLabel, SqlPluLabelContextModel pluLabelContext)
    {
        SqlBarCodeEntity barCode = new(pluLabel);
        BarCode.SetBarCodeTop(barCode, pluLabelContext);
        BarCode.SetBarCodeRight(barCode, pluLabelContext);
        BarCode.SetBarCodeBottom(barCode, pluLabelContext);
        SqlCore.Save(barCode);
    }

    #endregion
}