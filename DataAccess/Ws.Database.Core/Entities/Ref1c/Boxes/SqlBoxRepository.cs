using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Boxes;

public sealed class SqlBoxRepository : IUid1CRepo<BoxEntity>, IUidRepo<BoxEntity>
{
    public BoxEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<BoxEntity>(uid);
    
    public BoxEntity GetByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCoreHelper.Instance.GetItemByCrud<BoxEntity>(sqlCrudConfig);
    }
    public IEnumerable<BoxEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.Asc(nameof(BoxEntity.Weight)));
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<BoxEntity>(crud);
    }
}