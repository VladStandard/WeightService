// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusCharacteristicsFks;

/// <summary>
/// SQL-контроллер таблицы связей характеристик и ПЛУ.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluCharacteristicsFkRepository : WsSqlTableRepositoryBase<WsSqlPluCharacteristicsFkModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluCharacteristicsFkRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluCharacteristicsFkRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    public WsSqlPluCharacteristicsFkModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluCharacteristicsFkModel>();

    public List<WsSqlPluCharacteristicsFkModel> GetList() => ContextList.GetListNotNullablePlusCharacteristicsFks(SqlCrudConfig);

    public List<WsSqlPluCharacteristicsFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => 
        ContextList.GetListNotNullablePlusCharacteristicsFks(sqlCrudConfig);

    #endregion
}