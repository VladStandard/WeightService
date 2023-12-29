namespace Ws.StorageCore.Entities.SchemaRef.Hosts;

public sealed class SqlHostValidator : SqlTableValidator<SqlHostEntity>
{
    public SqlHostValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.LoginDt)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Ip)
            .NotNull()
            .Matches(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$");
    }
}
