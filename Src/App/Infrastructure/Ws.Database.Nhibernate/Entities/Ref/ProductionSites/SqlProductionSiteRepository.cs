using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Nhibernate.Entities.Ref.ProductionSites;

public sealed class SqlProductionSiteRepository : BaseRepository, IGetItemByUid<ProductionSite>,
    IGetAll<ProductionSite>, IUpdate<ProductionSite>, ISave<ProductionSite>,
    IDelete<ProductionSite>
{
    public ProductionSite GetByUid(Guid uid) => Session.Get<ProductionSite>(uid) ?? new();
    public IList<ProductionSite> GetAll() => Session.Query<ProductionSite>().OrderBy(i => i.Name).ToList();
    public ProductionSite Save(ProductionSite item) { Session.Save(item); return item; }
    public ProductionSite Update(ProductionSite item) { Session.Update(item); return item; }
    public void Delete(ProductionSite item) => Session.Delete(item);
}