namespace WsStorageCore.Entities.SchemaScale.PlusWeightings;

/// <summary>
/// SQL-контроллер таблицы PLUS_WEIGHTINGS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluWeighingRepository : WsSqlTableRepositoryBase<WsSqlPluWeighingEntity>
{
    #region Public and private methods

    public WsSqlPluWeighingEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluWeighingEntity>();

    public List<WsSqlPluWeighingEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<WsSqlPluWeighingEntity>(sqlCrudConfig).ToList();
    }
    
    #endregion
}