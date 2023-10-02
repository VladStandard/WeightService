using WsStorageCore.OrmUtils;
namespace WsStorageCore.Tables.TableScaleModels.BarCodes;

public sealed class WsSqlBarcodeRepository : WsSqlTableRepositoryBase<WsSqlBarCodeModel>
{
    public List<WsSqlBarCodeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerableNotNullable<WsSqlBarCodeModel>(sqlCrudConfig).ToList();
    }
}