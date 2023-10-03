using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.Versions;

public class WsSqlVersionRepository : WsSqlTableRepositoryBase<WsSqlVersionModel>
{
    public List<WsSqlVersionModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Desc(nameof(WsSqlVersionModel.Version)));
        return SqlCore.GetEnumerable<WsSqlVersionModel>(sqlCrudConfig).ToList();
    }
}