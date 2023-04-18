// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableDiagModels.LogsWebs;

/// <summary>
/// Table validation "LOGS_WEBS".
/// </summary>
public sealed class LogWebValidator : WsSqlTableValidator<LogWebModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LogWebValidator() : base(true, false)
    {
        RuleFor(item => item.StampDt)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
        RuleFor(item => item.Version)
            .NotNull();
        RuleFor(item => item.Direction)
            .NotNull();
        RuleFor(item => item.Url)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Params)
            .NotNull();
        RuleFor(item => item.Headers)
            .NotNull();
        RuleFor(item => item.DataType)
            .NotNull();
        RuleFor(item => item.DataString)
            .NotNull();
        RuleFor(item => item.CountAll)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        RuleFor(item => item.CountSuccess)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        RuleFor(item => item.CountErrors)
            .NotNull()
            .GreaterThanOrEqualTo(0);
    }
}