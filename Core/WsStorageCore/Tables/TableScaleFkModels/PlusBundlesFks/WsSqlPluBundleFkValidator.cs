namespace WsStorageCore.Tables.TableScaleFkModels.PlusBundlesFks;

/// <summary>
/// Table validation "PLUS_BUNDLES_FK".
/// </summary>
public sealed class WsSqlPluBundleFkValidator : WsSqlTableValidator<WsSqlPluBundleFkModel>
{

    public WsSqlPluBundleFkValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Bundle)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlBundleValidator(isCheckIdentity));
    }
}