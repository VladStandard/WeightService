using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Template.Validators;

internal abstract class TemplateValidator : AbstractValidator<TemplateEntity>
{
    protected TemplateValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty();
        RuleFor(item => item.Body)
            .NotEmpty();
    }
}
