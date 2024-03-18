using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Nhibernate.Entities.Ref.ProductionSites;

public sealed class SqlProductionSiteRepository : BaseRepository, IGetItemByUid<ProductionSiteEntity>,
    IGetAll<ProductionSiteEntity>, IUpdate<ProductionSiteEntity>, ISave<ProductionSiteEntity>,
    IDelete<ProductionSiteEntity>
{
    public ProductionSiteEntity GetByUid(Guid uid) => Session.Get<ProductionSiteEntity>(uid) ?? new();
    public IEnumerable<ProductionSiteEntity> GetAll() => Session.Query<ProductionSiteEntity>().OrderBy(i => i.Name).ToList();
    public ProductionSiteEntity Save(ProductionSiteEntity item) { Session.Save(item); return item; }
    public ProductionSiteEntity Update(ProductionSiteEntity item) { Session.Update(item); return item; }
    public void Delete(ProductionSiteEntity item) => Session.Delete(item);
}