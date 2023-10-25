namespace WsStorageCore.Entities.SchemaScale.PlusStorageMethodsFks;

/// <summary>
/// Table validation "PLUS_STORAGE_METHODS_FK".
/// </summary>
public sealed class WsSqlPluStorageMethodFkValidator : WsSqlTableValidator<WsSqlPluStorageMethodFkEntity>
{

    public WsSqlPluStorageMethodFkValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Method)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluStorageMethodValidator(isCheckIdentity));
        RuleFor(item => item.Resource)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlTemplateResourceValidator(isCheckIdentity));
    }
}