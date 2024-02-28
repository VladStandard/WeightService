namespace Ws.Domain.Services.Features.Template.Validators;

internal sealed class TemplateNewValidator : TemplateValidator
{
    public TemplateNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}