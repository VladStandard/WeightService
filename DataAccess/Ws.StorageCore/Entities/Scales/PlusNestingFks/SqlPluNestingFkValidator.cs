using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.StorageCore.Entities.Ref1c.Boxes;
using Ws.StorageCore.Entities.Ref1c.Plus;

namespace Ws.StorageCore.Entities.Scales.PlusNestingFks;

public sealed class SqlPluNestingFkValidator : SqlTableValidator<PluNestingEntity>
{
    public SqlPluNestingFkValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluValidator(isCheckIdentity));
        RuleFor(item => item.BundleCount)
            .NotNull();
        RuleFor(item => item.Box)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlBoxValidator(isCheckIdentity));
    }
}