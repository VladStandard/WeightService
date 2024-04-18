using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Clip.Validators;

internal abstract class ClipValidator : AbstractValidator<ClipEntity>
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