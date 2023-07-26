// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlEnumOrder.Desc });
        return SqlCore.GetListNotNullable<WsSqlPluWeighingModel>(sqlCrudConfig);
    }
    
    #endregion
}