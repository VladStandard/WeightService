namespace Ws.DeviceControl.Models.Dto.References1C.Plus.Commands.Update;

public class PluUpdateValidator : AbstractValidator<PluUpdateDto>
{
    public PluUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.FullName).NotEmpty().WithName(wsDataLocalizer["ColFullName"]);
        RuleFor(item => item.Description).NotEmpty().WithName(wsDataLocalizer["ColDescription"]);
    }
}