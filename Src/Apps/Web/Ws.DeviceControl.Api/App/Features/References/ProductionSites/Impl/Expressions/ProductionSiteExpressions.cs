using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Expressions;

internal static class ProductionSiteExpressions
{
    public static Expression<Func<ProductionSiteEntity, ProductionSiteDto>> ToDto =>
        productionSite => new()
        {
            Id = productionSite.Id,
            Name = productionSite.Name,
            Address = productionSite.Address,
            CreateDt = productionSite.CreateDt,
            ChangeDt = productionSite.ChangeDt
        };
}