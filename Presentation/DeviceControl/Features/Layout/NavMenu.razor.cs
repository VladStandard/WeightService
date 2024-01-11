using Blazor.Heroicons;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Shared.Utils;

namespace DeviceControl.Features.Layout;

public sealed partial class NavMenu : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    private bool IsProduction { get; set; }

    private IEnumerable<MenuSection> MenuSections { get; set; } = [];

    protected override void OnInitialized()
    {
        IsProduction = !ConfigurationUtil.IsDevelop;
        MenuSections = CreateNavMenus();
    }
    
    private IEnumerable<MenuSection> CreateNavMenus() =>
    [
        new()
        {
            Label = Localizer["MenuDevices"],
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.DeviceTablet,
            SubItems =
            [
                new() { Name = Localizer["SectionLines"], Link = RouteUtils.SectionLines },
                new() { Name = Localizer["SectionHosts"], Link = RouteUtils.SectionHosts },
                new() { Name = Localizer["SectionPrinters"], Link = RouteUtils.SectionPrinters }
            ]
        },
        new()
        {
            Label = Localizer["MenuOperations"],
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.Clipboard,
            SubItems =
            [
                new() { Name = Localizer["SectionLabels"], Link = RouteUtils.SectionLabels }
                // new() { Name = Localizer["SectionBarcodes"], Link = RouteUtils.SectionBarCodes },
                // new() { Name = Localizer["SectionWeightings"], Link = RouteUtils.SectionPlusWeightings },
                // new() { Name = Localizer["SectionAggregatedLabels"], Link = RouteUtils.SectionPlusLabelsAggr }
            ]
        },

        new()
        {
            Label = Localizer["Menu1CReferences"],
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.CurrencyEuro,
            SubItems =
            [
                new() { Name = Localizer["SectionPLU"], Link = RouteUtils.SectionPlus },
                new() { Name = Localizer["SectionBoxes"], Link = RouteUtils.SectionBoxes },
                new() { Name = Localizer["SectionClips"], Link = RouteUtils.SectionClips },
                new() { Name = Localizer["SectionBundles"], Link = RouteUtils.SectionBundles },
                new() { Name = Localizer["SectionBrands"], Link = RouteUtils.SectionBrands }
            ]
        },

        new()
        {
            Label = Localizer["MenuReferences"],
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.BookOpen,
            SubItems =
            [
                new() { Name = Localizer["SectionWorkshops"], Link = RouteUtils.SectionWorkShops },
                new() { Name = Localizer["SectionProductionSites"], Link = RouteUtils.SectionProductionSites },
                new() { Name = Localizer["SectionTemplates"], Link = RouteUtils.SectionTemplates },
                new() { Name = Localizer["SectionTemplatesResources"], Link = RouteUtils.SectionTemplateResources },
                new() { Name = Localizer["SectionPluStorages"], Link = RouteUtils.SectionPlusStorage }
            ]
        },

        new()
        {
            Label = Localizer["MenuDiagnostics"],
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.Wrench,
            SubItems =
            [
                new() { Name = Localizer["SectionAppsLogs"], Link = RouteUtils.SectionLogs },
                new() { Name = Localizer["Section1CLogs"], Link = RouteUtils.Section1CLogs }
            ]
        },

        new()
        {
            Label = Localizer["MenuAdministration"],
            RequiredRole = UserAccessStr.Admin,
            Icon = HeroiconName.UserGroup,
            SubItems =
            [
                new() { Name = Localizer["SectionDatabase"], Link = RouteUtils.SectionDatabase }
                // new() { Name = Localizer["SectionVersions"], Link = RouteUtils.SectionVersions }
            ]
        }
    ];
        
}

internal class MenuSection
{
    public string Label { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;
    public string RequiredRole { get; init; } = string.Empty;
    public IEnumerable<NavMenuItemModel> SubItems { get; init; } = [];
}