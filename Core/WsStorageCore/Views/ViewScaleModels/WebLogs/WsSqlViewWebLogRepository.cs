// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewScaleModels.WebLogs;

public class WsSqlViewWebLogRepository : IViewWebLogRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewWebLogModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewWebLogModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetWebLogs(sqlCrudConfig.SelectTopRowsCount);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 8 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
            result.Add(new()
            {
                IdentityValueUid = uid,
                CreateDt = Convert.ToDateTime(item[i++]),
                RequestUrl = item[i++] as string ?? string.Empty,
                RequestCountAll = Convert.ToInt32(item[i++]),
                ResponseCountSuccess = Convert.ToInt32(item[i++]),
                ResponseCountError = Convert.ToInt32(item[i++]),
                AppVersion = item[i++] as string ?? string.Empty
            });
        }
        return result;
    }
    
}