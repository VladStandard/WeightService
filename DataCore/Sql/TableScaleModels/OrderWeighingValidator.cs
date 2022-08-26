// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "ORDERS_WEIGHINGS".
/// </summary>
public class OrderWeighingValidator : BaseValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public OrderWeighingValidator() : base(ColumnName.Uid)
	{
		RuleFor(item => ((OrderWeighingEntity)item).Order)
			.NotEmpty()
			.NotNull()
			.SetValidator(new OrderValidator());
		RuleFor(item => ((OrderWeighingEntity)item).PluWeighing)
			.NotEmpty()
			.NotNull();
	}
}
