namespace Ws.Domain.Services.Features.Templates.Validators;

internal sealed class TemplateUpdateValidator : TemplateValidator
{
    public TemplateUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}