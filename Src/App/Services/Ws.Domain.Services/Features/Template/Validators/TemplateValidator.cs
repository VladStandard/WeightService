namespace Ws.Domain.Services.Features.Template.Validators;

internal abstract class TemplateValidator : AbstractValidator<Models.Entities.Print.Template>
{
    protected TemplateValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty();
        RuleFor(item => item.Body)
            .NotEmpty();
    }
}