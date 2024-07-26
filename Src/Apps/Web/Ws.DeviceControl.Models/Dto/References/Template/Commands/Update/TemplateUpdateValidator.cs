namespace Ws.DeviceControl.Models.Dto.References.Template.Commands.Update;

public sealed class TemplateUpdateValidator : AbstractValidator<TemplateUpdateDto>
{
    public TemplateUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(64)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Width)
            .InclusiveBetween((ushort)1, (ushort)512)
            .WithName(wsDataLocalizer["ColWidth"]);

        RuleFor(item => item.Height)
            .InclusiveBetween((ushort)1, (ushort)512)
            .WithName(wsDataLocalizer["ColHeight"]);

        RuleFor(item => item.Rotate)
            .Must(value => value is 0 or 90)
            .WithName(wsDataLocalizer["ColRotation"]);

        RuleFor(item => item.Body)
            .NotEmpty()
            .MaximumLength(8000).WithName(wsDataLocalizer["ColTemplate"]);
    }
}