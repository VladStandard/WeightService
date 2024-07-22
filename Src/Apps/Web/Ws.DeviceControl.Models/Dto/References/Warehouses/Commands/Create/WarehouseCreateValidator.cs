namespace Ws.DeviceControl.Models.Dto.References.Warehouses.Commands.Create;

public class WarehouseCreateValidator : AbstractValidator<WarehouseCreateDto>
{
    public WarehouseCreateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Id1C).NotEmpty().WithName("UID 1C");
        RuleFor(item => item.ProductionSiteId).NotEmpty().WithName(wsDataLocalizer["ColProductionSite"]);
    }
}