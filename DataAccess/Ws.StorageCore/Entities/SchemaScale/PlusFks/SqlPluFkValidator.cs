using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Models;
namespace Ws.StorageCore.Entities.SchemaScale.PlusFks;

/// <summary>
/// Table validation "PLUS_FK".
/// </summary>
public sealed class SqlPluFkValidator : SqlTableValidator<SqlPluFkEntity>
{

    public SqlPluFkValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
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