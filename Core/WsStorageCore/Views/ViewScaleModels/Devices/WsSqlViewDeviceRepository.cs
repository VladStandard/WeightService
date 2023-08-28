namespace WsStorageCore.Views.ViewScaleModels.Devices;

public class WsSqlViewDeviceRepository : IViewDeviceRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public IList<WsSqlViewDeviceModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IList<WsSqlViewDeviceModel> result = new List<WsSqlViewDeviceModel>();
        string query = WsSqlQueriesDiags.Views.GetDevices(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 8 || !Guid.TryParse(Convert.ToString(item[i++]), out Guid uid)) break;
            result.Add(new() 
            {
                IdentityValueUid = uid,
                IsMarked = Convert.ToBoolean(item[i++]),
                LoginDate = Convert.ToDateTime(item[i++]),
                LogoutDate = Convert.ToDateTime(item[i++]),
                Name = item[i++] as string ?? string.Empty,
                TypeName = item[i++] as string ?? string.Empty,
                Ip = item[i++] as string ?? string.Empty,
                Mac = item[i] as string ?? string.Empty
            });
        }
        return result;
    }
}