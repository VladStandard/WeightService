namespace Ws.StorageCore.Entities.SchemaScale.PlusFks;

public sealed class SqlPluFkValidator : SqlTableValidator<SqlPluFkEntity>
{

    public SqlPluFkValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
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