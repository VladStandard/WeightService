// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusFks;

/// <summary>
/// SQL-контроллер таблицы PLUS_BUNDLES_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluFkRepository : WsSqlTableRepositoryBase<WsSqlPluFkModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluFkRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluFkRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlPluRepository ContextPlu => WsSqlPluRepository.Instance;

    #endregion

    #region Public and private methods

    public WsSqlPluFkModel GetNewItem()
    {
        WsSqlPluFkModel item = SqlCore.GetItemNewEmpty<WsSqlPluFkModel>();
        item.Plu = ContextPlu.GetNewItem();
        item.Parent = ContextPlu.GetNewItem();
        item.Category = null;
        return item;
    }

    public List<WsSqlPluFkModel> GetList() => ContextList.GetListNotNullablePlusFks(SqlCrudConfig);

    #endregion
}