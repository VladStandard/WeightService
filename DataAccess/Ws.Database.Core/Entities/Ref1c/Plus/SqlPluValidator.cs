using Ws.Database.Core.Entities.Ref1c.Brands;
using Ws.Database.Core.Entities.Ref1c.Bundles;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Plus;

public sealed class SqlPluValidator : SqlTableValidator<PluEntity>
{
    public SqlPluValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Description)
            .NotNull();
        RuleFor(item => item.ShelfLifeDays)
            .NotNull()
            .GreaterThanOrEqualTo(byte.MinValue)
            .LessThanOrEqualTo(byte.MaxValue);
        RuleFor(item => item.IsCheckWeight)
            .NotNull();
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(150)
            .NotNull();
        RuleFor(item => item.Number)
            .NotNull()
            .GreaterThanOrEqualTo((short)0)
            .LessThanOrEqualTo((short)10_999);
        RuleFor(item => item.FullName)
            .NotEmpty();
        RuleFor(item => item.Number)
            .NotEmpty();
        RuleFor(item => item.FullName)
            .NotEmpty();
        RuleFor(item => item.Bundle)
            .NotEmpty()
            .SetValidator(new SqlBundleValidator(isCheckIdentity));
        RuleFor(item => item.Brand)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlBrandValidator(isCheckIdentity));
    }
}