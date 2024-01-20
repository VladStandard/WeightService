using Ws.Domain.Models.Entities.Print;
using Ws.StorageCore.Entities.Ref.Lines;
using Ws.StorageCore.Entities.Ref1c.Plus;

namespace Ws.StorageCore.Entities.Print.Pallets;

public sealed class SqlPalletValidator : SqlTableValidator<PalletEntity>
{

    public SqlPalletValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Kneading)
            .NotEmpty();
        RuleFor(item => item.Plu)
            .SetValidator(new SqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Line)
            .SetValidator(new SqlLineValidator(isCheckIdentity));
        RuleFor(item => item.ProductDt)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.ExpirationDt)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(item => item.ProductDt);
    }
}