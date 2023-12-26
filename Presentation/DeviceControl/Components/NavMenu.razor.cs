using Ws.Shared.Utils;

namespace DeviceControl.Components;

public sealed partial class NavMenu : ComponentBase
{
    private static string SidebarThemeCss => ConfigurationUtil.IsDevelop ? "side-nav-dev" : "side-nav-prod";
    
    private List<MenuSection> MenuSections { get; set; } = new()
    {
        new()
        {
            Text = Locale.SectionDevices,
            RequiredRole = UserAccessStr.Read,
            Icon = "extension",
            SubItems = new()
            {
                new(Locale.SectionScales, RouteUtils.SectionLines),
                new(Locale.SectionHosts, RouteUtils.SectionHosts),
                new(Locale.Names, RouteUtils.SectionPrinters),
            }
        },
        new()
        {
            Text = Locale.SectionOperations,
            RequiredRole = UserAccessStr.Read,
            Icon = "assignment",
            SubItems = new()
            {
                new(Locale.SectionLabels, RouteUtils.SectionPlusLabels),
            }
        },
        new()
        {
            Text = Locale.SectionReferences1C,
            RequiredRole = UserAccessStr.Read,
            Icon = "copyright",
            SubItems = new()
            {
                new(Locale.SectionPlus, RouteUtils.SectionPlus),
                new(Locale.SectionBoxes, RouteUtils.SectionBoxes),
                new(Locale.SectionClips, RouteUtils.SectionClips),
                new(Locale.SectionBundles, RouteUtils.SectionBundles),
                new(Locale.SectionBrands, RouteUtils.SectionBrands)
            }
        },
        new()
        {
            Text = Locale.SectionReferences,
            RequiredRole = UserAccessStr.Read,
            Icon = "description",
            SubItems = new()
            {
                new(Locale.SectionWorkShops, RouteUtils.SectionWorkShops),
                new(Locale.SectionProductionFacilitiesShort, RouteUtils.SectionProductionFacilities),
                new(Locale.SectionTemplates, RouteUtils.SectionTemplates),
                new(Locale.SectionTemplateResources, RouteUtils.SectionTemplateResources),
                new(Locale.SectionPlusStorage, RouteUtils.SectionPlusStorage)
            }
        },
        new()
        {
            Text = Locale.MenuReports,
            RequiredRole = UserAccessStr.Read,
            Icon = "build",
            SubItems = new()
            {
                new(Locale.SystemLogsAll, RouteUtils.SectionLogs),
                new(Locale.Name, RouteUtils.SectionLogsWebService)
            }
        },
        new()
        {
            Text = Locale.SectionAdministering,
            RequiredRole = UserAccessStr.Admin,
            Icon = "people",
            SubItems = new()
            {
                new(Locale.Users, RouteUtils.SectionAccess),
                new(Locale.DatabaseInfo, RouteUtils.SystemDatabaseInfo),
                new(Locale.MenuDbVersionHistory, RouteUtils.SectionVersions),
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