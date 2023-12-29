using Ws.StorageCore.Entities.SchemaRef.Lines;

namespace Ws.StorageCore.Entities.SchemaRef.PlusLines;

public sealed class SqlPluLineValidator : SqlTableValidator<SqlPluLineEntity>
{
    public SqlPluLineValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
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
