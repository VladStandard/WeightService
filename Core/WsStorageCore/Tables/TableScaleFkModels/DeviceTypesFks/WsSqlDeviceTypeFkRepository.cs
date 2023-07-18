namespace WsStorageCore.Tables.TableScaleFkModels.DeviceTypesFks;

public class WsSqlDeviceTypeFkRepository : WsSqlTableRepositoryBase<WsSqlDeviceTypeFkModel>
{
    public List<WsSqlDeviceTypeFkModel> GetList() => ContextList.GetListDevicesTypesFks(SqlCrudConfig);
    
    public List<WsSqlDeviceTypeFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListDevicesTypesFks(sqlCrudConfig);
}