namespace WsStorageCore.Tables.TableScaleModels.Versions;

public class WsSqlVersionRepository : WsSqlTableRepositoryBase<WsSqlVersionModel>
{
    public List<WsSqlVersionModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(new(nameof(WsSqlVersionModel.Version), WsSqlEnumOrder.Desc));
        return SqlCore.GetListNotNullable<WsSqlVersionModel>(sqlCrudConfig);
    }
}