namespace Ws.DeviceControl.Models.Features.References.ProductionSites.Commands.Create;

public sealed class ProductionSiteCreateValidator : AbstractValidator<ProductionSiteCreateDto>
{
    public ProductionSiteCreateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Address).NotEmpty().WithName(wsDataLocalizer["ColAddress"]);
    }
}