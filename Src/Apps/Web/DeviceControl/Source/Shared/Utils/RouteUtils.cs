namespace DeviceControl.Source.Shared.Utils;

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
    public const string SectionProductionSites = "/production-sites";
    public const string SectionTemplates = "/templates";
    public const string SectionResources = "/resources";

    # endregion

    # region References1C

    public const string SectionClips = "/clips";
    public const string SectionBoxes = "/boxes";
    public const string SectionBrands = "/brands";
    public const string SectionBundles = "/bundles";
    public const string SectionPlus = "/plus";

    # endregion

    # region Admin

    public const string SectionDatabase = "/database";
    public const string SectionUsers = "/users";
    public const string SectionRoles = "/roles";
    public const string SectionPalletMen = "/pallet-men";

    # endregion

    # region Diagnostics

    public const string SectionMigrations = "/migrations";
    public const string SectionTables = "/tables";
    public const string SectionAnalytics = "/analytics";

    # endregion

    public const string Home = "/";
    public const string Authorization = "/auth";
}