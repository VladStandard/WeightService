using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Database.Core.Models;

public class SqlFieldIdentityValidator : AbstractValidator<IdentityModel>
{
    public SqlFieldIdentityValidator()
    {
        RuleFor(item => item.Id)
            .GreaterThan(0)
            .When(item => item.Name == SqlEnumFieldIdentity.Id);
        RuleFor(item => item.Uid)
            .NotEqual(Guid.Empty)
            .When(item => item.Name == SqlEnumFieldIdentity.Uid);
    }
}