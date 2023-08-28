namespace WsStorageCore.Tables.TableDiagModels.LogsWebs;

public class WsSqlLogWebRepository: WsSqlTableRepositoryBase<WsSqlLogWebModel>
{
    public List<WsSqlLogWebModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.CreateDt), WsSqlEnumOrder.Desc);
        return SqlCore.GetEnumerableNotNullable<WsSqlLogWebModel>(sqlCrudConfig).ToList();
    }
}