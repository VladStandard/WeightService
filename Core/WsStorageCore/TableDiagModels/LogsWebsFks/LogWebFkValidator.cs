// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableDiagModels.LogsWebsFks;

/// <summary>
/// Table validation "LOGS_WEBS_FK".
/// </summary>
public sealed class LogWebFkValidator : WsSqlTableValidator<LogWebFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LogWebFkValidator() : base(false, false)
    {
        RuleFor(item => item.LogWebRequest)
            .NotEmpty()
            .NotNull()
            .SetValidator(new LogWebValidator());
        RuleFor(item => item.LogWebResponse)
            .NotEmpty()
            .NotNull()
            .SetValidator(new LogWebValidator());
        RuleFor(item => item.App)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlAppValidator());
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