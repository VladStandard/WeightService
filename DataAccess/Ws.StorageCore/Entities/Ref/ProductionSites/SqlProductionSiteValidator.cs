using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCore.Entities.Ref.ProductionSites;

public sealed class SqlProductionSiteValidator : SqlTableValidator<ProductionSiteEntity>
{

    public SqlProductionSiteValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(150)
            .NotNull();
        RuleFor(item => item.Address)
            .NotEmpty()
            .MaximumLength(512)
            .NotNull();
    }
}
