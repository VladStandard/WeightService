// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewScaleModels.Devices;

public class WsSqlViewDeviceRepository : IViewDeviceRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewDeviceModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewDeviceModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetDevices(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 8 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
            result.Add(new() 
            {
                IdentityValueUid = uid,
                IsMarked = Convert.ToBoolean(item[i++]),
                LoginDate = Convert.ToDateTime(item[i++]),
                LogoutDate = Convert.ToDateTime(item[i++]),
                Name = item[i++] as string ?? string.Empty,
                TypeName = item[i++] as string ?? string.Empty,
                Ip = item[i++] as string ?? string.Empty,
                Mac = item[i++] as string ?? string.Empty
            });
        }
        return result;
    }
}