namespace WsStorageCore.Tables.TableScaleModels.ProductionFacilities;

public class WsSqlProductionFacilityRepository: WsSqlTableRepositoryBase<WsSqlProductionFacilityModel>
{
    public List<WsSqlProductionFacilityModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableAreas(sqlCrudConfig);
}