using Ws.StorageCore.Entities.SchemaRef.ProductionSites;

namespace Ws.StorageCore.Entities.SchemaRef.Warehouses;

public sealed class SqlWarehouseValidator : SqlTableValidator<SqlWarehouseEntity>
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
