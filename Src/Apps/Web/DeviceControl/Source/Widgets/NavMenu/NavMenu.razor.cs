using System.Security.Claims;
using Blazor.Heroicons;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Users;
using Ws.Shared.Utils;

namespace DeviceControl.Source.Widgets.NavMenu;

public sealed partial class NavMenu : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;

    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;


    private bool IsProduction { get; set; }
    private IEnumerable<MenuSection> MenuSections { get; set; } = [];
    private User User { get; set; } = new();

    protected override void OnInitialized()
    {
        IsProduction = !ConfigurationUtil.IsDevelop;
        MenuSections = CreateNavMenus();
    }

    protected override async Task OnInitializedAsync()
    {
        ClaimsPrincipal userPrincipal = (await AuthState).User;
        if (userPrincipal is { Identity.Name: not null })
            User = UserService.GetItemByNameOrCreate(userPrincipal.Identity.Name);
    }

    private IEnumerable<MenuSection> CreateNavMenus() =>
    [
        new()
        {
            Label = Localizer["MenuDevices"],
            Icon = HeroiconName.ComputerDesktop,
            RequiredClaim = PolicyEnum.Support,
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
            RequiredClaim = PolicyEnum.Admin,
            SubItems =
            [
                new(Localizer["SectionWarehouses"], RouteUtils.SectionWarehouses),
                new(Localizer["SectionProductionSites"], RouteUtils.SectionProductionSites)
            ]
        },
        new()
        {
            Label = Localizer["MenuPrintSettings"],
            Icon = HeroiconName.Printer,
            RequiredClaim = PolicyEnum.Admin,
            SubItems =
            [
                new(Localizer["SectionTemplates"], RouteUtils.SectionTemplates),
                new(Localizer["SectionTemplateResources"], RouteUtils.SectionTemplateResources),
                new(Localizer["SectionStorageMethods"], RouteUtils.SectionStorageMethods)
            ]
        },
        new()
        {
            Label = Localizer["MenuAdministration"],
            Icon = HeroiconName.UserGroup,
            RequiredClaim = PolicyEnum.Support,
            SubItems =
            [
                new(Localizer["SectionPalletMen"], RouteUtils.SectionPalletMen, PolicyEnum.Support),
                new(Localizer["SectionUsers"], RouteUtils.SectionUsers, PolicyEnum.SupportSenior),
                new(Localizer["SectionRoles"], RouteUtils.SectionRoles, PolicyEnum.Admin)
            ]
        }
        // new()
        // {
        //     Label = Localizer["MenuDiagnostics"],
        //     Icon = HeroiconName.Wrench,
        //     RequiredClaim = PolicyEnum.Admin,
        //     SubItems =
        //     [
        //         new(Localizer["Section1CLogs"], RouteUtils.Section1CLogs),
        //         new(Localizer["SectionDatabase"], RouteUtils.SectionDatabase)
        //     ]
        // }
    ];
}

public class MenuSection
{
    public required string Label { get; init; } = string.Empty;
    public required string Icon { get; init; } = string.Empty;
    public required IEnumerable<NavMenuItemModel> SubItems { get; init; } = [];
    public string? RequiredClaim { get; init; }
}