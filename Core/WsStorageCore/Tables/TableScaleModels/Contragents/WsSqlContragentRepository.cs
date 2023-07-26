namespace WsStorageCore.Tables.TableScaleModels.Contragents;

public class WsSqlContragentRepository : WsSqlTableRepositoryBase<WsSqlContragentModel>
{
    public List<WsSqlContragentModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        return SqlCore.GetListNotNullable<WsSqlContragentModel>(sqlCrudConfig);
    }
}