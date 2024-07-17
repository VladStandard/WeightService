using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Boxes.Validators;

internal abstract class BoxValidator : AbstractValidator<Box>
{
    protected BoxValidator()
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Weight)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }
}