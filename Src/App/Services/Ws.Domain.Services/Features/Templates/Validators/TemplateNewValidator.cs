namespace Ws.Domain.Services.Features.Templates.Validators;

internal sealed class TemplateNewValidator : TemplateValidator
{
    public TemplateNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}