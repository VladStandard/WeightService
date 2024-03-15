using System.Security.Claims;
using Blazor.Heroicons;
using DeviceControl.Resources;
using DeviceControl2.Source.Shared.Services;
using DeviceControl2.Source.Shared.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Shared.Utils;

namespace DeviceControl2.Source.Widgets.NavMenu;

public sealed partial class NavMenu : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private StartupService StartupService { get; set; } = null!;
    [Parameter] public ClaimsPrincipal? User { get; set; }
    
    private bool IsProduction { get; set; }

    private IEnumerable<MenuSection> MenuSections { get; set; } = [];
    private string TimeOnline => $"{StartupService.TimeOnline.Days} дн {StartupService.TimeOnline.Hours} ч {StartupService.TimeOnline.Minutes} мин";
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
                new(Localizer["SectionProductionSites"], RouteUtils.SectionProductionSites),
                new(Localizer["SectionTemplates"], RouteUtils.SectionTemplates),
                new(Localizer["SectionTemplatesResources"], RouteUtils.SectionTemplateResources),
                new(Localizer["SectionPluStorages"], RouteUtils.SectionStorageMethods)
            ]
        },

        new()
        {
            Label = Localizer["MenuDiagnostics"],
            Icon = HeroiconName.Wrench,
            SubItems =
            [
                // new(Localizer["SectionAppsLogs"],RouteUtils.SectionLogs),
                new(Localizer["Section1CLogs"], RouteUtils.Section1CLogs)
            ]
        },

        new()
        {
            Label = Localizer["MenuAdministration"],
            Icon = HeroiconName.UserGroup,
            SubItems =
            [
                new(Localizer["SectionUsers"], RouteUtils.SectionUsers),
                new(Localizer["SectionPalletMen"], RouteUtils.SectionPalletMen),
                new(Localizer["SectionRoles"], RouteUtils.SectionRoles),
                new(Localizer["SectionDatabase"], RouteUtils.SectionDatabase)
            ]
        }
    ];

    private static string VerBlazor => $"v{BlazorCoreUtils.GetLibVersion()}";
}

internal class MenuSection
{
    public string Label { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;
    public string RequiredRole { get; init; } = string.Empty;
    public IEnumerable<NavMenuItemModel> SubItems { get; init; } = [];
}