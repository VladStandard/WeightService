using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.Contragents;

public class WsSqlContragentRepository : WsSqlTableRepositoryBase<WsSqlContragentModel>
{
    public List<WsSqlContragentModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerableNotNullable<WsSqlContragentModel>(sqlCrudConfig).ToList();
    }
}