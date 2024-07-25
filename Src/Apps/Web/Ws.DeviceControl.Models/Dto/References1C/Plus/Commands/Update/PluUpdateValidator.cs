namespace Ws.DeviceControl.Models.Dto.References1C.Plus.Commands.Update;

public sealed class PluUpdateValidator : AbstractValidator<PluUpdateDto>
{
    public PluUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.TemplateId).NotEmpty().WithName(wsDataLocalizer["ColTemplate"]);
    }
}