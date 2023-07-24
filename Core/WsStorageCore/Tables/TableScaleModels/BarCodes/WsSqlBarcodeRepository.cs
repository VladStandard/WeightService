namespace WsStorageCore.Tables.TableScaleModels.BarCodes;

public sealed class WsSqlBarcodeRepository : WsSqlTableRepositoryBase<WsSqlBarCodeModel>
{
    public List<WsSqlBarCodeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlEnumOrder.Desc });
        List<WsSqlBarCodeModel> list = SqlCore.GetListNotNullable<WsSqlBarCodeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }
}