namespace WsStorageCore.Tables.TableScaleModels.Templates;

public sealed class WsSqlTemplateValidator : WsSqlTableValidator<WsSqlTemplateModel>
{
    public WsSqlTemplateValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Title)
            .NotEmpty()
            .NotNull();
    }
}
