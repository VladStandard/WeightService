using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Brands;

public sealed class SqlBrandValidator : SqlTableValidator<BrandEntity>
{
    public SqlBrandValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotNull()
            .MaximumLength(128);
    }
}