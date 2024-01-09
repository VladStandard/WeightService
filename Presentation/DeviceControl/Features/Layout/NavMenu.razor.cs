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

    private List<MenuSection> MenuSections { get; set; } = new();

    protected override void OnInitialized()
    {
        IsProduction = !ConfigurationUtil.IsDevelop;
        MenuSections = CreateNavMenus();
    }
    
    private List<MenuSection> CreateNavMenus() => new()
    {
        new()
        {
            Label = Localizer["MenuDevices"],
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.DeviceTablet,
            SubItems = new()
            {
                new() { Name = Localizer["SectionLines"], Link = RouteUtils.SectionLines },
                new() { Name = Localizer["SectionHosts"], Link = RouteUtils.SectionHosts },
                new() { Name = Localizer["SectionPrinters"], Link = RouteUtils.SectionPrinters}
            }
        },
        // new()
        // {
        //     Label = Localizer["MenuOperations"],
        //     RequiredRole = UserAccessStr.Read,
        //     Icon = HeroiconName.Clipboard,
        //     SubItems = new()
        //     {
        //         new() { Name = Localizer["SectionLabels"], Link = RouteUtils.SectionPlusLabels },
        //         new() { Name = Localizer["SectionBarcodes"], Link = RouteUtils.SectionBarCodes },
        //         new() { Name = Localizer["SectionWeightings"], Link = RouteUtils.SectionPlusWeightings },
        //         new() { Name = Localizer["SectionAggregatedLabels"], Link = RouteUtils.SectionPlusLabelsAggr }
        //     }
        // },
        new()
        {
            Label = Localizer["Menu1CReferences"],
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.CurrencyEuro,
            SubItems = new()
            {
                new() { Name = Localizer["SectionPLU"], Link = RouteUtils.SectionPlus },
                new() { Name = Localizer["SectionBoxes"], Link = RouteUtils.SectionBoxes },
                new() { Name = Localizer["SectionClips"], Link = RouteUtils.SectionClips },
                new() { Name = Localizer["SectionBundles"], Link = RouteUtils.SectionBundles },
                new() { Name = Localizer["SectionBrands"], Link = RouteUtils.SectionBrands }
            }
        },
        new()
        {
            Label = Localizer["MenuReferences"],
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.BookOpen,
            SubItems = new()
            {
                new() { Name = Localizer["SectionWorkshops"], Link = RouteUtils.SectionWorkShops },
                new() { Name = Localizer["SectionProductionSites"], Link = RouteUtils.SectionProductionSites },
                new() { Name = Localizer["SectionTemplates"], Link = RouteUtils.SectionTemplates },
                new() { Name = Localizer["SectionTemplatesResources"], Link = RouteUtils.SectionTemplateResources },
                new() { Name = Localizer["SectionPluStorages"], Link = RouteUtils.SectionPlusStorage }
            }
        },
        new()
        {
            Label = Localizer["MenuDiagnostics"],
            RequiredRole = UserAccessStr.Read,
            Icon = HeroiconName.Wrench,
            SubItems = new()
            {
                new() { Name = Localizer["SectionAppsLogs"], Link = RouteUtils.SectionLogs },
                new() { Name = Localizer["Section1CLogs"], Link = RouteUtils.Section1CLogs }
            }
        },
        new()
        {
            Label = Localizer["MenuAdministration"],
            RequiredRole = UserAccessStr.Admin,
            Icon = HeroiconName.UserGroup,
            SubItems = new()
            {
                // new() { Name = Localizer["SectionUsers"], Link = RouteUtils.SectionAccess },
                new() { Name = Localizer["SectionDatabase"], Link = RouteUtils.SectionDatabase },
                // new() { Name = Localizer["SectionVersions"], Link = RouteUtils.SectionVersions }
            }
        }
    };
        
}

internal class MenuSection
{
    public string Label { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;
    public string RequiredRole { get; init; } = string.Empty;
    public List<NavMenuItemModel> SubItems { get; init; } = new();
}