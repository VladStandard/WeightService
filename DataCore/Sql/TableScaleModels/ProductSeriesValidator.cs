// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "ProductSeries".
/// </summary>
public class ProductSeriesValidator : SqlTableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public ProductSeriesValidator() : base(true, false)
	{
		RuleFor(item => ((ProductSeriesModel)item).Sscc)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((ProductSeriesModel)item).Scale)
			.NotEmpty()
			.NotNull()
			.SetValidator(new ScaleValidator());
	}
}
