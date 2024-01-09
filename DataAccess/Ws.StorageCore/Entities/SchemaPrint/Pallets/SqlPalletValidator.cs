using Ws.StorageCore.Entities.SchemaRef.Lines;

namespace Ws.StorageCore.Entities.SchemaPrint.Pallets;

public sealed class SqlPalletValidator : SqlTableValidator<SqlPalletEntity>
{

    public SqlPalletValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
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