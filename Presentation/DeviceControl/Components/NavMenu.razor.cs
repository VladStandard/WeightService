using Ws.DataCore.Helpers;
using WsLocalizationCore.DeviceControlModels;

namespace DeviceControl.Components;

public sealed partial class NavMenu : ComponentBase
{
    private static LocaleDeviceControl LocaleBlazor => new();
    
    private static string SidebarThemeCss =>
        DebugHelper.Instance.IsDevelop ? "side-nav-dev" : "side-nav-prod";
    
    private List<MenuSection> MenuSections { get; set; } = new()
    {
        new()
        {
            Text = LocaleBlazor.SectionDevices,
            RequiredRole = UserAccessStr.Read,
            Icon = "extension",
            SubItems = new()
            {
                new(LocaleBlazor.SectionScales, RouteUtils.SectionLines),
                new(LocaleBlazor.SectionHosts, RouteUtils.SectionHosts),
                new(LocaleCore.Print.Names, RouteUtils.SectionPrinters),
            }
        },
        new()
        {
            Text = LocaleBlazor.SectionOperations,
            RequiredRole = UserAccessStr.Read,
            Icon = "assignment",
            SubItems = new()
            {
                new(LocaleBlazor.SectionLabels, RouteUtils.SectionPlusLabels),
                new(LocaleBlazor.SectionBarCodes, RouteUtils.SectionBarCodes),
                new(LocaleBlazor.SectionWeighings, RouteUtils.SectionPlusWeightings),
                new(LocaleBlazor.SectionWeithingFactsAggregationShort, RouteUtils.SectionPlusLabelsAggr)
            }
        },
        new()
        {
            Text = LocaleBlazor.SectionReferences1C,
            RequiredRole = UserAccessStr.Read,
            Icon = "copyright",
            SubItems = new()
            {
                new(LocaleBlazor.SectionPlus, RouteUtils.SectionPlus),
                new(LocaleBlazor.SectionBoxes, RouteUtils.SectionBoxes),
                new(LocaleBlazor.SectionClips, RouteUtils.SectionClips),
                new(LocaleBlazor.SectionBundles, RouteUtils.SectionBundles),
                new(LocaleBlazor.SectionBrands, RouteUtils.SectionBrands)
            }
        },
        new()
        {
            Text = LocaleBlazor.SectionReferences,
            RequiredRole = UserAccessStr.Read,
            Icon = "description",
            SubItems = new()
            {
                new(LocaleBlazor.SectionWorkShops, RouteUtils.SectionWorkShops),
                new(LocaleBlazor.SectionProductionFacilitiesShort, RouteUtils.SectionProductionFacilities),
                new(LocaleBlazor.SectionTemplates, RouteUtils.SectionTemplates),
                new(LocaleBlazor.SectionTemplateResources, RouteUtils.SectionTemplateResources),
                new(LocaleBlazor.SectionPlusStorage, RouteUtils.SectionPlusStorage)
            }
        },
        new()
        {
            Text = LocaleCore.Menu.MenuReports,
            RequiredRole = UserAccessStr.Read,
            Icon = "build",
            SubItems = new()
            {
                new(LocaleCore.System.SystemLogsAll, RouteUtils.SectionLogs),
                new(LocaleCore.WebService.Name, RouteUtils.SectionLogsWebService)
            }
        },
        new()
        {
            Text = LocaleBlazor.SectionAdministering,
            RequiredRole = UserAccessStr.Admin,
            Icon = "people",
            SubItems = new()
            {
                new(LocaleCore.System.Users, RouteUtils.SectionAccess),
                new(LocaleCore.System.DatabaseInfo, RouteUtils.SystemDatabaseInfo),
                new(LocaleCore.Menu.MenuDbVersionHistory, RouteUtils.SectionVersions),
            }
        }
    };
}


internal class MenuItem
{
    public string Text { get; }
    public string Path { get; }

    public MenuItem(string text, string path)
    {
        Text = text;
        Path = path;
    }
}

internal class MenuSection
{
    public string Text { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;
    public string RequiredRole { get; init; } = string.Empty;
    public List<MenuItem> SubItems { get; init; } = new();
}