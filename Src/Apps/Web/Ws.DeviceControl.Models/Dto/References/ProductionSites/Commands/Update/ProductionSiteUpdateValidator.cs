namespace Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Update;

public class ProductionSiteUpdateValidator : AbstractValidator<ProductionSiteUpdateDto>
{
    public ProductionSiteUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Address).NotEmpty().WithName(wsDataLocalizer["ColAddress"]);
    }
}