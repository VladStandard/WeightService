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
            Icon = HeroiconName.DeviceTablet,
            SubItems =
            [
                new(Localizer["SectionLines"], RouteUtils.SectionLines),
                new(Localizer["SectionPrinters"], RouteUtils.SectionPrinters)
            ]
        },
        new()
        {
            Label = Localizer["MenuOperations"],
            Icon = HeroiconName.Clipboard,
            SubItems =
            [
                new(Localizer["SectionLabels"], RouteUtils.SectionLabels)
            ]
        },
        new()
        {
            Label = Localizer["Menu1CReferences"],
            Icon = HeroiconName.CurrencyEuro,
            SubItems =
            [
                new(Localizer["SectionPLU"], RouteUtils.SectionPlus),
                new(Localizer["SectionBoxes"], RouteUtils.SectionBoxes),
                new(Localizer["SectionClips"], RouteUtils.SectionClips),
                new(Localizer["SectionBundles"], RouteUtils.SectionBundles),
                new(Localizer["SectionBrands"], RouteUtils.SectionBrands)
            ]
        },

        new()
        {
            Label = Localizer["MenuReferences"],
            Icon = HeroiconName.BookOpen,
            SubItems =
            [
                new(Localizer["SectionWarehouses"], RouteUtils.SectionWarehouses),
                new(Localizer["SectionProductionSites"],RouteUtils.SectionProductionSites),
                new(Localizer["SectionTemplates"], RouteUtils.SectionTemplates),
                new(Localizer["SectionTemplatesResources"], RouteUtils.SectionTemplateResources),
                new(Localizer["SectionPluStorages"], RouteUtils.SectionPlusStorage)
            ]
        },

        new()
        {
            Label = Localizer["MenuDiagnostics"],
            Icon = HeroiconName.Wrench,
            SubItems =
            [
                new(Localizer["SectionAppsLogs"],RouteUtils.SectionLogs),
                new(Localizer["Section1CLogs"], RouteUtils.Section1CLogs)
            ]
        },

        new()
        {
            Label = Localizer["MenuAdministration"],
            Icon = HeroiconName.UserGroup,
            SubItems =
            [
                new(Localizer["SectionUsers"],RouteUtils.SectionUsers),
                new(Localizer["SectionRoles"],RouteUtils.SectionRoles),
                new(Localizer["SectionDatabase"],RouteUtils.SectionDatabase)
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
