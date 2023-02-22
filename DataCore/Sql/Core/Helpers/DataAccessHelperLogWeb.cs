// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.LogsWebsFks;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.LogsWebs;

namespace DataCore.Sql.Core.Helpers;

public partial class DataAccessHelper
{
    #region Public and private methods

    public void LogWebService(DateTime stampDt, ServiceLogDirection logDirection, string url, string parameters, string headers, 
        FormatType formatType, string dataString, int countAll, int countSuccess, int countErrors, LogType logType) =>
        LogWebService(stampDt, logDirection, url, parameters, headers, (byte)formatType, dataString,
            countAll, countSuccess, countErrors, logType);

    public void LogWebService(DateTime stampDt, ServiceLogDirection logDirection, string url, string parameters, string headers, 
        string format, string dataString, int countAll, int countSuccess, int countErrors, LogType logType) =>
        LogWebService(stampDt, logDirection, url, parameters, headers, (byte)DataFormatUtils.GetFormatType(format), dataString,
            countAll, countSuccess, countErrors, logType);

    private void LogWebService(DateTime stampDt, ServiceLogDirection logDirection, string url, string parameters, string headers,
        byte formatType, string dataString, int countAll, int countSuccess, int countErrors, LogType logType)
    {
        LogWebModel logWeb = new()
        {
            CreateDt = DateTime.Now,
            StampDt = stampDt,
            IsMarked = false,
            Version = AppVersion.Version,
            Direction = (byte)logDirection,
            Url = url,
            Params = parameters,
            Headers = headers,
            DataType = formatType,
            DataString = dataString,
            CountAll = countAll,
            CountSuccess = countSuccess,
            CountErrors = countErrors,
        };
        SaveAsync(logWeb).ConfigureAwait(false);

        LogTypeModel logTypeItem = GetItemLogTypeNotNullable(logType);
        LogWebFkModel logWebFk = new()
        {
            LogWeb = logWeb,
            App = App,
            LogType = logTypeItem,
            Device = Device,
        };
        SaveAsync(logWebFk).ConfigureAwait(false);
    }

    #endregion
}