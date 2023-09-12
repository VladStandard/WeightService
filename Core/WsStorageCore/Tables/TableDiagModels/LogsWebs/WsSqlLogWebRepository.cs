namespace WsStorageCore.Tables.TableDiagModels.LogsWebs;

public class WsSqlLogWebRepository: WsSqlTableRepositoryBase<WsSqlLogWebModel>
{
    public List<WsSqlLogWebModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.CreateDt), WsSqlEnumOrder.Desc);
        return SqlCore.GetEnumerableNotNullable<WsSqlLogWebModel>(sqlCrudConfig).ToList();
    }
    
    public void Save(DateTime requestStampDt, string requestDataString, string responseDataString, string url,
        int success, int errors)
    {
        WsSqlLogWebModel webLog = new()
        {
            CreateDt = requestStampDt,
            StampDt = DateTime.Now,
            Version = WsAppVersionHelper.Instance.Version,
            Url = url,
            DataRequest = requestDataString,
            DataResponse = responseDataString,
            CountSuccess = success,
            CountErrors = errors,
            CountAll = errors + success
        };
        SqlCore.Save(webLog, WsSqlEnumSessionType.IsolatedAsync);
    }

}