namespace WsStorageCore.Entities.SchemaScale.PlusTemplatesFks;

/// <summary>
/// Table validation "PLUS_TEMPLATES_FK".
/// </summary>
public sealed class SqlPluTemplateFkValidator : SqlTableValidator<SqlPluTemplateFkEntity>
{

    public SqlPluTemplateFkValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Template)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlTemplateValidator(isCheckIdentity));
    }
}