// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableRefModels.Plus1CFk;

/// <summary>
/// Контроллер таблицы REF.PLUS_1C_FK.
/// </summary>
public sealed class WsSqlPlu1CRepository : WsSqlTableRepositoryBase<WsSqlPlu1CFkModel>
{
    #region Public and private methods

    public WsSqlPlu1CFkModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPlu1CFkModel>();

    public List<WsSqlPlu1CFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlPlu1CFkModel> list = SqlCore.GetListNotNullable<WsSqlPlu1CFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Plu.Number).ToList();
        return list;
    }
    
    public List<WsSqlPlu1CFkModel> GetNewList() => new();

    #endregion
}