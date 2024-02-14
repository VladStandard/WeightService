using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Database.Core.Common;

[DebuggerDisplay("{ToString()}")]
public abstract class SqlTableValidator<T> : AbstractValidator<T> where T : EntityBase
{
    protected SqlTableValidator(bool isCheckIdentity)
    {
        if (isCheckIdentity)
            RuleFor(item => item.IsNew).NotEqual(true);
    }
}