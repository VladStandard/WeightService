namespace Ws.Domain.Services.Features.Template.Validators;

internal sealed class TemplateNewValidator : TemplateValidator
{
    public TemplateNewValidator() : base()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}