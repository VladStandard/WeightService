// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using FluentValidation;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "PLUS_WEIGHINGS".
/// </summary>
public class PluWeighingValidator : TableValidator
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluWeighingValidator() : base(ColumnName.Uid)
	{
	    RuleFor(item => ((PluWeighingEntity)item).Kneading)
		    .NotEmpty()
		    .NotNull()
		    .GreaterThan(default(short));
		RuleFor(item => ((PluWeighingEntity)item).PluScale)
			.NotEmpty()
			.NotNull()
			.SetValidator(new PluScaleValidator());
		RuleFor(item => ((PluWeighingEntity)item).Series)
			.NotEmpty()
			.NotNull()
			.SetValidator(new ProductSeriesValidator());
		RuleFor(item => ((PluWeighingEntity)item).Sscc)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((PluWeighingEntity)item).NettoWeight)
			.NotEmpty()
			.NotNull()
			.NotEqual(0);
		RuleFor(item => ((PluWeighingEntity)item).TareWeight)
			.NotEmpty()
			.NotNull()
			.NotEqual(0);
		RuleFor(item => ((PluWeighingEntity)item).ProductDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => ((PluWeighingEntity)item).RegNum)
			.NotEmpty()
			.NotNull()
			.NotEqual(0);
	}
}
