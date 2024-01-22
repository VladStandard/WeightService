using Ws.Database.Core.Entities.Ref.ProductionSites;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Warehouses;

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
