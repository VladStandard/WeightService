using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.Lines;
using Ws.StorageCore.Entities.Ref1c.Plus;

namespace Ws.StorageCore.Entities.Ref.PlusLines;

public sealed class SqlPluLineValidator : SqlTableValidator<PluLineEntity>
{
    public SqlPluLineValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Line)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlLineValidator(isCheckIdentity));
    }
}
