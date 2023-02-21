// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.LogsWebs;

namespace DataCore.Sql.TableScaleFkModels.LogsWebsFks;

/// <summary>
/// Table validation "LOGS_WEBS_FK".
/// </summary>
public class LogWebFkValidator : SqlTableValidator<LogWebFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LogWebFkValidator() : base(true, true)
    {
        RuleFor(item => item.LogWeb)
            .NotEmpty()
            .NotNull()
            .SetValidator(new LogWebValidator());
        RuleFor(item => item.App)
            .NotEmpty()
            .NotNull()
            .SetValidator(new AppValidator());
        RuleFor(item => item.LogType)
            .NotEmpty()
            .NotNull()
            .SetValidator(new LogTypeValidator());
        RuleFor(item => item.Device)
            .NotEmpty()
            .NotNull()
            .SetValidator(new DeviceValidator());
    }
}
