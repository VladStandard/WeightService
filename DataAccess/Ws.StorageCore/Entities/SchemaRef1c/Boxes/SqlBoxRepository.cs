namespace Ws.StorageCore.Entities.SchemaRef1c.Boxes;

public sealed class SqlBoxRepository : SqlTableRepositoryBase<SqlBoxEntity>
{
    public IEnumerable<SqlBoxEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(SqlBoxEntity.Weight)));
        sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlBoxEntity>(sqlCrudConfig);
    }
    
    public SqlBoxEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<SqlBoxEntity>(sqlCrudConfig);
    }
}