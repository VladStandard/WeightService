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
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.ChangeDt), WsSqlEnumOrder.Desc);
        return SqlCore.GetEnumerableNotNullable<WsSqlPluWeighingModel>(sqlCrudConfig).ToList();
    }
    
    #endregion
}