using System.Security.Claims;
using Blazor.Heroicons;
using DeviceControl.Source.Shared.Constants;

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
        IsProduction = !ConfigurationUtils.IsDevelop;
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
                new(Localizer["SectionLines"], Urls.Lines),
                new(Localizer["SectionPrinters"], Urls.Printers)
            ]
        },
        new()
        {
            Label = Localizer["MenuOperations"],
            Icon = HeroiconName.Clipboard,
            SubItems =
            [
                new(Localizer["SectionLabels"], Urls.Labels)
            ]
        },
        new()
        {
            Label = Localizer["Menu1CReferences"],
            Icon = HeroiconName.CurrencyEuro,
            SubItems =
            [
                new(Localizer["SectionPLU"], Urls.Plus),
                new(Localizer["SectionBoxes"], Urls.Boxes),
                new(Localizer["SectionClips"], Urls.Clips),
                new(Localizer["SectionBundles"], Urls.Bundles),
                new(Localizer["SectionBrands"], Urls.Brands)
            ]
        },

        new()
        {
            Label = Localizer["MenuReferences"],
            Icon = HeroiconName.BookOpen,
            RequiredClaim = PolicyEnum.SeniorSupport,
            SubItems =
            [
                new(Localizer["SectionWarehouses"], Urls.Warehouses),
                new(Localizer["SectionProductionSites"], Urls.ProductionSites)
            ]
        },
        new()
        {
            Label = Localizer["MenuPrintSettings"],
            Icon = HeroiconName.Printer,
            RequiredClaim = PolicyEnum.SeniorSupport,
            SubItems =
            [
                new(Localizer["SectionTemplates"], Urls.Templates),
                new(Localizer["SectionTemplateResources"], Urls.Resources),
            ]
        },
        new()
        {
            Label = Localizer["MenuAdministration"],
            Icon = HeroiconName.UserGroup,
            RequiredClaim = PolicyEnum.Support,
            SubItems =
            [
                new(Localizer["SectionPalletMen"], Urls.PalletMen, PolicyEnum.Support),
                new(Localizer["SectionUsers"], Urls.Users, PolicyEnum.SeniorSupport),
            ]
        },
        new()
        {
            Label = Localizer["MenuDiagnostics"],
            Icon = HeroiconName.Wrench,
            RequiredClaim = PolicyEnum.Admin,
            SubItems =
            [
                new(Localizer["SectionMigrations"], Urls.Migrations),
                new(Localizer["SectionTables"], Urls.Tables),
                new(Localizer["SectionAnalytics"], Urls.Analytics)
            ]
        }
    ];

    private string GetUserShortName()
    {
        string fullName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value ?? "";
        string[] nameParts = fullName.Split(" ");
        IEnumerable<string> initialChar = nameParts.Skip(1).Select(s => string.IsNullOrWhiteSpace(s) ? "" : $"{char.ToUpper(s[0])}.");
        return $"{nameParts[0]} {string.Concat(initialChar)}".Capitalize();
    }
}

public class MenuSection
{
    public required string Label { get; init; } = string.Empty;
    public required string Icon { get; init; } = string.Empty;
    public required IEnumerable<NavMenuItemModel> SubItems { get; init; } = [];
    public string? RequiredClaim { get; init; }
}