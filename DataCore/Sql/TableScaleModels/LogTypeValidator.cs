// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class LogTypeValidator : BaseUidValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public LogTypeValidator()
	{
		RuleFor(item => ((LogTypeEntity)item).Number)
			.NotNull()
			.GreaterThanOrEqualTo((byte)ShareEnums.LogType.None)
			.LessThanOrEqualTo((byte)ShareEnums.LogType.Information);
		RuleFor(item => ((LogTypeEntity)item).Icon)
			.NotEmpty()
			.NotNull();
	}
}
