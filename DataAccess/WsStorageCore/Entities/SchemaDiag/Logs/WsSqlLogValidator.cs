namespace WsStorageCore.Entities.SchemaDiag.Logs;

public sealed class WsSqlLogValidator : WsSqlTableValidator<WsSqlLogEntity>
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
        // RuleFor(item => item.Type)
        //     .NotEmpty()
        //     .NotNull();
        RuleFor(item => item.Message)
            .NotEmpty()
            .NotNull();
    }
}