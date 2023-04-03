// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableDiagModels.LogsTypes;
using DataCore.Sql.TableDiagModels.LogsWebs;
using DataCore.Sql.TableDiagModels.LogsWebsFks;

namespace DataCore.Sql.Core.Helpers;

public partial class DataAccessHelper
{
    #region Public and private methods

    public void LogWebService(DateTime requestStampDt, string requestDataString,
        DateTime responseStampDt, string responseDataString, LogType logType, 
        string url, string parameters, string headers, FormatType formatType, int countAll, int countSuccess, int countErrors) =>
        LogWebService(requestStampDt, requestDataString, responseStampDt, responseDataString, logType,
            url, parameters, headers, (byte)formatType, countAll, countSuccess, countErrors);

    public void LogWebService(DateTime requestStampDt, string requestDataString,
        DateTime responseStampDt, string responseDataString, LogType logType,
        string url, string parameters, string headers, string format, int countAll, int countSuccess, int countErrors) =>
        LogWebService(requestStampDt, requestDataString, responseStampDt, responseDataString, logType,
            url, parameters, headers, (byte)DataFormatUtils.GetFormatType(format), countAll, countSuccess, countErrors);

    private void LogWebService(DateTime requestStampDt, string requestDataString, 
        DateTime responseStampDt, string responseDataString, LogType logType,
        string url, string parameters, string headers,
        byte formatType, int countAll, int countSuccess, int countErrors)
    {
        LogWebModel logWebRequest = new()
        {
            CreateDt = DateTime.Now,
            StampDt = requestStampDt,
            IsMarked = false,
            Version = AppVersion.Version,
            Direction = (byte)ServiceLogDirection.Request,
            Url = url,
            Params = parameters,
            Headers = headers,
            DataType = formatType,
            DataString = requestDataString,
            CountAll = countAll,
            CountSuccess = countSuccess,
            CountErrors = countErrors,
        };
        Save(logWebRequest);

        LogWebModel logWebResponse = new()
        {
            CreateDt = DateTime.Now,
            StampDt = responseStampDt,
            IsMarked = false,
            Version = AppVersion.Version,
            Direction = (byte)ServiceLogDirection.Response,
            Url = url,
            Params = parameters,
            Headers = headers,
            DataType = formatType,
            DataString = responseDataString,
            CountAll = countAll,
            CountSuccess = countSuccess,
            CountErrors = countErrors,
        };
        Save(logWebResponse);

        LogTypeModel logTypeItem = GetItemLogTypeNotNullable(logType);
        LogWebFkModel logWebFk = new()
        {
            LogWebRequest = logWebRequest,
            LogWebResponse = logWebResponse,
            App = App,
            LogType = logTypeItem,
            Device = Device,
        };
        SaveAsync(logWebFk).ConfigureAwait(false);
    }

    #endregion
}