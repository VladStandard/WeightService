using Ws.Domain.Models.Entities.SchemaScale;

namespace Ws.StorageCore.Entities.Scales.TemplatesResources;

public sealed class SqlTemplateResourceValidator : SqlTableValidator<TemplateResourceEntity>
{

    public SqlTemplateResourceValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Type)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.DataValue)
            .NotEmpty()
            .NotNull();
    }
}