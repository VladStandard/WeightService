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
    
    public IEnumerable<PluEntity> GetEnumerable() => GetEnumerable(new());
    
    public IEnumerable<PluEntity> GetEnumerableByNumber(short number)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(PluEntity.Number), number));
        return GetEnumerable(sqlCrudConfig);
    }
}