// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "___".
/// </summary>
public class TaskValidator : BaseValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public TaskValidator() : base(ColumnName.Uid)
	{
		RuleFor(item => ((TaskEntity)item).TaskType)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((TaskEntity)item).Scale)
			.NotEmpty()
			.NotNull();
	}
}
