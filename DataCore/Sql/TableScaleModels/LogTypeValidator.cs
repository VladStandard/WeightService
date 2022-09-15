// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class LogTypeValidator : SqlTableValidator<LogTypeModel>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public LogTypeValidator() : base(false, false)
	{
		RuleFor(item => (item).Number)
			.NotNull()
			.GreaterThanOrEqualTo((byte)LogTypeEnum.None)
			.LessThanOrEqualTo((byte)LogTypeEnum.Information);
		RuleFor(item => item.Icon)
			.NotEmpty()
			.NotNull();
	}
}
