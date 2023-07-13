// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Clips;

/// <summary>
/// SQL-контроллер таблицы CLIPS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlClipRepository : WsSqlTableRepositoryBase<WsSqlClipModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlClipRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlClipRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    public WsSqlClipModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlClipModel>();

    public WsSqlClipModel GetItem(WsSqlPluModel plu) => ContextItem.GetItemPluClipFkNotNullable(plu).Clip;

    public List<WsSqlClipModel> GetList() => ContextList.GetListNotNullableClips(SqlCrudConfig);

    /// <summary>
    /// Получить клипсу по полю UID_1C.
    /// </summary>
    /// <param name="uid1C"></param>
    /// <returns></returns>
    public WsSqlClipModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>
                { new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C } },
            WsSqlEnumIsMarked.ShowAll, false, false, false);
        return SqlCore.GetItemNotNullable<WsSqlClipModel>(sqlCrudConfig);
    }

    #endregion
}