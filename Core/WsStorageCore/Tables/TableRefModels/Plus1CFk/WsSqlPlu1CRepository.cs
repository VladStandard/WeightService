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

    public IEnumerable<WsSqlPlu1CFkModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlPlu1CFkModel> items = SqlCore.GetEnumerableNotNullable<WsSqlPlu1CFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Plu.Number);
        return items;
    }
    
    public List<WsSqlPlu1CFkModel> GetNewList() => new();

    #endregion
}