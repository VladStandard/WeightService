using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.StorageCore.Entities.Ref1c.Plus;
using Ws.StorageCore.Entities.Scales.Templates;

namespace Ws.StorageCore.Entities.Scales.PlusTemplatesFks;

public sealed class SqlPluTemplateFkValidator : SqlTableValidator<PluTemplateFkEntity>
{

    public SqlPluTemplateFkValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Template)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlTemplateValidator(isCheckIdentity));
    }
}