using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Arms;
using Ws.Domain.Services.Features.Printers;
using Ws.Domain.Services.Features.Warehouses;

namespace DeviceControl.Source.Pages.Devices.Arms;

public sealed partial class ArmsUpdateForm : SectionFormBase<Arm>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;
    [Inject] private IArmService ArmService { get; set; } = default!;
    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;

    # endregion

    [CascadingParameter] private ProductionSite UserProductionSite { get; set; } = default!;

    private IEnumerable<Warehouse> Warehouses { get; set; } = [];
    private IEnumerable<Printer> Printers { get; set; } = [];
    private IEnumerable<ArmType> LineTypes { get; set; } = [];
    private bool IsOnlyView { get; set; }
    private bool IsSeniorSupport { get; set; }
    private ProductionSite ProductionSite => DialogItem.Warehouse.ProductionSite;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Warehouses = WarehouseService.GetAllByProductionSite(ProductionSite);
        Printers = PrinterService.GetAllByProductionSite(ProductionSite);
        LineTypes = Enum.GetValues(typeof(ArmType)).Cast<ArmType>().ToList();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.SeniorSupport)).Succeeded;
        IsOnlyView = !IsSeniorSupport && !UserProductionSite.Equals(ProductionSite);
    }

    protected override Arm UpdateItemAction(Arm item) => ArmService.Update(item);

    protected override Task DeleteItemAction(Arm item)
    {
        ArmService.DeleteById(item.Uid);
        return Task.CompletedTask;
    }
}

public class LinesUpdateFormValidator : AbstractValidator<Arm>
{
    public LinesUpdateFormValidator(IStringLocalizer<ApplicationResources> localizer, IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().MaximumLength(64).WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Number).GreaterThan(10000).LessThan(100000).WithName(wsDataLocalizer["ColNumber"]);
        RuleFor(item => item.PcName).NotEmpty().Matches("^[A-Z0-9-]*$").WithName(wsDataLocalizer["ColPcName"]);
        RuleFor(item => item.Type).IsInEnum().WithName(wsDataLocalizer["ColType"]);
        RuleFor(item => item.Counter).GreaterThanOrEqualTo(0).WithName(wsDataLocalizer["ColCounter"]);;
        RuleFor(item => item.Printer).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure(string.Format(localizer["FormFieldNotSelected"], wsDataLocalizer["ColPrinter"]));
        });
        RuleFor(item => item.Warehouse).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure(string.Format(localizer["FormFieldNotSelected"], wsDataLocalizer["ColWarehouse"]));
        });
    }
}