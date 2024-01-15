using Ws.Shared.Validators;
using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace Ws.StorageCore.Entities.SchemaRef.Users;

public sealed class SqlUserValidator : SqlTableValidator<SqlUserEntity>
{
    public SqlUserValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.LoginDt)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}
