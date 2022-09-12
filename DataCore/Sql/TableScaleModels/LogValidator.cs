// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class LogValidator : SqlTableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public LogValidator() : base(true, false)
	{
		RuleFor(item => ((LogModel)item).Version)
			.NotNull();
		RuleFor(item => ((LogModel)item).File)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((LogModel)item).Line)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((LogModel)item).Member)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((LogModel)item).LogType)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((LogModel)item).Message)
			.NotEmpty()
			.NotNull();
	}
}
