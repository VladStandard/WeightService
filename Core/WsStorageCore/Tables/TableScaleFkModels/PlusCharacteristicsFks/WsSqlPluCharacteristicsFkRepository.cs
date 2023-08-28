namespace WsStorageCore.Tables.TableScaleFkModels.PlusCharacteristicsFks;

/// <summary>
/// SQL-контроллер таблицы связей характеристик и ПЛУ.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluCharacteristicsFkRepository : WsSqlTableRepositoryBase<WsSqlPluCharacteristicsFkModel>
{
    #region Public and private methods

    public WsSqlPluCharacteristicsFkModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluCharacteristicsFkModel>();

    public IEnumerable<WsSqlPluCharacteristicsFkModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig) => 
        SqlCore.GetEnumerableNotNullable<WsSqlPluCharacteristicsFkModel>(sqlCrudConfig);

    #endregion
}