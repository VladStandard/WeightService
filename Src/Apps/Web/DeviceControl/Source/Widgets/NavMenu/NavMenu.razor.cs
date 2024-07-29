using System.Security.Claims;
using Blazor.Heroicons;
using Ws.Shared.Extensions;

namespace DeviceControl.Source.Widgets.NavMenu;

public sealed partial class NavMenu : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;

    private bool IsProduction { get; set; }
    private IEnumerable<MenuSection> MenuSections { get; set; } = [];
    private ClaimsPrincipal User { get; set; } = default!;

    protected override void OnInitialized()
    {
        IsProduction = !ConfigurationUtil.IsDevelop;
        MenuSections = CreateNavMenus();
    }

    protected override async Task OnInitializedAsync() => User = (await AuthState).User;

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
                new(Localizer["SectionTemplateResources"], RouteUtils.SectionResources),
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
                new(Localizer["SectionUsers"], RouteUtils.SectionUsers, PolicyEnum.SeniorSupport),
            ]
        },
        new()
        {
            Label = Localizer["MenuDiagnostics"],
            Icon = HeroiconName.Wrench,
            RequiredClaim = PolicyEnum.Admin,
            SubItems =
            [
                new(Localizer["SectionMigrations"], RouteUtils.SectionMigrations),
                new(Localizer["SectionTables"], RouteUtils.SectionTables),
                new(Localizer["SectionAnalytics"], RouteUtils.SectionAnalytics)
            ]
        }
    ];

    private string GetUserShortName()
    {
        string fullName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value ?? "";
        string[] nameParts = fullName.Split(" ");
        IEnumerable<string> initialChar = nameParts.Skip(1).Select(s => string.IsNullOrEmpty(s) ? "" : $"{char.ToUpper(s[0])}.");
        return $"{nameParts[0]} {string.Join("", initialChar)}".Capitalize();
    }
}

public class MenuSection
{
    public required string Label { get; init; } = string.Empty;
    public required string Icon { get; init; } = string.Empty;
    public required IEnumerable<NavMenuItemModel> SubItems { get; init; } = [];
    public string? RequiredClaim { get; init; }
}