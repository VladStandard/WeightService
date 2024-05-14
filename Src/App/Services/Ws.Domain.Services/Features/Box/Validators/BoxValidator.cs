namespace Ws.Domain.Services.Features.Box.Validators;

internal abstract class BoxValidator : AbstractValidator<Models.Entities.Ref1c.Box>
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