using Ws.Shared.Validators;

namespace Ws.StorageCore.Entities.SchemaRef.Hosts;

public sealed class SqlHostValidator : SqlTableValidator<SqlHostEntity>
{
    public SqlHostValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.LoginDt)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Ip).MustBeAValidIpAddress();
    }
}
