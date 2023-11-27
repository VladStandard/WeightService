namespace Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;

/// <summary>
/// Валидатор таблицы "PLUS_NESTING_FK".
/// </summary>
public sealed class SqlPluNestingFkValidator : SqlTableValidator<SqlPluNestingFkEntity>
{
    public SqlPluNestingFkValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
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