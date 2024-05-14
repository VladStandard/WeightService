using Ws.Domain.Models.Entities.Ref1c.Plu;

namespace Ws.Domain.Services.Features.Plu.Validators;

internal abstract class PluValidator : AbstractValidator<PluEntity>
{
    protected PluValidator()
    {
        RuleFor(item => item.Description)
            .NotEmpty();
        RuleFor(item => item.ShelfLifeDays)
            .GreaterThanOrEqualTo(byte.MinValue)
            .LessThanOrEqualTo(byte.MaxValue);
        RuleFor(item => item.Name)
            .NotEmpty().MaximumLength(150);
        RuleFor(item => item.Number)
            .GreaterThanOrEqualTo((short)0)
            .LessThanOrEqualTo((short)10_999);
        RuleFor(item => item.FullName)
            .NotEmpty();
        RuleFor(item => item.Number)
            .NotEmpty();
        RuleFor(item => item.FullName)
            .NotEmpty();
        // RuleFor(item => item.Bundle)
        //     .NotEmpty()
        //     .SetValidator(new SqlBundleValidator(isCheckIdentity));
        // RuleFor(item => item.Brand)
        //     .NotEmpty()
        //     .NotNull()
        //     .SetValidator(new BrandValidator(isCheckIdentity));
    }
}