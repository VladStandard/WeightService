using Ws.StorageCore.Entities.SchemaRef1c.Plus;

namespace Ws.StorageCore.Entities.SchemaScale.PlusStorageMethodsFks;

public sealed class SqlPluStorageMethodFkRepository : SqlTableRepositoryBase<SqlPluStorageMethodFkEntity>
{
    public SqlPluStorageMethodFkEntity GetItemByPlu(SqlPluEntity plu)
    {
        if (plu.IsNew)
            return new();
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlPluStorageMethodFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<SqlPluStorageMethodFkEntity>(sqlCrudConfig);
    }

    public List<SqlPluStorageMethodFkEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<SqlPluStorageMethodFkEntity> items = SqlCore.GetEnumerable<SqlPluStorageMethodFkEntity>(sqlCrudConfig);
        items = items.OrderBy(item => item.Plu.Number);
        return items.ToList();
    }
    
    public SqlPluStorageMethodFkEntity GetItemByPluNumber(int pluNumber)
    {
        SqlPluEntity plu = new SqlPluRepository().GetEnumerableByNumber((short)pluNumber).FirstOrDefault() ?? new();
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlPluStorageMethodFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<SqlPluStorageMethodFkEntity>(sqlCrudConfig);
    }
}