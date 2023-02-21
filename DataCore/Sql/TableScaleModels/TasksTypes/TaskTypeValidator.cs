// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.TasksTypes;

/// <summary>
/// Table validation "___".
/// </summary>
public class TaskTypeValidator : SqlTableValidator<TaskTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TaskTypeValidator() : base(false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}
