using System.Net;
using TscZebra.Plugin.Abstractions.Enums;
using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Printers;
using Ws.Domain.Services.Features.ProductionSites;
using Ws.Domain.Services.Features.Users;

namespace DeviceControl.Source.Pages.Devices.Printers;

public sealed partial class PrintersCreateForm : SectionFormBase<Printer>
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public ProductionSite ProductionSite { get; set; } = new();
    private IEnumerable<PrinterTypes> PrinterTypes { get; set; } = new List<PrinterTypes>();
    private User User { get; set; } = new();
    private bool IsSeniorSupport { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        PrinterTypes = Enum.GetValues(typeof(PrinterTypes)).Cast<PrinterTypes>().ToList();
        DialogItem.ProductionSite.Name = Localizer["FormProductionSiteDefaultPlaceholder"];
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (UserPrincipal is { Identity.Name: not null })
            User = UserService.GetItemByNameOrCreate(UserPrincipal.Identity.Name);
        DialogItem.ProductionSite = ProductionSite;

        IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.SupportSenior))
            .Succeeded;
    }

    protected override Printer CreateItemAction(Printer item) =>
        PrinterService.Create(item);
}

public class PrintersCreateFormValidator : AbstractValidator<Printer>
{
    public PrintersCreateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty().Matches("^[A-Z0-9-]*$");
        RuleFor(item => item.Ip).NotEmpty().NotEqual(IPAddress.Parse("127.0.0.1"));
        RuleFor(item => item.Type).IsInEnum();
        RuleFor(item => item.ProductionSite).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure("С объектом Production Site что-то не так");
        });
    }
}