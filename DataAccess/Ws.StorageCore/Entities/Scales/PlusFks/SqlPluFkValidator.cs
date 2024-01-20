using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.StorageCore.Entities.Ref1c.Plus;

namespace Ws.StorageCore.Entities.Scales.PlusFks;

public sealed class SqlPluFkValidator : SqlTableValidator<PluFkEntity>
{

    public SqlPluFkValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Parent)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluValidator(isCheckIdentity));
    }
}