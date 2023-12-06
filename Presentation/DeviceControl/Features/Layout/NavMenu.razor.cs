using Blazor.Heroicons;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Ws.Shared.Utils;

namespace DeviceControl.Features.Layout;

public sealed partial class NavMenu : ComponentBase
{
    
    private bool IsProduction { get; set; }
    
    private List<MenuSection> MenuSections { get; set; } = new()
    {
        new()
        {
            Label = Locale.SectionDevices,
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.DeviceTablet,
            SubItems = new()
            {
                new() { Name = Locale.SectionScales, Link = RouteUtils.SectionLines },
                new() { Name = Locale.SectionHosts, Link = RouteUtils.SectionHosts },
                new() { Name = Locale.Names, Link = RouteUtils.SectionPrinters}
            }
        },
        new()
        {
            Label = Locale.SectionOperations,
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.Clipboard,
            SubItems = new()
            {
                new() { Name = Locale.SectionLabels, Link = RouteUtils.SectionPlusLabels },
                new() { Name = Locale.SectionBarCodes, Link = RouteUtils.SectionBarCodes },
                new() { Name = Locale.SectionWeighings, Link = RouteUtils.SectionPlusWeightings },
                new() { Name = Locale.SectionWeithingFactsAggregationShort, Link = RouteUtils.SectionPlusLabelsAggr }
            }
        },
        new()
        {
            Label = Locale.SectionReferences1C,
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.CurrencyEuro,
            SubItems = new()
            {
                new() { Name = Locale.SectionPlus, Link = RouteUtils.SectionPlus },
                new() { Name = Locale.SectionBoxes, Link = RouteUtils.SectionBoxes },
                new() { Name = Locale.SectionClips, Link = RouteUtils.SectionClips },
                new() { Name = Locale.SectionBundles, Link = RouteUtils.SectionBundles },
                new() { Name = Locale.SectionBrands, Link = RouteUtils.SectionBrands }
            }
        },
        new()
        {
            Label = Locale.SectionReferences,
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.BookOpen,
            SubItems = new()
            {
                new() { Name = Locale.SectionWorkShops, Link = RouteUtils.SectionWorkShops },
                new() { Name = Locale.SectionProductionFacilitiesShort, Link = RouteUtils.SectionProductionFacilities },
                new() { Name = Locale.SectionTemplates, Link = RouteUtils.SectionTemplates },
                new() { Name = Locale.SectionTemplateResources, Link = RouteUtils.SectionTemplateResources },
                new() { Name = Locale.SectionPlusStorage, Link = RouteUtils.SectionPlusStorage }
            }
        },
        new()
        {
            Label = Locale.MenuReports,
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.Wrench,
            SubItems = new()
            {
                new() { Name = Locale.SystemLogsAll, Link = RouteUtils.SectionLogs },
                new() { Name = Locale.Name, Link = RouteUtils.SectionLogsWebService }
            }
        },
        new()
        {
            Label = Locale.SectionAdministering,
            RequiredRole = UserAccessStr.Admin,
            Icon = HeroiconName.UserGroup,
            SubItems = new()
            {
                new() { Name = Locale.Users, Link = RouteUtils.SectionAccess },
                new() { Name = Locale.DatabaseInfo, Link = RouteUtils.SystemDatabaseInfo },
                new() { Name = Locale.MenuDbVersionHistory, Link = RouteUtils.SectionVersions }
            }
        }
    };

    protected override void OnInitialized()
    {
        IsProduction = !ConfigurationUtil.IsDevelop;
    }
}

internal class MenuSection
{
    public string Label { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;
    public string RequiredRole { get; init; } = string.Empty;
    public List<NavMenuItemModel> SubItems { get; init; } = new();
}