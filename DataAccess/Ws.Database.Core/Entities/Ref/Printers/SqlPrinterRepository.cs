using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Printers;

public class SqlPrinterRepository : BaseRepository, IGetItemByUid<PrinterEntity>, IGetAll<PrinterEntity>
{
    public PrinterEntity GetByUid(Guid uid) => Session.Get<PrinterEntity>(uid);
    public IEnumerable<PrinterEntity> GetAll() => Session.Query<PrinterEntity>().OrderBy(i => i.Type).ToList();
}