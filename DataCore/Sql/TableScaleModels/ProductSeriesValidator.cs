// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "___".
/// </summary>
public class ProductSeriesValidator : BaseValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public ProductSeriesValidator() : base(ColumnName.Uid, true, false)
	{
		RuleFor(item => ((ProductSeriesEntity)item).Sscc)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((ProductSeriesEntity)item).Scale)
			.NotEmpty()
			.NotNull()
			.SetValidator(new ScaleValidator());
	}
}
