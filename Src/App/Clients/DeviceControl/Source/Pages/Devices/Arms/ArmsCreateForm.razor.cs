using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Devices.Arms.Commands;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Arms;
using Ws.Domain.Services.Features.Printers;
using Ws.Domain.Services.Features.Warehouses;
using Ws.Shared.Enums;

namespace DeviceControl.Source.Pages.Devices.Arms;

public sealed partial class ArmsCreateForm : SectionFormBase<Arm>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;
    [Inject] private IArmService ArmService { get; set; } = default!;
    [Inject] private Redirector Redirector { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public ProductionSite ProductionSite { get; set; } = new();

    private IEnumerable<Printer> Printers { get; set; } = [];
    private IEnumerable<Warehouse> Warehouses { get; set; } = [];
    private IEnumerable<ArmType> LineTypes { get; set; } = [];
    private bool IsSeniorSupport { get; set; }
    private bool IsDeveloper { get; set; }

    private readonly CreateArmBySite _createCommand = new();

    protected override void OnInitialized()
    {
        DialogItem.Warehouse.Name = Localizer["FormWarehouseDefaultPlaceholder"];
        DialogItem.Printer.Name = Localizer["FormPrinterDefaultPlaceholder"];
        GenerateLineNumber();

        LineTypes = Enum.GetValues(typeof(ArmType)).Cast<ArmType>().ToList();
        Printers = PrinterService.GetAllByProductionSite(ProductionSite);
        Warehouses = WarehouseService.GetAllByProductionSite(ProductionSite);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        IsDeveloper = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.Developer)).Succeeded;
        IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.SupportSenior)).Succeeded;

        DialogItem.Printer = Printers.FirstOrDefault() ?? new() { Name = Localizer["FormPrinterDefaultPlaceholder"] };
        DialogItem.Warehouse = Warehouses.FirstOrDefault() ?? new();
    }

    protected override Arm CreateItemAction(Arm item) =>
        ArmService.Create(new(_createCommand));

    private void GenerateLineNumber() => DialogItem.Number = new Random().Next(10001, 100000);
}

public class LinesCreateFormValidator : AbstractValidator<CreateArmBySite>
{
    public LinesCreateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty().MaximumLength(64);
        RuleFor(item => item.Number).GreaterThan(10000).LessThan(100000);
        RuleFor(item => item.PcName).NotEmpty().Matches("^[A-Z0-9-]*$");
        RuleFor(item => item.Type).IsInEnum();
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