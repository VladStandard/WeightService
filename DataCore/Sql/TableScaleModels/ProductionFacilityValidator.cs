// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "___".
/// </summary>
public class ProductionFacilityValidator : BaseValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public ProductionFacilityValidator() : base(ColumnName.Id, false, false)
	{
		RuleFor(item => ((ProductionFacilityEntity)item).Name)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((ProductionFacilityEntity)item).Address)
			.NotEmpty()
			.NotNull();
	}
}
