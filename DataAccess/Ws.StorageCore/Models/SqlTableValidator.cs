using System;

namespace Ws.StorageCore.Models;

[DebuggerDisplay("{ToString()}")]
public class SqlTableValidator<T> : AbstractValidator<T> where T : SqlEntityBase
{
    protected SqlTableValidator(bool isCheckIdentity)
    {
        if (isCheckIdentity)
            RuleFor(item => item.Identity).SetValidator(new SqlFieldIdentityValidator());
    }
}