using Ws.Shared.Validators;

namespace Ws.StorageCore.Entities.SchemaRef.Claims;

public sealed class SqlClaimValidator : SqlTableValidator<SqlClaimEntity>
{
    public SqlClaimValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}
