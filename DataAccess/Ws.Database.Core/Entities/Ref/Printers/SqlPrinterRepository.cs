using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Printers;

public class SqlPrinterRepository : BaseRepository, IGetItemByUid<PrinterEntity>, IGetAll<PrinterEntity>
{
    public PrinterEntity GetByUid(Guid uid) => SqlCoreHelper.GetItemById<PrinterEntity>(uid);
    
    public IEnumerable<PrinterEntity> GetAll()
    {
        return SqlCoreHelper.GetEnumerable(
            QueryOver.Of<PrinterEntity>().OrderBy(i => i.Type).Asc
        );
    }
}