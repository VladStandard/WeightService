namespace Ws.StorageCore.Entities.SchemaScale.PlusScales;

public sealed class SqlPluScaleValidator : SqlTableValidator<SqlPluScaleEntity>
{
    public SqlPluScaleValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
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
