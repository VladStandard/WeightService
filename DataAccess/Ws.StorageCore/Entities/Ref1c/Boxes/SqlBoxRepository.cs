using Ws.Domain.Models.Entities.Ref1c;
using Ws.StorageCore.OrmUtils;

namespace Ws.StorageCore.Entities.Ref1c.Boxes;

public sealed class SqlBoxRepository : SqlTableRepositoryBase<BoxEntity>
{
    public IEnumerable<BoxEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.Asc(nameof(BoxEntity.Weight)));
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<BoxEntity>(crud);
    }
    
    public BoxEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<BoxEntity>(sqlCrudConfig);
    }
}