// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusWeighingsFks;

/// <summary>
/// Table validation "PLUS_WEIGHINGS".
/// </summary>
public sealed class WsSqlPluWeighingValidator : WsSqlTableValidator<WsSqlPluWeighingModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluWeighingValidator() : base(true, true)
    {
        RuleFor(item => item.Kneading)
            .NotEmpty()
            .NotNull()
            .GreaterThan(default(short));
        RuleFor(item => item.PluScale)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluScaleValidator());
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
