using System.Net;
using TscZebra.Plugin.Abstractions.Enums;
using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Printers;
using Ws.Domain.Services.Features.ProductionSites;
using Ws.Domain.Services.Features.Users;

namespace DeviceControl.Source.Pages.Devices.Printers;

public sealed partial class PrintersUpdateForm : SectionFormBase<Printer>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;

    [CascadingParameter] private ProductionSite UserProductionSite { get; set; } = default!;

    # endregion

    private IEnumerable<PrinterTypes> PrinterTypes { get; set; } = new List<PrinterTypes>();
    private bool IsOnlyView { get; set; }
    private bool IsSeniorSupport { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        PrinterTypes = Enum.GetValues(typeof(PrinterTypes)).Cast<PrinterTypes>().ToList();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.SeniorSupport)).Succeeded;
        IsOnlyView = !IsSeniorSupport && !UserProductionSite.Equals(DialogItem.ProductionSite);
    }

    protected override Printer UpdateItemAction(Printer item) =>
        PrinterService.Update(item);

    protected override Task DeleteItemAction(Printer item)
    {
        PrinterService.DeleteById(item.Uid);
        return Task.CompletedTask;
    }
}

public class PrintersUpdateFormValidator : AbstractValidator<Printer>
{
    public PrintersUpdateFormValidator()
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