using Ws.Database.Core.Entities.Ref.PalletMen;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Core.Entities.Print.Pallets;

public sealed class SqlPalletValidator : SqlTableValidator<PalletEntity>
{

    public SqlPalletValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Barcode)
            .NotEmpty();
        RuleFor(item => item.PalletMan)
            .SetValidator(new SqlPalletManValidator(isCheckIdentity));
        RuleFor(item => item.Counter)
            .GreaterThan(0);
    }
}