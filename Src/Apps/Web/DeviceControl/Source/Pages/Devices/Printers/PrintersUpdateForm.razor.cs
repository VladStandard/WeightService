using System.Net;
using TscZebra.Plugin.Abstractions.Enums;
using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Printers;

namespace DeviceControl.Source.Pages.Devices.Printers;

public sealed partial class PrintersUpdateForm : SectionFormBase<Printer>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;

    # endregion

    [CascadingParameter] private ProductionSite UserProductionSite { get; set; } = default!;

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
    public PrintersUpdateFormValidator(IStringLocalizer<ApplicationResources> localizer, IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().Matches("^[A-Z0-9-]*$").WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Ip).NotEmpty().NotEqual(IPAddress.Parse("127.0.0.1"));
        RuleFor(item => item.Type).IsInEnum().WithName(wsDataLocalizer["ColType"]);
        RuleFor(item => item.ProductionSite).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure(string.Format(localizer["FormFieldNotSelected"], wsDataLocalizer["ColProductionSite"]));
        });
    }
}