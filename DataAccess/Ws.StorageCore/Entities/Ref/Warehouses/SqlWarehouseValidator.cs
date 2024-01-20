using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.ProductionSites;

namespace Ws.StorageCore.Entities.Ref.Warehouses;

public sealed class SqlWarehouseValidator : SqlTableValidator<WarehouseEntity>
{
    public SqlWarehouseValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(128)
            .NotNull();
        RuleFor(item => item.ProductionSite)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlProductionSiteValidator(isCheckIdentity));
    }
}
