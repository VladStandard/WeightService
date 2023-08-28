namespace WsStorageCore.Tables.TableScaleModels.PlusScales;

public sealed class WsSqlPluScaleValidator : WsSqlTableValidator<WsSqlPluScaleModel>
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
