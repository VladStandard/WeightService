using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Plus;

public sealed class SqlPluRepository : SqlTableRepositoryBase<PluEntity>
{
    public PluEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<PluEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<PluEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(PluEntity.Number)));
        return SqlCore.GetEnumerable<PluEntity>(sqlCrudConfig);
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
    
    public PluEntity GetByUid1C(Guid uid)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(PluEntity.Uid1C), uid));
        return SqlCore.GetItemByCrud<PluEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<PluEntity> GetPluUid1CInRange(List<Guid> uidList)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.In(nameof(PluEntity.Uid1C), uidList));
        return SqlCore.GetEnumerable<PluEntity>(sqlCrudConfig);
    }
}