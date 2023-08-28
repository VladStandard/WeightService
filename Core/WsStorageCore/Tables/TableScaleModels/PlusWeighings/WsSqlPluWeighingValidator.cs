namespace WsStorageCore.Tables.TableScaleModels.PlusWeighings;

/// <summary>
/// Table validation "PLUS_WEIGHINGS".
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
        //RuleFor(item => item.Series)
        //	.NotEmpty()
        //	.NotNull()
        //	.SetValidator(new ProductSeriesValidator());
        //RuleFor(item => item.Sscc)
        //	.NotEmpty()
        //	.NotNull();
        RuleFor(item => item.NettoWeight)
            .NotEmpty()
            .NotNull()
            .NotEqual(0);
        RuleFor(item => item.WeightTare)
            //.NotEmpty()
            .NotNull();
        //.NotEqual(0);
        //RuleFor(item => item.RegNum)
        //	.NotEmpty()
        //	.NotNull()
        //	.NotEqual(0);
    }
}
