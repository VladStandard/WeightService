// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "___".
/// </summary>
public class WorkShopValidator : SqlTableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public WorkShopValidator()
	{
		RuleFor(item => ((WorkShopModel)item).Name)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((WorkShopModel)item).ProductionFacility)
			.NotEmpty()
			.NotNull()
			.SetValidator(new ProductionFacilityValidator());
	}
}
