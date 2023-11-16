namespace Ws.StorageCore.Entities.SchemaScale.BarCodes;

public sealed class SqlBarcodeRepository : SqlTableRepositoryBase<SqlBarCodeEntity>
{
    public List<SqlBarCodeEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<SqlBarCodeEntity>(sqlCrudConfig).ToList();
    }
}