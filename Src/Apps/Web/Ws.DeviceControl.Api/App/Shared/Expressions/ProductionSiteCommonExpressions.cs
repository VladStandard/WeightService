using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;

namespace Ws.DeviceControl.Api.App.Shared.Expressions;

internal static class ProductionSiteCommonExpressions
{
    public static Expression<Func<ProductionSiteEntity, ProxyDto>> ToProxy =>
        productionSite => new()
        {
            Id = productionSite.Id,
            Name = productionSite.Name
        };
}