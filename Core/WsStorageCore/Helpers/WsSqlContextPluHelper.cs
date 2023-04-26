// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник табличных записей таблицы PLUS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlContextPluHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlContextPluHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlContextPluHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    private WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    private WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;

    #endregion

    #region Public and private methods

    public PluModel GetItemByUid1c(Guid uid)
    {
        SqlCrudConfigModel sqlCrudConfig = new SqlCrudConfigModel(new List<SqlFieldFilterModel>()
                { new() { Name = nameof(PluModel.Uid1c), Value = uid } },
            true, false, false, false, false);
        return AccessItem.GetItemNotNullable<PluModel>(sqlCrudConfig);
    }

    public PluModel GetNewItem() => AccessItem.GetItemNewEmpty<PluModel>();

    public List<PluModel> GetList() => ContextList.GetListNotNullablePlus(new());

    #endregion
}