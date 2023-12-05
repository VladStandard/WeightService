using Ws.DataCore.Helpers;
using WsLocalizationCore.DeviceControlModels;
using Blazor.Heroicons;

namespace DeviceControl.Components.Layout;

public sealed partial class NavMenu : ComponentBase
{
    private static LocaleDeviceControl LocaleBlazor => new();
    
    private bool IsProduction { get; set; }
    
    private List<MenuSection> MenuSections { get; set; } = new()
    {
        new()
        {
            Label = LocaleBlazor.SectionDevices,
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.DeviceTablet,
            SubItems = new()
            {
                new() { Name = LocaleBlazor.SectionScales, Link = RouteUtils.SectionLines },
                new() { Name = LocaleBlazor.SectionHosts, Link = RouteUtils.SectionHosts },
                new() { Name = LocaleCore.Print.Names, Link = RouteUtils.SectionPrinters}
            }
        },
        new()
        {
            Label = LocaleBlazor.SectionOperations,
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.Clipboard,
            SubItems = new()
            {
                new() { Name = LocaleBlazor.SectionLabels, Link = RouteUtils.SectionPlusLabels },
                new() { Name = LocaleBlazor.SectionBarCodes, Link = RouteUtils.SectionBarCodes },
                new() { Name = LocaleBlazor.SectionWeighings, Link = RouteUtils.SectionPlusWeightings },
                new() { Name = LocaleBlazor.SectionWeithingFactsAggregationShort, Link = RouteUtils.SectionPlusLabelsAggr }
            }
        },
        new()
        {
            Label = LocaleBlazor.SectionReferences1C,
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.CurrencyEuro,
            SubItems = new()
            {
                new() { Name = LocaleBlazor.SectionPlus, Link = RouteUtils.SectionPlus },
                new() { Name = LocaleBlazor.SectionBoxes, Link = RouteUtils.SectionBoxes },
                new() { Name = LocaleBlazor.SectionClips, Link = RouteUtils.SectionClips },
                new() { Name = LocaleBlazor.SectionBundles, Link = RouteUtils.SectionBundles },
                new() { Name = LocaleBlazor.SectionBrands, Link = RouteUtils.SectionBrands }
            }
        },
        new()
        {
            Label = LocaleBlazor.SectionReferences,
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.BookOpen,
            SubItems = new()
            {
                new() { Name = LocaleBlazor.SectionWorkShops, Link = RouteUtils.SectionWorkShops },
                new() { Name = LocaleBlazor.SectionProductionFacilitiesShort, Link = RouteUtils.SectionProductionFacilities },
                new() { Name = LocaleBlazor.SectionTemplates, Link = RouteUtils.SectionTemplates },
                new() { Name = LocaleBlazor.SectionTemplateResources, Link = RouteUtils.SectionTemplateResources },
                new() { Name = LocaleBlazor.SectionPlusStorage, Link = RouteUtils.SectionPlusStorage }
            }
        },
        new()
        {
            Label = LocaleCore.Menu.MenuReports,
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.Wrench,
            SubItems = new()
            {
                new() { Name = LocaleCore.System.SystemLogsAll, Link = RouteUtils.SectionLogs },
                new() { Name = LocaleCore.WebService.Name, Link = RouteUtils.SectionLogsWebService }
            }
        },
        new()
        {
            Label = LocaleBlazor.SectionAdministering,
            RequiredRole = UserAccessStr.Admin,
            Icon = HeroiconName.UserGroup,
            SubItems = new()
            {
                new() { Name = LocaleCore.System.Users, Link = RouteUtils.SectionAccess },
                new() { Name = LocaleCore.System.DatabaseInfo, Link = RouteUtils.SystemDatabaseInfo },
                new() { Name = LocaleCore.Menu.MenuDbVersionHistory, Link = RouteUtils.SectionVersions }
            }
        }
    };

    protected override void OnInitialized()
    {
        IsProduction = !DebugHelper.Instance.IsDevelop;
    }
}

internal class MenuSection
{
    public string Label { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;
    public string RequiredRole { get; init; } = string.Empty;
    public List<NavMenuItemModel> SubItems { get; init; } = new();
}