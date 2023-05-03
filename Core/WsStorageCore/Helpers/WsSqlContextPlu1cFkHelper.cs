// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableRefFkModels.Plus1CFk;

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник табличных записей таблицы PLUS_1C_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlContextPlu1cFkHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlContextPlu1cFkHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlContextPlu1cFkHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    private WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    private WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;

    #endregion

    #region Public and private methods

    public WsSqlPlu1CFkModel GetNewItem() => AccessItem.GetItemNewEmpty<WsSqlPlu1CFkModel>();

    public List<WsSqlPlu1CFkModel> GetList() => ContextList.GetListNotNullablePlus1cFks(new());
    
    public List<WsSqlPlu1CFkModel> GetNewList() => new() { GetNewItem() };

    #endregion
}