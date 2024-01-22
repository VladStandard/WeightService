using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Clips;

public sealed class SqlClipRepository : IUid1CRepo<ClipEntity>, IUidRepo<ClipEntity>
{
    public ClipEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<ClipEntity>(uid);
    
    public IEnumerable<ClipEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<ClipEntity>(crud);
    }

    public ClipEntity GetByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCoreHelper.Instance.GetItemByCrud<ClipEntity>(sqlCrudConfig);
    }
}