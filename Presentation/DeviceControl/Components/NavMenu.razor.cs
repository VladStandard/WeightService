using WsLocalizationCore.DeviceControlModels;
namespace DeviceControl.Components;

public sealed partial class NavMenu : ComponentBase
{
    private static WsLocaleDeviceControl LocaleBlazor => new();

    private static string SidebarThemeCss =>
        WsDebugHelper.Instance.IsDevelop ? "side-nav_dev" : "side-nav_prod";
    
    private List<MenuSection> MenuSections { get; set; } = new()
    {
        new MenuSection
        {
            Text = LocaleBlazor.SectionDevices,
            RequiredRole = WsUserAccessStr.Read,
            SubItems = new List<MenuItem>
            {
                new() { Text = LocaleBlazor.SectionScales, Path = WsRouteUtils.SectionLines },
                new() { Text = LocaleBlazor.SectionHosts, Path = WsRouteUtils.SectionHosts },
                new() { Text = WsLocaleCore.Print.Names, Path = WsRouteUtils.SectionPrinters }
            }
        },
        new MenuSection
        {
            Text = LocaleBlazor.SectionOperations,
            RequiredRole = WsUserAccessStr.Read,
            SubItems = new List<MenuItem>
            {
                new() { Text = LocaleBlazor.SectionLabels, Path = WsRouteUtils.SectionPlusLabels },
                new() { Text = LocaleBlazor.SectionBarCodes, Path = WsRouteUtils.SectionBarCodes },
                new() { Text = LocaleBlazor.SectionWeighings, Path = WsRouteUtils.SectionPlusWeightings },
                new() { Text = LocaleBlazor.SectionWeithingFactsAggregationShort, Path = WsRouteUtils.SectionPlusLabelsAggr }
            }
        },
        new MenuSection
        {
            Text = LocaleBlazor.SectionReferences1C,
            RequiredRole = WsUserAccessStr.Read,
            SubItems = new List<MenuItem>
            {
                new() { Text = LocaleBlazor.SectionPlus, Path = WsRouteUtils.SectionPlus },
                new() { Text = LocaleBlazor.SectionBoxes, Path = WsRouteUtils.SectionBoxes },
                new() { Text = LocaleBlazor.SectionClips, Path = WsRouteUtils.SectionClips },
                new() { Text = LocaleBlazor.SectionBundles, Path = WsRouteUtils.SectionBundles },
                new() { Text = LocaleBlazor.SectionBrands, Path = WsRouteUtils.SectionBrands }
            }
        },
        new MenuSection
        {
            Text = LocaleBlazor.SectionReferences,
            RequiredRole = WsUserAccessStr.Read,
            SubItems = new List<MenuItem>
            {
                new() { Text = LocaleBlazor.SectionOrganizations, Path = WsRouteUtils.SectionOrganizations },
                new() { Text = LocaleBlazor.SectionWorkShops, Path = WsRouteUtils.SectionWorkShops },
                new() { Text = LocaleBlazor.SectionProductionFacilitiesShort, Path = WsRouteUtils.SectionProductionFacilities },
                new() { Text = LocaleBlazor.SectionTemplates, Path = WsRouteUtils.SectionTemplates },
                new() { Text = LocaleBlazor.SectionTemplateResources, Path = WsRouteUtils.SectionTemplateResources },
                new() { Text = LocaleBlazor.SectionPlusStorage, Path = WsRouteUtils.SectionPlusStorage }
            }
        },
        new MenuSection
        {
            Text = WsLocaleCore.Menu.MenuReports,
            RequiredRole = WsUserAccessStr.Read,
            SubItems = new List<MenuItem>
            {
                new() { Text = WsLocaleCore.System.SystemLogsAll, Path = WsRouteUtils.SectionLogs },
                new() { Text = WsLocaleCore.WebService.Name, Path = WsRouteUtils.SectionLogsWebService }
            }
        },
        new MenuSection
        {
            Text = LocaleBlazor.SectionAdministering,
            RequiredRole = WsUserAccessStr.Admin,
            SubItems = new List<MenuItem>
            {
                new() { Text = WsLocaleCore.System.Users, Path = WsRouteUtils.SectionAccess },
                new() { Text = WsLocaleCore.System.DatabaseInfo, Path = WsRouteUtils.SystemDatabaseInfo },
                new() { Text = WsLocaleCore.Menu.MenuDbVersionHistory, Path = WsRouteUtils.SectionVersions },
                new() { Text = WsLocaleCore.System.SystemInfo, Path = WsRouteUtils.SystemAppInfo }
            }
        }
    };
}


internal class MenuItem
{
    public string Text { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
}

internal class MenuSection
{
    public string Text { get; set; } = string.Empty;
    public string RequiredRole { get; set; } = string.Empty;
    public List<MenuItem> SubItems { get; set; } = new();
}