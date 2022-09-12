// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class OrderValidator : SqlTableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public OrderValidator()
	{
		RuleFor(item => ((OrderModel)item).Name)
			.NotEmpty()
			.NotNull()
			.Length(1, 256);
		RuleFor(item => ((OrderModel)item).BoxCount)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(1)
			.LessThanOrEqualTo(999);
		RuleFor(item => ((OrderModel)item).PalletCount)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(1)
			.LessThanOrEqualTo(999);
	}
}
