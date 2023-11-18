using WsLocalizationCore.DeviceControlModels;

namespace DeviceControl.Components;

public sealed partial class NavMenu : ComponentBase
{
    private static LocaleDeviceControl LocaleBlazor => new();
    
    private static string SidebarThemeCss =>
        !DebugHelper.Instance.IsDevelop ? "side-nav_dev" : "side-nav_prod";
    
    private List<MenuSection> MenuSections { get; set; } = new()
    {
        new()
        {
            Text = LocaleBlazor.SectionDevices,
            RequiredRole = UserAccessStr.Read,
            Icon = "extension",
            SubItems = new()
            {
                new() { Text = LocaleBlazor.SectionScales, Path = RouteUtils.SectionLines },
                new() { Text = LocaleBlazor.SectionHosts, Path = RouteUtils.SectionHosts },
                new() { Text = LocaleCore.Print.Names, Path = RouteUtils.SectionPrinters }
            }
        },
        new()
        {
            Text = LocaleBlazor.SectionOperations,
            RequiredRole = UserAccessStr.Read,
            Icon = "assignment",
            SubItems = new()
            {
                new() { Text = LocaleBlazor.SectionLabels, Path = RouteUtils.SectionPlusLabels },
                new() { Text = LocaleBlazor.SectionBarCodes, Path = RouteUtils.SectionBarCodes },
                new() { Text = LocaleBlazor.SectionWeighings, Path = RouteUtils.SectionPlusWeightings },
                new() { Text = LocaleBlazor.SectionWeithingFactsAggregationShort, Path = RouteUtils.SectionPlusLabelsAggr }
            }
        },
        new()
        {
            Text = LocaleBlazor.SectionReferences1C,
            RequiredRole = UserAccessStr.Read,
            Icon = "copyright",
            SubItems = new()
            {
                new() { Text = LocaleBlazor.SectionPlus, Path = RouteUtils.SectionPlus },
                new() { Text = LocaleBlazor.SectionBoxes, Path = RouteUtils.SectionBoxes },
                new() { Text = LocaleBlazor.SectionClips, Path = RouteUtils.SectionClips },
                new() { Text = LocaleBlazor.SectionBundles, Path = RouteUtils.SectionBundles },
                new() { Text = LocaleBlazor.SectionBrands, Path = RouteUtils.SectionBrands }
            }
        },
        new()
        {
            Text = LocaleBlazor.SectionReferences,
            RequiredRole = UserAccessStr.Read,
            Icon = "description",
            SubItems = new()
            {
                new() { Text = LocaleBlazor.SectionOrganizations, Path = RouteUtils.SectionOrganizations },
                new() { Text = LocaleBlazor.SectionWorkShops, Path = RouteUtils.SectionWorkShops },
                new() { Text = LocaleBlazor.SectionProductionFacilitiesShort, Path = RouteUtils.SectionProductionFacilities },
                new() { Text = LocaleBlazor.SectionTemplates, Path = RouteUtils.SectionTemplates },
                new() { Text = LocaleBlazor.SectionTemplateResources, Path = RouteUtils.SectionTemplateResources },
                new() { Text = LocaleBlazor.SectionPlusStorage, Path = RouteUtils.SectionPlusStorage }
            }
        },
        new()
        {
            Text = LocaleCore.Menu.MenuReports,
            RequiredRole = UserAccessStr.Read,
            Icon = "build",
            SubItems = new()
            {
                new() { Text = LocaleCore.System.SystemLogsAll, Path = RouteUtils.SectionLogs },
                new() { Text = LocaleCore.WebService.Name, Path = RouteUtils.SectionLogsWebService }
            }
        },
        new()
        {
            Text = LocaleBlazor.SectionAdministering,
            RequiredRole = UserAccessStr.Admin,
            Icon = "people",
            SubItems = new()
            {
                new() { Text = LocaleCore.System.Users, Path = RouteUtils.SectionAccess },
                new() { Text = LocaleCore.System.DatabaseInfo, Path = RouteUtils.SystemDatabaseInfo },
                new() { Text = LocaleCore.Menu.MenuDbVersionHistory, Path = RouteUtils.SectionVersions },
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