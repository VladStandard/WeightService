namespace WsStorageCore.Tables.TableScaleModels.Scales;

/// <summary>
/// Контроллер таблицы SCALES.
/// </summary>
public sealed class WsSqlLineRepository : WsSqlTableRepositoryBase<WsSqlScaleModel>
{

    #region Public and private methods

    public WsSqlScaleModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlScaleModel>();

    public WsSqlScaleModel GetItemById(long id) => SqlCore.GetItemById<WsSqlScaleModel>(id);

    public WsSqlScaleModel GetItemByDevice(WsSqlDeviceModel device)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlScaleModel.Device), device));
        return SqlCore.GetItemByCrud<WsSqlScaleModel>(sqlCrudConfig);
    }

    public IEnumerable<WsSqlScaleModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(WsSqlTableBase.Description)));
        return SqlCore.GetEnumerableNotNullable<WsSqlScaleModel>(sqlCrudConfig);
    }

    public void Update(WsSqlScaleModel line) => SqlCore.Update(line);

    #endregion
}