using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.PlusStorageMethods;

public class WsSqlPluStorageMethodRepository : WsSqlTableRepositoryBase<WsSqlPluStorageMethodModel>
{
    public List<WsSqlPluStorageMethodModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlPluStorageMethodModel>(sqlCrudConfig).ToList();
    }
}