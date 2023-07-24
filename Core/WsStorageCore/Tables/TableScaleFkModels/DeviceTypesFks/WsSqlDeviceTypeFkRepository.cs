namespace WsStorageCore.Tables.TableScaleFkModels.DeviceTypesFks;

public class WsSqlDeviceTypeFkRepository : WsSqlTableRepositoryBase<WsSqlDeviceTypeFkModel>
{
    public List<WsSqlDeviceTypeFkModel> GetList() => GetList(SqlCrudConfig);

    public List<WsSqlDeviceTypeFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlDeviceTypeFkModel> result = SqlCore.GetListNotNullable<WsSqlDeviceTypeFkModel>(sqlCrudConfig);
        result = result
            .OrderBy(item => item.Type.Name)
            .ThenBy(item => item.Device.Name).ToList();
        return result;
    }
}