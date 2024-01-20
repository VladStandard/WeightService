using Ws.Domain.Models.Entities.Ref1c;
using Ws.StorageCore.OrmUtils;

namespace Ws.StorageCore.Entities.Ref1c.Clips;

public sealed class SqlClipRepository : SqlTableRepositoryBase<ClipEntity>
{
    public IEnumerable<ClipEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<ClipEntity>(crud);
    }

    public ClipEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<ClipEntity>(sqlCrudConfig);
    }
}