namespace WsStorageCore.Tables.TableScaleModels.BarCodes;

public sealed class WsSqlBarcodeRepository : WsSqlTableRepositoryBase<WsSqlBarCodeModel>
{
    public List<WsSqlBarCodeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableBarCodes(sqlCrudConfig);
}