// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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