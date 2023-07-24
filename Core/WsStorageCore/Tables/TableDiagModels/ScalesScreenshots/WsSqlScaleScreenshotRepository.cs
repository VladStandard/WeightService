namespace WsStorageCore.Tables.TableDiagModels.ScalesScreenshots;

public class WsSqlScaleScreenshotRepository: WsSqlTableRepositoryBase<WsSqlScaleScreenShotModel>
{
    public List<WsSqlScaleScreenShotModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlEnumOrder.Desc });
        List<WsSqlScaleScreenShotModel> list = SqlCore.GetListNotNullable<WsSqlScaleScreenShotModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }
}