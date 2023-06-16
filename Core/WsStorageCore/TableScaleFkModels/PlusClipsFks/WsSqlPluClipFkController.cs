// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusClipsFks;

/// <summary>
/// SQL-контроллер таблицы связей клипс и ПЛУ.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluClipFkController : WsSqlTableControllerBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluClipFkController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluClipFkController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    public WsSqlPluClipFkModel GetNewItem() => SqlCoreItem.GetItemNewEmpty<WsSqlPluClipFkModel>();

    public WsSqlPluClipFkModel GetItem(WsSqlPluModel plu) => ContextItem.GetItemPluClipFkNotNullable(plu);

    public List<WsSqlPluClipFkModel> GetList() => ContextList.GetListNotNullablePlusClipsFks(SqlCrudConfig);

    public List<WsSqlPluClipFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => 
        ContextList.GetListNotNullablePlusClipsFks(sqlCrudConfig);

    #endregion
}