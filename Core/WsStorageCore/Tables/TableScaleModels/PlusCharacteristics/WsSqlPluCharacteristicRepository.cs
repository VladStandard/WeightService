// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.PlusCharacteristics;

/// <summary>
/// SQL-контроллер таблицы характеристик ПЛУ.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluCharacteristicRepository : WsSqlTableRepositoryBase<WsSqlPluCharacteristicModel>
{
    #region Public and private methods

    public WsSqlPluCharacteristicModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluCharacteristicModel>();

    public List<WsSqlPluCharacteristicModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlPluCharacteristicModel> list = SqlCore.GetListNotNullable<WsSqlPluCharacteristicModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }
    
    /// <summary>
    /// Получить бренд по полю UID_1C.
    /// </summary>
    /// <param name="uid1C"></param>
    /// <returns></returns>
    public WsSqlPluCharacteristicModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new() { new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C } },
            WsSqlEnumIsMarked.ShowAll, false, false, false);
        return SqlCore.GetItemNotNullable<WsSqlPluCharacteristicModel>(sqlCrudConfig);
    }

    #endregion
}