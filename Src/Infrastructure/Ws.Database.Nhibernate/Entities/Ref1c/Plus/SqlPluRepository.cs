using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref1c.Plus;

namespace Ws.Database.Nhibernate.Entities.Ref1c.Plus;

public sealed class SqlPluRepository : BaseRepository, IGetItemByUid<Plu>, IGetAll<Plu>
{
    public Plu GetByUid(Guid uid) => Session.Get<Plu>(uid) ?? new();
    public IList<Plu> GetAll() => Session.Query<Plu>().OrderBy(i => i.Number).ToList();
    public Plu Update(Plu item)
    {
        const string sql = "UPDATE REF_1C.PLUS SET TEMPLATE_UID = :newValue WHERE UID = :entityId";
        Session.CreateSQLQuery(sql)
            .SetParameter("newValue", item.TemplateUid)
            .SetParameter("entityId", item.Uid)
            .ExecuteUpdate();
        Session.Update(item);
        return item;
    }
}