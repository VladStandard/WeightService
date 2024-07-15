using ProjectionTools.Specifications;
using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Domain.Models.Entities.Devices;

namespace Ws.Database.Nhibernate.Entities.Ref.Printers;

public class SqlPrinterRepository : BaseRepository, IGetItemByUid<Printer>, ISave<Printer>, IUpdate<Printer>, IDelete<Printer>
{
    public Printer GetByUid(Guid uid) => Session.Get<Printer>(uid) ?? new();

    #region Specs

    public IList<Printer> GetListBySpec(Specification<Printer> spec) =>
        Session.Query<Printer>().Where(spec).OrderBy(i => i.Type).ThenBy(i => i.Name).ToList();

    #endregion

    public Printer Save(Printer item) { Session.Save(item); return item; }
    public Printer Update(Printer item) { Session.Update(item); return item; }
    public void Delete(Printer item) => Session.Delete(item);
}