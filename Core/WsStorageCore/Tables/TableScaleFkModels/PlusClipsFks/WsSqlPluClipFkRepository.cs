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

    public List<WsSqlPluClipFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluClipFkModel.Clip)}.{nameof(ClipModel.Name)}", SqlOrderDirection.Asc));
        List<WsSqlPluClipFkModel> list = SqlCore.GetListNotNullable<WsSqlPluClipFkModel>(sqlCrudConfig);
        if (list.Count > 0)
        {
            WsSqlPluClipFkModel pluClipFk = list.First();
            if (pluClipFk.Plu.IsNew)
                pluClipFk.Plu = SqlCore.GetItemNewEmpty<WsSqlPluModel>();
            if (pluClipFk.Clip.IsNew)
                pluClipFk.Clip = SqlCore.GetItemNewEmpty<WsSqlClipModel>();
        }
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Clip.Name).ToList();
        return list;
    }

    #endregion
}