using DeviceControl.Source.Shared.Auth.Policies;
using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Enums;
using Ws.Domain.Services.Features.Line;
using Ws.Domain.Services.Features.Printer;
using Ws.Domain.Services.Features.ProductionSite;
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
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;

    # endregion

    private IEnumerable<PrinterEntity> PrinterEntities { get; set; } = [];
    private IEnumerable<WarehouseEntity> WarehousesEntities { get; set; } = [];
    private IEnumerable<WarehouseEntity> CachedWarehousesEntities { get; set; } = [];
    private IEnumerable<ProductionSiteEntity> ProductionSitesEntities { get; set; } = [];
    private IEnumerable<LineTypeEnum> LineTypesEntities { get; set; } = [];
    private ProductionSiteEntity ProductionSiteEntity { get; set; } = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();
        PrinterEntities = PrinterService.GetAll();
        ProductionSiteEntity = DialogItem.Warehouse.ProductionSite;
        LineTypesEntities = Enum.GetValues(typeof(LineTypeEnum)).Cast<LineTypeEnum>().ToList();

        CachedWarehousesEntities = WarehouseService.GetAll().ToList();
        ProductionSitesEntities = new HashSet<ProductionSiteEntity>(CachedWarehousesEntities.Select(warehouse => warehouse.ProductionSite)
            .Where(productionSite => !productionSite.IsNew));
        WarehousesEntities = CachedWarehousesEntities.Where(item => item.ProductionSite.Equals(ProductionSiteEntity));
    }

    protected override LineEntity UpdateItemAction(LineEntity item) =>
        LineService.Update(item);


    protected override Task DeleteItemAction(LineEntity item)
    {
        LineService.Delete(item);
        return Task.CompletedTask;
    }

    private void UpdateCurrentWarehouses()
    {
        WarehousesEntities = CachedWarehousesEntities.Where(item => item.ProductionSite.Equals(ProductionSiteEntity));
        DialogItem.Warehouse = WarehousesEntities.FirstOrDefault() ?? new();
    }

    private bool IsAdmin() => AuthorizationService.AuthorizeAsync(User, PolicyEnum.Admin).GetAwaiter().GetResult().Succeeded;
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