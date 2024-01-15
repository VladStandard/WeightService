using Ws.StorageCore.Entities.SchemaRef.ProductionSites;

namespace Ws.StorageCore.Entities.SchemaRef.WorkShops;

public sealed class SqlWorkShopValidator : SqlTableValidator<SqlWorkShopEntity>
{
    public SqlWorkShopValidator(bool isCheckIdentity) : base(isCheckIdentity)
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
