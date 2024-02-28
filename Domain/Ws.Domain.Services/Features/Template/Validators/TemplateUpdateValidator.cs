namespace Ws.Domain.Services.Features.Template.Validators;

internal sealed class TemplateUpdateValidator : TemplateValidator
{
    public TemplateUpdateValidator() : base()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}