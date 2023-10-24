namespace WsStorageCore.Entities.SchemaScale.PlusScales;

public sealed class WsSqlPluScaleValidator : WsSqlTableValidator<WsSqlPluScaleEntity>
{
    public WsSqlPluScaleValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Line)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlScaleValidator(isCheckIdentity));
    }
}
