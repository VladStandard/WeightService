using Ws.Domain.Models.Common;
using Ws.Domain.Models.Models;

namespace Ws.StorageCore.Models;

public class SqlFieldIdentityValidator : AbstractValidator<SqlFieldIdentityModel>
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