// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.LogsWebs;

/// <summary>
/// Table validation "LOGS_WEBS".
/// </summary>
public class LogWebValidator : SqlTableValidator<LogWebModel>
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
        RuleFor(item => item.File)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Line)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Member)
            .NotEmpty()
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
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(0);
        RuleFor(item => item.CountSuccess)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(0);
        RuleFor(item => item.CountErrors)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(0);
    }
}
