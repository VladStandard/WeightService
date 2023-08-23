// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableDiagModels.Logs;

public sealed class WsSqlLogValidator : WsSqlTableValidator<WsSqlLogModel>
{
    public WsSqlLogValidator(bool isCheckIdentity) : base(isCheckIdentity, true, false)
    {
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
        RuleFor(item => item.LogType)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Message)
            .NotEmpty()
            .NotNull();
    }
}