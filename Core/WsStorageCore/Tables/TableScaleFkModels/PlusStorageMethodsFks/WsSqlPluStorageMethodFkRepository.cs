// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusStorageMethodsFks;

/// <summary>
/// SQL-контроллер таблицы записей таблиц PLUS_STORAGE_METHODS, PLUS_STORAGE_METHODS_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluStorageMethodFkRepository : WsSqlTableRepositoryBase<WsSqlPluStorageMethodFkModel>
{
    #region Public and private methods

    public WsSqlPluStorageMethodFkModel GetItemByPlu(WsSqlPluModel plu)
    {
        if (plu.IsNew)
            return new();
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluStorageMethodFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", 
            plu.IdentityValueUid, WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNotNullable<WsSqlPluStorageMethodFkModel>(sqlCrudConfig);
    }

    public List<WsSqlPluStorageMethodFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlPluStorageMethodFkModel> list = SqlCore.GetListNotNullable<WsSqlPluStorageMethodFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Plu.Number).ToList();
        return list;
    }

    #endregion
}