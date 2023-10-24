using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.Scales;

/// <summary>
/// Контроллер таблицы SCALES.
/// </summary>
public sealed class WsSqlLineRepository : WsSqlTableRepositoryBase<WsSqlScaleEntity>
{

    #region Public and private methods

    public WsSqlScaleEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlScaleEntity>();

    public WsSqlScaleEntity GetItemById(long id) => SqlCore.GetItemById<WsSqlScaleEntity>(id);

    public WsSqlScaleEntity GetItemByDevice(WsSqlDeviceEntity device)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlScaleEntity.Device), device));
        return SqlCore.GetItemByCrud<WsSqlScaleEntity>(sqlCrudConfig);
    }

    public IEnumerable<WsSqlScaleEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(WsSqlTableBase.Description)));
        return SqlCore.GetEnumerable<WsSqlScaleEntity>(sqlCrudConfig);
    }

    public void Update(WsSqlScaleEntity line) => SqlCore.Update(line);

    #endregion
}