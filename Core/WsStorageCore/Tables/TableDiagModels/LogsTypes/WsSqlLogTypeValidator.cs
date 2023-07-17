// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableDiagModels.LogsTypes;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public sealed class WsSqlLogTypeValidator : WsSqlTableValidator<WsSqlLogTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isCheckIdentity"></param>
    public WsSqlLogTypeValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Number)
            .NotNull()
            .GreaterThanOrEqualTo((byte)WsEnumLogType.None)
            .LessThanOrEqualTo((byte)WsEnumLogType.Information);
        RuleFor(item => item.Icon)
            .NotEmpty()
            .NotNull();
    }
}
