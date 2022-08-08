// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class LogTypeValidator : AbstractValidator<LogTypeEntity>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public LogTypeValidator()
	{
		RuleFor(item => item.Number)
			.NotNull()
			.GreaterThanOrEqualTo((byte)ShareEnums.LogType.None)
			.LessThanOrEqualTo((byte)ShareEnums.LogType.Information);
		RuleFor(item => item.Icon)
			.NotEmpty()
			.NotNull();
	}
}
