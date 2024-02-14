using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.TemplatesResources;

public sealed class SqlTemplateResourceValidator : SqlTableValidator<TemplateResourceEntity>
{

    public SqlTemplateResourceValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Zpl)
            .NotEmpty()
            .NotNull();
    }
}