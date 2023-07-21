// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.ProductionFacilities;

/// <summary>
/// SQL-контроллер таблицы ProductionFacility.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlAreaRepository : WsSqlTableRepositoryBase<WsSqlProductionFacilityModel>
{
    #region Public and private methods

    public WsSqlProductionFacilityModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlProductionFacilityModel>();

    public WsSqlProductionFacilityModel GetItem(Guid? uid) => SqlCore.GetItemNotNullableByUid<WsSqlProductionFacilityModel>(uid);

    public List<WsSqlProductionFacilityModel> GetList() => ContextList.GetListNotNullableAreas(SqlCrudConfig);

    #endregion
}