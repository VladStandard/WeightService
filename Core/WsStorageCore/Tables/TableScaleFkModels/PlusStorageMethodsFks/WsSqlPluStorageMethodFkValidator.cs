namespace WsStorageCore.Tables.TableScaleFkModels.PlusStorageMethodsFks;

/// <summary>
/// Table validation "PLUS_STORAGE_METHODS_FK".
/// </summary>
public sealed class WsSqlPluStorageMethodFkValidator : WsSqlTableValidator<WsSqlPluStorageMethodFkModel>
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