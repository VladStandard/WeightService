namespace WsStorageCore.Tables.TableScaleModels.BarCodes;

public sealed class WsSqlBarcodeRepository : WsSqlTableRepositoryBase<WsSqlBarCodeModel>
{
    public List<WsSqlBarCodeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.ChangeDt), WsSqlEnumOrder.Desc);
        return SqlCore.GetListNotNullable<WsSqlBarCodeModel>(sqlCrudConfig);
    }
}