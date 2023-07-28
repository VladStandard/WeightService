// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Clips;

/// <summary>
/// SQL-контроллер таблицы CLIPS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlClipRepository : WsSqlTableRepositoryBase<WsSqlClipModel>
{
    #region Public and private methods

    public WsSqlClipModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlClipModel>();

    public List<WsSqlClipModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(new(nameof(WsSqlTableBase.Name)));
        return SqlCore.GetListNotNullable<WsSqlClipModel>(sqlCrudConfig);
    }

    public WsSqlClipModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new() { new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C } },
            WsSqlEnumIsMarked.ShowAll, false, false, false);
        return SqlCore.GetItemByCrud<WsSqlClipModel>(sqlCrudConfig);
    }

    #endregion
}