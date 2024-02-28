using Ws.Database.Core.Common.Commands;
using Ws.Database.Core.Common.Queries.Item;
using Ws.Database.Core.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Printers;

public class SqlPrinterRepository : BaseRepository, IGetItemByUid<PrinterEntity>, 
    IGetAll<PrinterEntity>, ISave<PrinterEntity>, IUpdate<PrinterEntity>, IDelete<PrinterEntity>
{
    public PrinterEntity GetByUid(Guid uid) => Session.Get<PrinterEntity>(uid) ?? new();
    public IEnumerable<PrinterEntity> GetAll() => Session.Query<PrinterEntity>().OrderBy(i => i.Type).ToList();
    public PrinterEntity Save(PrinterEntity item) { Session.Save(item); return item; }
    public PrinterEntity Update(PrinterEntity item) { Session.Update(item); return item; }
    public void Delete(PrinterEntity item) => Session.Delete(item);
}