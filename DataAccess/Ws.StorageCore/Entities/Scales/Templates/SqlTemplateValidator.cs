using Ws.Domain.Models.Entities.Scale;

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
