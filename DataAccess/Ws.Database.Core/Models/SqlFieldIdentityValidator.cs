using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Database.Core.Models;

public class SqlFieldIdentityValidator : AbstractValidator<IdentityModel>
{
    public SqlFieldIdentityValidator()
    {
        RuleFor(item => item.Id)
            .NotEmpty()
            .NotNull()
            .NotEqual(0)
            .When(item => item.Name == SqlEnumFieldIdentity.Id);
        RuleFor(item => item.Uid)
            .NotEmpty()
            .NotNull()
            .NotEqual(Guid.Empty)
            .When(item => item.Name == SqlEnumFieldIdentity.Uid);
    }
}