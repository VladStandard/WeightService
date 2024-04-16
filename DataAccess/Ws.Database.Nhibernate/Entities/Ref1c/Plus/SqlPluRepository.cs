using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Nhibernate.Entities.Ref1c.Plus;

public sealed class SqlPluRepository : BaseRepository, IGetItemByUid<PluEntity>, IGetAll<PluEntity>
{
    public PluEntity GetByUid(Guid uid) => Session.Get<PluEntity>(uid) ?? new();
    public IEnumerable<PluEntity> GetAll() => Session.Query<PluEntity>().OrderBy(i => i.Number).ToList();
    public PluEntity Update(PluEntity item) {
        const string sql = "UPDATE REF_1C.PLUS SET TEMPLATE_UID = :newValue WHERE UID = :entityId";
        Session.CreateSQLQuery(sql)
            .SetParameter("newValue", item.TemplateUid)
            .SetParameter("entityId", item.Uid)
            .ExecuteUpdate();
        return item;
    }
}