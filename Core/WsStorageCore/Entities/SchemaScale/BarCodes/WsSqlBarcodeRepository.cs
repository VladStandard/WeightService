using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.BarCodes;

public sealed class WsSqlBarcodeRepository : WsSqlTableRepositoryBase<WsSqlBarCodeEntity>
{
    public List<WsSqlBarCodeEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<WsSqlBarCodeEntity>(sqlCrudConfig).ToList();
    }
}