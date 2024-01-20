using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;

namespace Ws.StorageCore.Entities.Scales.Templates;

public sealed class SqlTemplateValidator : SqlTableValidator<TemplateEntity>
{
    public SqlTemplateValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Title)
            .NotEmpty()
            .NotNull();
    }
}
