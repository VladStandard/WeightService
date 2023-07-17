// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.TasksTypes;

/// <summary>
/// Table validation "TASKS_TYPES".
/// </summary>
public sealed class WsSqlTaskTypeValidator : WsSqlTableValidator<WsSqlTaskTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isCheckIdentity"></param>
    public WsSqlTaskTypeValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}
