using System;

namespace Ws.StorageCore.Entities.SchemaRef1c.Plus;

public sealed class SqlPluRepository : SqlTableRepositoryBase<SqlPluEntity>
{
    public SqlPluEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<SqlPluEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<SqlPluEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(SqlPluEntity.Number)));
        return SqlCore.GetEnumerable<SqlPluEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<SqlPluEntity> GetEnumerableByNumber(short number)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlPluEntity.Number), number));
        return GetEnumerable(sqlCrudConfig);
    }
    
    public SqlPluEntity GetByUid1C(Guid uid)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlPluEntity.Uid1C), uid));
        return SqlCore.GetItemByCrud<SqlPluEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<SqlPluEntity> GetPluUid1CInRange(List<Guid> uidList)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.In(nameof(SqlPluEntity.Uid1C), uidList));
        return SqlCore.GetEnumerable<SqlPluEntity>(sqlCrudConfig);
    }
}