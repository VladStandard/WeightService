namespace WsStorageCore.Tables.TableScaleModels.PlusCharacteristics;

/// <summary>
/// SQL-контроллер таблицы характеристик ПЛУ.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluCharacteristicRepository : WsSqlTableRepositoryBase<WsSqlPluCharacteristicModel>
{
    #region Public and private methods

    public WsSqlPluCharacteristicModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluCharacteristicModel>();

    public IEnumerable<WsSqlPluCharacteristicModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.Name));
        return SqlCore.GetEnumerableNotNullable<WsSqlPluCharacteristicModel>(sqlCrudConfig);
    }
    
    /// <summary>
    /// Получить бренд по полю UID_1C.
    /// </summary>
    /// <param name="uid1C"></param>
    /// <returns></returns>
    public WsSqlPluCharacteristicModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C });
        return SqlCore.GetItemByCrud<WsSqlPluCharacteristicModel>(sqlCrudConfig);
    }

    #endregion
}