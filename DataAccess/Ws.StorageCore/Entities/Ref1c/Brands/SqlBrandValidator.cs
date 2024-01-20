using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.StorageCore.Entities.Ref1c.Brands;

public sealed class SqlBrandValidator : SqlTableValidator<BrandEntity>
{
    public SqlBrandValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotNull()
            .MaximumLength(128);
        RuleFor(item => item.Code)
            .NotEmpty()
            .NotNull()
            .Length(9);
    }
}