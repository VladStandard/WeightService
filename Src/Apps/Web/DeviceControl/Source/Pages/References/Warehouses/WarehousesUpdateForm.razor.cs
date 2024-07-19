using Ws.DeviceControl.Models.Dto.References.Warehouses.Queries;

namespace DeviceControl.Source.Pages.References.Warehouses;

public sealed partial class WarehousesUpdateForm : SectionFormBase<WarehouseDto>
{
    #region Inject

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;

    #endregion

    protected override WarehouseDto UpdateItemAction(WarehouseDto item) =>
        throw new NotImplementedException();

    protected override Task DeleteItemAction(WarehouseDto item) =>
        throw new NotImplementedException();
}

public class WarehousesUpdateFormValidator : AbstractValidator<WarehouseDto>
{
    public WarehousesUpdateFormValidator(IStringLocalizer<ApplicationResources> localizer, IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Id1C).NotEmpty().WithName("UID 1C");
    }
}