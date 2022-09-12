// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "PLUS_WEIGHINGS".
/// </summary>
public class PluWeighingValidator : SqlTableValidator
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluWeighingValidator()
    {
	    RuleFor(item => ((PluWeighingModel)item).Kneading)
		    .NotEmpty()
		    .NotNull()
		    .GreaterThan(default(short));
		RuleFor(item => ((PluWeighingModel)item).PluScale)
			.NotEmpty()
			.NotNull()
			.SetValidator(new PluScaleValidator());
		RuleFor(item => ((PluWeighingModel)item).Series)
			.NotEmpty()
			.NotNull()
			.SetValidator(new ProductSeriesValidator());
		RuleFor(item => ((PluWeighingModel)item).Sscc)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((PluWeighingModel)item).NettoWeight)
			.NotEmpty()
			.NotNull()
			.NotEqual(0);
		RuleFor(item => ((PluWeighingModel)item).TareWeight)
			.NotEmpty()
			.NotNull()
			.NotEqual(0);
		RuleFor(item => ((PluWeighingModel)item).ProductDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => ((PluWeighingModel)item).RegNum)
			.NotEmpty()
			.NotNull()
			.NotEqual(0);
	}
}
