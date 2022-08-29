// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class OrderValidator : TableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public OrderValidator() : base(ColumnName.Uid)
	{
		RuleFor(item => ((OrderEntity)item).Name)
			.NotEmpty()
			.NotNull()
			.Length(1, 256);
		RuleFor(item => ((OrderEntity)item).BoxCount)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(1)
			.LessThanOrEqualTo(999);
		RuleFor(item => ((OrderEntity)item).PalletCount)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(1)
			.LessThanOrEqualTo(999);
	}
}
