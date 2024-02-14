using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Ref.Templates;

public sealed class SqlTemplateValidator : SqlTableValidator<TemplateEntity>
{
    public SqlTemplateValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}
