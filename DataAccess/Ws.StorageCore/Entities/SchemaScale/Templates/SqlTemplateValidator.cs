namespace Ws.StorageCore.Entities.SchemaScale.Templates;

public sealed class SqlTemplateValidator : SqlTableValidator<SqlTemplateEntity>
{
    public SqlTemplateValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Title)
            .NotEmpty()
            .NotNull();
    }
}
