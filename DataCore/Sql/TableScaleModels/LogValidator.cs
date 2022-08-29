// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class LogValidator : TableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public LogValidator() : base(ColumnName.Uid, true, false)
	{
		RuleFor(item => ((LogEntity)item).Version)
			.NotNull();
		RuleFor(item => ((LogEntity)item).File)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((LogEntity)item).Line)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((LogEntity)item).Member)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((LogEntity)item).LogType)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((LogEntity)item).Message)
			.NotEmpty()
			.NotNull();
	}
}
