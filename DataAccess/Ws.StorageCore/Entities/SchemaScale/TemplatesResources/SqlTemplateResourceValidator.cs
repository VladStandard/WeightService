namespace Ws.StorageCore.Entities.SchemaScale.TemplatesResources;

public sealed class SqlTemplateResourceValidator : SqlTableValidator<SqlTemplateResourceEntity>
{

    public SqlTemplateResourceValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Type)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.DataValue)
            .NotEmpty()
            .NotNull();
    }
}