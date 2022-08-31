// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "ORDERS_WEIGHINGS".
/// </summary>
public class OrderWeighingValidator : TableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public OrderWeighingValidator()
	{
		RuleFor(item => ((OrderWeighingModel)item).Order)
			.NotEmpty()
			.NotNull()
			.SetValidator(new OrderValidator());
		RuleFor(item => ((OrderWeighingModel)item).PluWeighing)
			.NotEmpty()
			.NotNull()
			.SetValidator(new PluWeighingValidator());
	}
}
