namespace WsStorageCore.Entities.SchemaScale.PlusFks;

/// <summary>
/// Table validation "PLUS_FK".
/// </summary>
public sealed class WsSqlPluFkValidator : WsSqlTableValidator<WsSqlPluFkEntity>
{

    public WsSqlPluFkValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Parent)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator(isCheckIdentity));
    }
}