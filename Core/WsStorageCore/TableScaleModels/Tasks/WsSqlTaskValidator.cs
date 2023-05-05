// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Tasks;

/// <summary>
/// Table validation "TASKS".
/// </summary>
public sealed class WsSqlTaskValidator : WsSqlTableValidator<WsSqlTaskModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTaskValidator() : base(false, false)
    {
        RuleFor(item => item.TaskType)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlTaskTypeValidator());
        RuleFor(item => item.Scale)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlScaleValidator());
    }
}
