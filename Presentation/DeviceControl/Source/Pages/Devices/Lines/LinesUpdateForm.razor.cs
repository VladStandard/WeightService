using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Enums;
using Ws.Domain.Services.Features.Line;
using Ws.Domain.Services.Features.Printer;
using Ws.Domain.Services.Features.Warehouse;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.Devices.Lines;

public sealed partial class LinesUpdateForm : SectionFormBase<LineEntity>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;
    [Inject] private ILineService LineService { get; set; } = default!;
    [Inject] private Redirector Redirector { get; set; } = default!;

    # endregion

    private IEnumerable<PrinterEntity> PrinterEntities { get; set; } = [];
    private IEnumerable<WarehouseEntity> WarehousesEntities { get; set; } = [];
    private IEnumerable<LineTypeEnum> LineTypesEntities { get; set; } = [];

    protected override void OnInitialized()
    {
        base.OnInitialized();
        PrinterEntities = PrinterService.GetAll();
        WarehousesEntities = WarehouseService.GetAll();
        LineTypesEntities = Enum.GetValues(typeof(LineTypeEnum)).Cast<LineTypeEnum>().ToList();
    }

    protected override LineEntity UpdateItemAction(LineEntity item) =>
        LineService.Update(item);


    protected override Task DeleteItemAction(LineEntity item)
    {
        LineService.Delete(item);
        return Task.CompletedTask;
    }
}

public class LinesUpdateFormValidator : AbstractValidator<LineEntity>
{
    public LinesUpdateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Number).GreaterThan(10000);
        RuleFor(item => item.PcName).NotEmpty();
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