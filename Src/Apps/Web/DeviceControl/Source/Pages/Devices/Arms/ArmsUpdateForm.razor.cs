using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Arms;
using Ws.Domain.Services.Features.Printers;
using Ws.Domain.Services.Features.Users;
using Ws.Domain.Services.Features.Warehouses;
using Ws.Shared.Enums;

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
    [Inject] private IUserService UserService { get; set; } = default!;

    # endregion

    private IEnumerable<Warehouse> Warehouses { get; set; } = [];
    private IEnumerable<Printer> Printers { get; set; } = [];
    private IEnumerable<ArmType> LineTypes { get; set; } = [];
    private User User { get; set; } = new();
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

        if (UserPrincipal is { Identity.Name: not null })
            User = UserService.GetItemByNameOrCreate(UserPrincipal.Identity.Name);

        ProductionSite productionSite = User.ProductionSite ?? new();

        IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.SupportSenior)).Succeeded;

        IsOnlyView = !IsSeniorSupport && !productionSite.Equals(ProductionSite);
    }

    protected override Arm UpdateItemAction(Arm item) => ArmService.Update(item);

    protected override Task DeleteItemAction(Arm item)
    {
        ArmService.Delete(item);
        return Task.CompletedTask;
    }
}

public class LinesUpdateFormValidator : AbstractValidator<Arm>
{
    public LinesUpdateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty().MaximumLength(64);
        RuleFor(item => item.Number).GreaterThan(10000).LessThan(100000);
        RuleFor(item => item.PcName).NotEmpty().Matches("^[A-Z0-9-]*$");
        RuleFor(item => item.Type).IsInEnum();
        RuleFor(item => item.Counter).GreaterThanOrEqualTo(0);
        RuleFor(item => item.Printer).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure("С объектом Printer что-то не так");
        });
        RuleFor(item => item.Warehouse).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure("С объектом Warehouse что-то не так");
        });
    }
}