using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.PlusWeighings;

/// <summary>
/// SQL-контроллер таблицы PLUS_WEIGHINGS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluWeighingRepository : WsSqlTableRepositoryBase<WsSqlPluWeighingModel>
{
    #region Public and private methods

    public WsSqlPluWeighingModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluWeighingModel>();

    public List<WsSqlPluWeighingModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<WsSqlPluWeighingModel>(sqlCrudConfig).ToList();
    }
    
    #endregion
}