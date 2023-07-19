// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Scales;

/// <summary>
/// Контроллер таблицы SCALES.
/// </summary>
public sealed class WsSqlLineRepository : WsSqlTableRepositoryBase<WsSqlScaleModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlLineRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlLineRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Item

    public WsSqlScaleModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlScaleModel>();

    public WsSqlScaleModel GetItemById(long id) => SqlCore.GetItemNotNullable<WsSqlScaleModel>(id);
    
    #endregion

    #region List

    public List<WsSqlScaleModel> GetList() => ContextList.GetListNotNullableLines(SqlCrudConfig);

    public List<WsSqlScaleModel> GetList(WsSqlEnumIsMarked isMarked) =>
        ContextList.GetListNotNullableLines(new() { IsMarked = isMarked, IsResultOrder = true });
    
    public List<WsSqlScaleModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableLines(sqlCrudConfig);

    #endregion
    
    #region CRUD
    
    public void Update(WsSqlScaleModel line) => SqlCore.Update(line);

    #endregion
    
}