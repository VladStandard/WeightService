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
    public static string SectionPlusLabels => "/section/plus_labels";
    public static string SectionPlusStorage => "/section/plus_storage";
    public static string SectionPlusNestingFks => "/section/plus_nesting";
    public static string SectionPlusWeightings => "/section/plus_weightings";
    public static string SectionPlusLabelsAggr => "/section/plus_labels_aggr";

    #endregion

    #region Прочее

    public static string SectionClips => "/section/clips";
    public static string SectionAccess => "/section/access";
    public static string SectionBarCodes => "/section/barcodes";
    public static string SectionBoxes => "/section/boxes";
    public static string SectionBrands => "/section/brands";
    public static string SectionBundles => "/section/bundles";
    public static string SectionProductionFacilities => "/section/production_facilities";
    public static string SectionTemplateResources => "/section/templates_resources";
    public static string SectionTemplates => "/section/templates";
    public static string SectionWorkShops => "/section/workshops";

    #endregion
    
    public const string SectionLines = "/section/lines";
    public const string SectionHosts = "/section/hosts";
    public static string SectionPrinters => "/section/printers";
    
    public const string Section1CLogs = "/section/1c_logs";
    public const string SectionLogs = "/section/logs";
    
    public const string Profile = "/profile";
    public const string Home = "/";
}
