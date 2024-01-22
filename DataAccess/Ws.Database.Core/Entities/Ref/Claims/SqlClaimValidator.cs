using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Claims;

public sealed class SqlClaimValidator : SqlTableValidator<ClaimEntity>
{
    public SqlClaimValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}
