// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableDiagModels.LogsMemories;

/// <summary>
/// Table validation "diag.LOGS_MEMORIES".
/// </summary>
public sealed class WsSqlLogMemoryValidator : WsSqlTableValidator<WsSqlLogMemoryModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlLogMemoryValidator(bool isCheckIdentity) : base(isCheckIdentity, true, false)
    {
        RuleFor(item => item.SizeAppMb)
            .NotNull()
            .GreaterThanOrEqualTo((short)0);
        RuleFor(item => item.SizeFreeMb)
            .NotNull()
            .GreaterThanOrEqualTo((short)0);
        RuleFor(item => item.App)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlAppValidator(isCheckIdentity));
        RuleFor(item => item.Device)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlDeviceValidator(isCheckIdentity));
    }
}