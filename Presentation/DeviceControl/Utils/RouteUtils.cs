namespace DeviceControl.Utils;

public static class RouteUtils
{
    #region Администрирование
    
    public static string SystemDatabaseInfo => "/system_sql";
    public static string SectionVersions => "/system_sql_versions";
    
    #endregion
    
    #region ПЛУ

    public static string SectionPlus => "/section/plus";
    public static string SectionPlusLines => "/section/plus_lines";
    public static string SectionPlusLabels => "/section/labels";
    public static string SectionPlusStorage => "/section/plus_storage";
    public static string SectionPlusNestingFks => "/section/plus_nesting";
    public static string SectionPlusWeightings => "/section/plus_weightings";
    public static string SectionPlusLabelsAggr => "/section/plus_labels_aggr";

    #endregion

    #region Диагностика

    public static string SectionLogs => "/section/logs";
    public static string SectionLogsWebService => "/section/logs_web_service";
    
    #endregion
    
    #region Принтеры
    
    public static string SectionPrinters => "/section/printers";
    
    #endregion
    
    #region Устройства
    
    public static string SectionHosts => "/section/hosts";
    
    #endregion

    #region Прочее

    public static string SectionClips => "/section/clips";
    public static string SectionAccess => "/section/access";
    public static string SectionBarCodes => "/section/barcodes";
    public static string SectionBoxes => "/section/boxes";
    public static string SectionBrands => "/section/brands";
    public static string SectionBundles => "/section/bundles";
    public static string SectionProductionFacilities => "/section/production_facilities";
    public static string SectionLines => "/section/lines";
    public static string SectionTemplateResources => "/section/templates_resources";
    public static string SectionTemplates => "/section/templates";
    public static string SectionWorkShops => "/section/workshops";
    public static string Profile => "/profile";

    #endregion
}
