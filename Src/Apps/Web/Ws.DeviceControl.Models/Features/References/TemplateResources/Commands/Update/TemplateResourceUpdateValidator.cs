namespace Ws.DeviceControl.Models.Features.References.TemplateResources.Commands.Update;

public sealed class TemplateResourceUpdateValidator : AbstractValidator<TemplateResourceUpdateDto>
{
    public TemplateResourceUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(64)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Body)
            .NotEmpty()
            .MaximumLength(8000).WithName(wsDataLocalizer["ColTemplate"]);
    }
}