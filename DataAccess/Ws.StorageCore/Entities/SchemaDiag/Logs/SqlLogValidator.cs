using Ws.StorageCore.Models;
namespace Ws.StorageCore.Entities.SchemaDiag.Logs;

public sealed class SqlLogValidator : SqlTableValidator<SqlLogEntity>
{
    public SqlLogValidator(bool isCheckIdentity) : base(isCheckIdentity, true, false)
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
        // RuleFor(item => item.Type)
        //     .NotEmpty()
        //     .NotNull();
        RuleFor(item => item.Message)
            .NotEmpty()
            .NotNull();
    }
}