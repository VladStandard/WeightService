// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Boxes;

/// <summary>
/// SQL-контроллер таблицы коробок.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlBoxRepository : WsSqlTableRepositoryBase<WsSqlBoxModel>
{
    #region Public and private methods

    public WsSqlBoxModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlBoxModel>();

    public List<WsSqlBoxModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlBoxModel> list = SqlCore.GetListNotNullable<WsSqlBoxModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }
    
    /// <summary>
    /// Получить коробку по полю UID_1C.
    /// </summary>
    /// <param name="uid1C"></param>
    /// <returns></returns>
    public WsSqlBoxModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new() { new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C } },
            WsSqlEnumIsMarked.ShowAll, false, false, false);
        return SqlCore.GetItemNotNullable<WsSqlBoxModel>(sqlCrudConfig);
    }

    #endregion
}