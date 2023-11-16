namespace WsStorageCore.Entities.SchemaScale.PlusClipsFks;

/// <summary>
/// Валидатор таблицы связей клипс и ПЛУ.
/// </summary>
public sealed class SqlPluClipFkValidator : SqlTableValidator<SqlPluClipFkEntity>
{
    public SqlPluClipFkValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Clip)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlClipValidator(isCheckIdentity));
    }
}