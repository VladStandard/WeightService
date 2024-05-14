namespace Ws.Domain.Services.Features.Clip.Validators;

internal abstract class ClipValidator : AbstractValidator<Models.Entities.Ref1c.Clip>
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