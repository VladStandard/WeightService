namespace WsStorageCore.Tables.TableScaleModels.PlusWeighings;

/// <summary>
/// Table validation "PLUS_WEIGHTINGS".
/// </summary>
public sealed class WsSqlPluWeighingValidator : WsSqlTableValidator<WsSqlPluWeighingModel>
{

    public WsSqlPluWeighingValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Kneading)
            .NotEmpty()
            .NotNull()
            .GreaterThan(default(short));
        RuleFor(item => item.PluScale)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluScaleValidator(isCheckIdentity));
        RuleFor(item => item.NettoWeight)
            .NotEmpty()
            .NotNull()
            .NotEqual(0);
        RuleFor(item => item.WeightTare)
            .NotNull();
    }
}
