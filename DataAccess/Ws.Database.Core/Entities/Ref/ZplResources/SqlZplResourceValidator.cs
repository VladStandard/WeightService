using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Ref.ZplResources;

public sealed class SqlZplResourceValidator : SqlTableValidator<TemplateResourceEntity>
{

    public SqlZplResourceValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Zpl)
            .NotEmpty()
            .NotNull();
    }
}