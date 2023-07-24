namespace WsStorageCore.Tables.TableScaleModels.Tasks;

public class WsSqlTaskRepository : WsSqlTableRepositoryBase<WsSqlTaskModel>
{
    public List<WsSqlTaskModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlTaskModel> list = SqlCore.GetListNotNullable<WsSqlTaskModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Scale.Description).ToList();
        return list;
    }
}