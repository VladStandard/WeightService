namespace WsStorageCore.Tables.TableScaleModels.TemplatesResources;

/// <summary>
/// Table validation "TEMPLATES_RESOURCES".
/// </summary>
public sealed class WsSqlTemplateResourceValidator : WsSqlTableValidator<WsSqlTemplateResourceModel>
{

    public WsSqlTemplateResourceValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
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