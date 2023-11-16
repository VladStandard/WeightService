using WsLocalizationCore.DeviceControlModels;

namespace DeviceControl.Components;

public sealed partial class NavMenu : ComponentBase
{
    private static LocaleDeviceControl LocaleBlazor => new();
    
    private static string SidebarThemeCss =>
        !DebugHelper.Instance.IsDevelop ? "side-nav_dev" : "side-nav_prod";
    
    private List<MenuSection> MenuSections { get; set; } = new()
    {
        new MenuSection
        {
            Text = LocaleBlazor.SectionDevices,
            RequiredRole = WsUserAccessStr.Read,
            Icon = "extension",
            SubItems = new List<MenuItem>
            {
                new() { Text = LocaleBlazor.SectionScales, Path = WsRouteUtils.SectionLines },
                new() { Text = LocaleBlazor.SectionHosts, Path = WsRouteUtils.SectionHosts },
                new() { Text = LocaleCore.Print.Names, Path = WsRouteUtils.SectionPrinters }
            }
        },
        new MenuSection
        {
            Text = LocaleBlazor.SectionOperations,
            RequiredRole = WsUserAccessStr.Read,
            Icon = "assignment",
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
            Icon = "copyright",
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
            Icon = "description",
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
            Text = LocaleCore.Menu.MenuReports,
            RequiredRole = WsUserAccessStr.Read,
            Icon = "build",
            SubItems = new List<MenuItem>
            {
                new() { Text = LocaleCore.System.SystemLogsAll, Path = WsRouteUtils.SectionLogs },
                new() { Text = LocaleCore.WebService.Name, Path = WsRouteUtils.SectionLogsWebService }
            }
        },
        new MenuSection
        {
            Text = LocaleBlazor.SectionAdministering,
            RequiredRole = WsUserAccessStr.Admin,
            Icon = "people",
            SubItems = new List<MenuItem>
            {
                new() { Text = LocaleCore.System.Users, Path = WsRouteUtils.SectionAccess },
                new() { Text = LocaleCore.System.DatabaseInfo, Path = WsRouteUtils.SystemDatabaseInfo },
                new() { Text = LocaleCore.Menu.MenuDbVersionHistory, Path = WsRouteUtils.SectionVersions },
                new() { Text = LocaleCore.System.SystemInfo, Path = WsRouteUtils.SystemAppInfo }
            }
        }
    };
}


internal class MenuItem
{
    public string Text { get; init; } = string.Empty;
    public string Path { get; init; } = string.Empty;
}

internal class MenuSection
{
    public string Text { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;
    public string RequiredRole { get; init; } = string.Empty;
    public List<MenuItem> SubItems { get; init; } = new();
}