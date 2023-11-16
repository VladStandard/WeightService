using WsStorageCore.Entities.SchemaRef.Hosts;

namespace WsStorageCore.Entities.SchemaScale.Scales;

/// <summary>
/// Контроллер таблицы SCALES.
/// </summary>
public sealed class SqlLineRepository : SqlTableRepositoryBase<SqlScaleEntity>
{

    #region Public and private methods

    public SqlScaleEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlScaleEntity>();

    public SqlScaleEntity GetItemById(long id) => SqlCore.GetItemById<SqlScaleEntity>(id);

    public SqlScaleEntity GetItemByHost(SqlHostEntity host)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlScaleEntity.Host), host));
        return SqlCore.GetItemByCrud<SqlScaleEntity>(sqlCrudConfig);
    }

    public IEnumerable<SqlScaleEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(SqlEntityBase.Description)));
        return SqlCore.GetEnumerable<SqlScaleEntity>(sqlCrudConfig);
    }

    public void Update(SqlScaleEntity line) => SqlCore.Update(line);

    #endregion
}