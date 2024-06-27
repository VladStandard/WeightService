using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Warehouses;

namespace DeviceControl.Source.Pages.References.Warehouses;

public sealed partial class WarehousesUpdateForm : SectionFormBase<Warehouse>
{
    #region Inject

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;

    #endregion

    protected override Warehouse UpdateItemAction(Warehouse item) =>
        WarehouseService.Update(item);

    protected override Task DeleteItemAction(Warehouse item)
    {
        WarehouseService.DeleteById(item.Uid);
        return Task.CompletedTask;
    }
}

public class WarehousesUpdateFormValidator : AbstractValidator<Warehouse>
{
    public WarehousesUpdateFormValidator(IStringLocalizer<ApplicationResources> localizer, IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Uid1C).NotEmpty().WithName("UID 1C");
        RuleFor(item => item.ProductionSite).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure(string.Format(localizer["FormFieldNotSelected"], wsDataLocalizer["ColProductionSite"]));
        });
    }
}