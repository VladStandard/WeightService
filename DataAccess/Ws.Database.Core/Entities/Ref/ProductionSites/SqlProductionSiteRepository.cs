using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.ProductionSites;

public sealed class SqlProductionSiteRepository :  BaseRepository, IGetItemByUid<ProductionSiteEntity>, IGetAll<ProductionSiteEntity>
{
    public ProductionSiteEntity GetByUid(Guid uid) => Session.Get<ProductionSiteEntity>(uid) ?? new();

    public IEnumerable<ProductionSiteEntity> GetAll() => Session.Query<ProductionSiteEntity>().OrderBy(i => i.Name).ToList();
}