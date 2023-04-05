// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.Devices;

namespace DataCore.Sql.TableDiagModels.LogsMemories;

/// <summary>
/// Table validation "diag.LOGS_MEMORIES".
/// </summary>
public sealed class LogMemoryValidator : SqlTableValidator<LogMemoryModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LogMemoryValidator() : base(true, false)
    {
        RuleFor(item => item.SizeAppMb)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo((short)0);
        RuleFor(item => item.SizeFreeMb)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo((short)0);
        RuleFor(item => item.App)
            .NotEmpty()
            .NotNull()
            .SetValidator(new AppValidator());
        RuleFor(item => item.Device)
            .NotEmpty()
            .NotNull()
            .SetValidator(new DeviceValidator());
    }
}