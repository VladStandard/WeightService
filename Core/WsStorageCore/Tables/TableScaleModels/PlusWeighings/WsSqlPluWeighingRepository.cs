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

    public WsSqlPluWeighingModel GetItem(Guid? uid) => SqlCore.GetItemNotNullableByUid<WsSqlPluWeighingModel>(uid);

    public List<WsSqlPluWeighingModel> GetList() => ContextList.GetListNotNullablePlusWeighings(SqlCrudConfig);
    public List<WsSqlPluWeighingModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullablePlusWeighings(sqlCrudConfig);
    
    #endregion
}