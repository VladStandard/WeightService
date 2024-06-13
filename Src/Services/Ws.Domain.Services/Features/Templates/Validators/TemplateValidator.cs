using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.Templates.Validators;

internal abstract class TemplateValidator : AbstractValidator<Template>
{
    protected TemplateValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty();
        RuleFor(item => item.Body)
            .NotEmpty();
    }
}