using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.ZplResource.Validators;

internal abstract class ZplResourceValidator : AbstractValidator<ZplResourceEntity>
{
    protected ZplResourceValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty();
        RuleFor(item => item.Zpl)
            .NotEmpty();
    }
}