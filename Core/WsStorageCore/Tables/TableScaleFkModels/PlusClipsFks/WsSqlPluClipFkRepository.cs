// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusClipsFks;

/// <summary>
/// SQL-контроллер таблицы связей клипс и ПЛУ.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluClipFkRepository : WsSqlTableRepositoryBase<WsSqlPluClipFkModel>
{
    #region Public and private methods

    public WsSqlPluClipFkModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluClipFkModel>();

    public WsSqlPluClipFkModel GetItem(WsSqlPluModel plu) => ContextItem.GetItemPluClipFkNotNullable(plu);

    public List<WsSqlPluClipFkModel> GetList() => ContextList.GetListNotNullablePlusClipsFks(SqlCrudConfig);

    public List<WsSqlPluClipFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => 
        ContextList.GetListNotNullablePlusClipsFks(sqlCrudConfig);

    #endregion
}