namespace Ws.StorageCore.Entities.SchemaScale.PlusWeightings;

/// <summary>
/// SQL-контроллер таблицы PLUS_WEIGHTINGS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlPluWeighingRepository : SqlTableRepositoryBase<SqlPluWeighingEntity>
{
    #region Public and private methods

    public SqlPluWeighingEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlPluWeighingEntity>();

    public IEnumerable<SqlPluWeighingEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<SqlPluWeighingEntity>(sqlCrudConfig).ToList();
    }
    
    #endregion
}