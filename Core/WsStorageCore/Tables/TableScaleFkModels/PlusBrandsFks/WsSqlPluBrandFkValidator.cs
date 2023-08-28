namespace WsStorageCore.Tables.TableScaleFkModels.PlusBrandsFks;

/// <summary>
/// Table validation "PLUS_BRANDS_FK".
/// </summary>
public sealed class WsSqlPluBrandFkValidator : WsSqlTableValidator<WsSqlPluBrandFkModel>
{

    public WsSqlPluBrandFkValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Brand)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlBrandValidator(isCheckIdentity));
    }
}