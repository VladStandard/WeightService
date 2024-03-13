using System.Security.Claims;
using Blazor.Heroicons;
using DeviceControl.Auth.Claims;
using DeviceControl.Resources;
using DeviceControl.Services;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Shared.Utils;

namespace DeviceControl.Features.Layout;

public sealed partial class NavMenu : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private StartupService StartupService { get; set; } = null!;
    [Parameter] public ClaimsPrincipal? User { get; set; }
    
    private bool IsProduction { get; set; }

    private IEnumerable<MenuSection> MenuSections { get; set; } = [];
    private string TimeOnline => $"{StartupService.TimeOnline.Days} дн " +
                                 $"{StartupService.TimeOnline.Hours} ч " +
                                 $"{StartupService.TimeOnline.Minutes} мин";
    
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
            Icon = HeroiconName.ComputerDesktop,
            RequiredClaim = PolicyNameUtils.Support,
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
            RequiredClaim = PolicyNameUtils.Admin,
            SubItems =
            [
                new(Localizer["SectionWarehouses"], RouteUtils.SectionWarehouses),
                new(Localizer["SectionProductionSites"], RouteUtils.SectionProductionSites),
            ]
        },
        new()
        {
            Label = Localizer["MenuPrintSettings"],
            Icon = HeroiconName.Printer,
            RequiredClaim = PolicyNameUtils.Admin,
            SubItems =
            [
                new(Localizer["SectionTemplates"], RouteUtils.SectionTemplates),
                new(Localizer["SectionTemplatesResources"], RouteUtils.SectionTemplateResources),
                new(Localizer["SectionPluStorages"], RouteUtils.SectionStorageMethods)
            ]
        },
        new()
        {
            Label = Localizer["MenuAdministration"],
            Icon = HeroiconName.UserGroup,
            RequiredClaim = PolicyNameUtils.Support,
            SubItems =
            [
                new(Localizer["SectionPalletMen"], RouteUtils.SectionPalletMen, PolicyNameUtils.Support),
                new(Localizer["SectionUsers"], RouteUtils.SectionUsers, PolicyNameUtils.Admin),
                new(Localizer["SectionRoles"], RouteUtils.SectionRoles, PolicyNameUtils.Admin),
            ]
        },
        new()
        {
            Label = Localizer["MenuDiagnostics"],
            Icon = HeroiconName.Wrench,
            RequiredClaim = PolicyNameUtils.Admin,
            SubItems =
            [
                new(Localizer["Section1CLogs"], RouteUtils.Section1CLogs),
                new(Localizer["SectionDatabase"], RouteUtils.SectionDatabase)
            ]
        },
    ];

    private static string VerBlazor => $"v{BlazorCoreUtils.GetLibVersion()}";
}

public class MenuSection
{
    public required string Label { get; init; } = string.Empty;
    public required string Icon { get; init; } = string.Empty;
    public required IEnumerable<NavMenuItemModel> SubItems { get; init; } = [];
    public string? RequiredClaim { get; init; }
}