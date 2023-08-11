namespace WsStorageCore.Tables.TableScaleModels.Versions;

public class WsSqlVersionRepository : WsSqlTableRepositoryBase<WsSqlVersionModel>
{
    public List<WsSqlVersionModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlVersionModel.Version), WsSqlEnumOrder.Desc);
        return SqlCore.GetEnumerableNotNullable<WsSqlVersionModel>(sqlCrudConfig).ToList();
    }
}