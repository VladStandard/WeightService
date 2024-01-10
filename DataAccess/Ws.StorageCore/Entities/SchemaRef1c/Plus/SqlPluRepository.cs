namespace Ws.StorageCore.Entities.SchemaRef1c.Plus;

public sealed class SqlPluRepository : SqlTableRepositoryBase<SqlPluEntity>
{
    public SqlPluEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<SqlPluEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<SqlPluEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(SqlPluEntity.Number)));
        return SqlCore.GetEnumerable<SqlPluEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<SqlPluEntity> GetEnumerableNotGroup(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlPluEntity.IsGroup), false));
        return GetEnumerable(sqlCrudConfig);
    }
    
    public IEnumerable<SqlPluEntity> GetEnumerableByNumber(short number)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlPluEntity.Number), number));
        return GetEnumerable(sqlCrudConfig);
    }
    
    public SqlPluEntity GetByUid1C(Guid uid)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlPluEntity.Uid1C), uid));
        return SqlCore.GetItemByCrud<SqlPluEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<SqlPluEntity> GetPluUid1CInRange(List<Guid> uidList)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.In(nameof(SqlPluEntity.Uid1C), uidList));
        return SqlCore.GetEnumerable<SqlPluEntity>(sqlCrudConfig);
    }
}