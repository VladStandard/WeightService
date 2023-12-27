namespace Ws.StorageCore.Entities.SchemaPrint.Pallets;

public sealed class SqlPalletRepository : SqlTableRepositoryBase<SqlPalletEntity>
{
    public IEnumerable<SqlPalletEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(new (nameof(SqlEntityBase.CreateDt), false));
        return SqlCore.GetEnumerable<SqlPalletEntity>(sqlCrudConfig);
    }
}