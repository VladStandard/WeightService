using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Clips.Validators;

internal abstract class ClipValidator : AbstractValidator<Clip>
{
    protected ClipValidator()
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Weight)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }
}