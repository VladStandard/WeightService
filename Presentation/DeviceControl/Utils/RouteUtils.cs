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

    #endregion
    
    # region Devices
    
    public const string SectionLines = "/lines";
    public const string SectionHosts = "/hosts";
    public const string SectionPrinters = "/printers";
    
    # endregion
    
    # region References
    
    public const string SectionWorkShops = "/workshops";
    public const string SectionPlatforms = "/platforms";
    public const string SectionTemplates = "/templates";
    public const string SectionTemplateResources = "/templates_resources";
    public const string SectionPlusStorage = "/plus_storage";
    
    # endregion
    
    # region Dignostics
    
    public const string Section1CLogs = "/1c_logs";
    public const string SectionLogs = "/logs";
    
    # endregion

    public const string SectionQrlQuery = "/{SearchingSectionItemId?}";
    public const string Profile = "/profile";
    public const string Home = "/";
}
