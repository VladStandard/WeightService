using Ws.Domain.Models.Common;
using Ws.Domain.Models.Models;

namespace Ws.StorageCore.Common;

[DebuggerDisplay("{ToString()}")]
public abstract class SqlTableValidator<T> : AbstractValidator<T> where T : EntityBase
{
    protected SqlTableValidator(bool isCheckIdentity)
    {
        if (isCheckIdentity)
            RuleFor(item => item.Identity).SetValidator(new SqlFieldIdentityValidator());
    }
}