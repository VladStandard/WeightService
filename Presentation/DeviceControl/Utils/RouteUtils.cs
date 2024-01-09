namespace DeviceControl.Utils;

public static class RouteUtils
{
    public static string SystemDatabaseInfo => "/system_sql";
    public static string SectionVersions => "/system_sql_versions";

    // public static string SectionPlus => "/section/plus";
    public static string SectionPlusLines => "/section/plus_lines";
    public static string SectionPlusLabels => "/section/plus_labels";
    public static string SectionPlusNestingFks => "/section/plus_nesting";
    public static string SectionPlusWeightings => "/section/plus_weightings";
    public static string SectionPlusLabelsAggr => "/section/plus_labels_aggr";


    // public static string SectionClips => "/section/clips";
    // public static string SectionAccess => "/section/access";
    // public static string SectionBarCodes => "/section/barcodes";
    // public static string SectionBoxes => "/section/boxes";
    // public static string SectionBrands => "/section/brands";
    // public static string SectionBundles => "/section/bundles";

    
    # region Devices
    
    public const string SectionLines = "/lines";
    public const string SectionHosts = "/hosts";
    public const string SectionPrinters = "/printers";
    
    # endregion
    
    # region References
    
    public const string SectionWorkShops = "/workshops";
    public const string SectionProductionSites = "/production_sites";
    public const string SectionTemplates = "/templates";
    public const string SectionTemplateResources = "/templates_resources";
    public const string SectionPlusStorage = "/plus_storage";
    
    # endregion
    
    # region References1C
    
    public const string SectionClips = "/clips";
    public const string SectionBoxes = "/boxes";
    public const string SectionBrands = "/brands";
    public const string SectionBundles = "/bundles";
    public const string SectionPlus = "/plus";
    
    # endregion
    
    # region Dignostics
    
    public const string Section1CLogs = "/1c_logs";
    public const string SectionLogs = "/logs";
    
    # endregion
    
    # region Admin

    public const string SectionDatabase = "/database";
    
    # endregion

    public const string SectionQrlQuery = "/{SearchingSectionItemId?}";
    public const string Profile = "/profile";
    public const string Home = "/";
}
