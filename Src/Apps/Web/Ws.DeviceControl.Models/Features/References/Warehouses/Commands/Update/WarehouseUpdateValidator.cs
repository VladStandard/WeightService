namespace Ws.DeviceControl.Models.Features.References.Warehouses.Commands.Update;

public sealed class WarehouseUpdateValidator : AbstractValidator<WarehouseUpdateDto>
{
    public WarehouseUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Id1C).NotEmpty().WithName("UID 1C");
    }
}