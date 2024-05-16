
using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Nhibernate.Entities.Ref.Printers;

public class SqlPrinterRepository : BaseRepository, IGetItemByUid<Printer>,
    IGetAll<Printer>, ISave<Printer>, IUpdate<Printer>, IDelete<Printer>
{
    public Printer GetByUid(Guid uid) => Session.Get<Printer>(uid) ?? new();
    public IEnumerable<Printer> GetAllByProductionSite(ProductionSite site) =>
        Session.Query<Printer>().Where(i => i.ProductionSite == site).OrderBy(i => i.Type).ToList();
    public IEnumerable<Printer> GetAll() => Session.Query<Printer>().OrderBy(i => i.Type).ToList();
    public Printer Save(Printer item) { Session.Save(item); return item; }
    public Printer Update(Printer item) { Session.Update(item); return item; }
    public void Delete(Printer item) => Session.Delete(item);
}