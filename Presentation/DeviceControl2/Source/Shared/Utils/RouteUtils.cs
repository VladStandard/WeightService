namespace DeviceControl2.Source.Shared.Utils;

public static class RouteUtils
{
    # region Devices

    public const string SectionLines = "/lines";
    public const string SectionPrinters = "/printers";

    # endregion

    # region Operations

    public const string SectionLabels = "/labels";

    # endregion

    # region References

    public const string SectionWarehouses = "/warehouses";
    public const string SectionProductionSites = "/production_sites";
    public const string SectionTemplates = "/templates";
    public const string SectionTemplateResources = "/templates_resources";
    public const string SectionStorageMethods = "/storage_methods";

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
    public const string SectionRoles = "/roles";
    public const string SectionPalletMen = "/pallet_men";

    # endregion

    public const string SectionQrlQuery = "/{SearchingSectionItemId?}";
    public const string Home = "/";
}
