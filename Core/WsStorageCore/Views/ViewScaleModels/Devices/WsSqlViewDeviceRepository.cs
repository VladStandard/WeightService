namespace WsStorageCore.Views.ViewScaleModels.Devices;

public class WsSqlViewDeviceRepository
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlViewDeviceRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlViewDeviceRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewDeviceModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewDeviceModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetDevices(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (var obj in objects)
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