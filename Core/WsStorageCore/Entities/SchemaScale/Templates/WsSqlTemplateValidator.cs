namespace WsStorageCore.Entities.SchemaScale.Templates;

public sealed class WsSqlTemplateValidator : WsSqlTableValidator<WsSqlTemplateEntity>
{
    public WsSqlTemplateValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Title)
            .NotEmpty()
            .NotNull();
    }
}
