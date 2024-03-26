using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Nhibernate.Entities.Ref.Printers;

public class SqlPrinterRepository : BaseRepository, IGetItemByUid<PrinterEntity>,
    IGetAll<PrinterEntity>, ISave<PrinterEntity>, IUpdate<PrinterEntity>, IDelete<PrinterEntity>
{
    public PrinterEntity GetByUid(Guid uid) => Session.Get<PrinterEntity>(uid) ?? new();
    public IEnumerable<PrinterEntity> GetAllByProductionSite(ProductionSiteEntity site) =>
        Session.Query<PrinterEntity>().Where(i => i.ProductionSite == site).OrderBy(i => i.Type).ToList();
    public IEnumerable<PrinterEntity> GetAll() => Session.Query<PrinterEntity>().OrderBy(i => i.Type).ToList();
    public PrinterEntity Save(PrinterEntity item) { Session.Save(item); return item; }
    public PrinterEntity Update(PrinterEntity item) { Session.Update(item); return item; }
    public void Delete(PrinterEntity item) => Session.Delete(item);
}