using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Plus;

public sealed class SqlPluRepository : IUid1CRepo<PluEntity>, IUidRepo<PluEntity>
{
    public PluEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<PluEntity>(uid);
    
    public PluEntity GetByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCoreHelper.Instance.GetItemByCrud<PluEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<PluEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(PluEntity.Number)));
        return SqlCoreHelper.Instance.GetEnumerable<PluEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<PluEntity> GetEnumerableNotGroup()
    {
        SqlCrudConfigModel crud = new();
        crud.AddFilter(SqlRestrictions.Equal(nameof(PluEntity.IsGroup), false));
        return GetEnumerable(crud);
    }
    
    public IEnumerable<PluEntity> GetEnumerableByNumber(short number)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(PluEntity.Number), number));
        return GetEnumerable(sqlCrudConfig);
    }
    
    public IEnumerable<PluEntity> GetPluUid1CInRange(List<Guid> uidList)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.In(nameof(PluEntity.Uid1C), uidList));
        return SqlCoreHelper.Instance.GetEnumerable<PluEntity>(sqlCrudConfig);
    }
}