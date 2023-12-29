namespace Ws.StorageCore.Entities.SchemaScale.PlusTemplatesFks;

public sealed class SqlPluTemplateFkValidator : SqlTableValidator<SqlPluTemplateFkEntity>
{

    public SqlPluTemplateFkValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
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