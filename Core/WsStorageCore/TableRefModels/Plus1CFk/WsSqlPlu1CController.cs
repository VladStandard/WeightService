// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableRefModels.Plus1CFk;

/// <summary>
/// Контроллер таблицы REF.PLUS_1C_FK.
/// </summary>
public sealed class WsSqlPlu1CController
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPlu1CController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPlu1CController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    private WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    private WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;

    #endregion

    #region Public and private methods

    public WsSqlPlu1CFkModel GetNewItem() => AccessItem.GetItemNewEmpty<WsSqlPlu1CFkModel>();

    public List<WsSqlPlu1CFkModel> GetList() => ContextList.GetListNotNullablePlus1CFks(new());

    public List<WsSqlPlu1CFkModel> GetNewList() => new();

    #endregion
}