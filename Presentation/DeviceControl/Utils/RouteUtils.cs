namespace DeviceControl.Utils;

public static class RouteUtils
{
    # region Devices
    
    public const string SectionLines = "/lines";
    public const string SectionHosts = "/hosts";
    public const string SectionPrinters = "/printers";
    
    # endregion
    
    # region Operations

    public const string SectionLabels = "/labels";
    
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
    public const string SectionUsers = "/users";
    
    # endregion

    public const string SectionQrlQuery = "/{SearchingSectionItemId?}";
    public const string Profile = "/profile";
    public const string Home = "/";
}
