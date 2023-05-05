// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableDiagModels.LogsTypes;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public sealed class WsSqlLogTypeValidator : WsSqlTableValidator<WsSqlLogTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlLogTypeValidator() : base(false, false)
    {
        RuleFor(item => item.Number)
            .NotNull()
            .GreaterThanOrEqualTo((byte)LogType.None)
            .LessThanOrEqualTo((byte)LogType.Information);
        RuleFor(item => item.Icon)
            .NotEmpty()
            .NotNull();
    }
}
