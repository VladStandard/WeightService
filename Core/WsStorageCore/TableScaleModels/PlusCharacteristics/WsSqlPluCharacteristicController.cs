// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.PlusCharacteristics;

/// <summary>
/// SQL-контроллер таблицы характеристик ПЛУ.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluCharacteristicController : WsSqlTableControllerBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluCharacteristicController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluCharacteristicController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    public WsSqlPluCharacteristicModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluCharacteristicModel>();

    public List<WsSqlPluCharacteristicModel> GetList() => ContextList.GetListNotNullablePlusCharacteristics(SqlCrudConfig);

    /// <summary>
    /// Получить бренд по полю UID_1C.
    /// </summary>
    /// <param name="uid1C"></param>
    /// <returns></returns>
    public WsSqlPluCharacteristicModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>
                { new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C } },
            WsSqlEnumIsMarked.ShowAll, false, false, false, false);
        return SqlCore.GetItemNotNullable<WsSqlPluCharacteristicModel>(sqlCrudConfig);
    }

    #endregion
}