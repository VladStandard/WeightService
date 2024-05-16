using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.ZplResources.Validators;

internal abstract class ZplResourceValidator : AbstractValidator<ZplResource>
{
    protected ZplResourceValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty();
        RuleFor(item => item.Zpl)
            .NotEmpty();
    }
}