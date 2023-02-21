// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.LogsWebs;

namespace DataCore.Sql.Core.Helpers;

public partial class DataAccessHelper
{
	#region Public and private methods

    private void LogWebCore(ServiceLogDirection logDirection, string url, string parameters, string headers,
        byte dataType, XmlDocument dataXml, string dataString, int countAll, int countSuccess, int countErrors,
        LogType logType, string filePath, int lineNumber, string memberName)
    {
        StringUtils.SetStringValueTrim(ref filePath, 64, true);
        StringUtils.SetStringValueTrim(ref memberName, 64);
        LogTypeModel? logTypeItem = GetItemLogTypeNullable(logType);

        LogWebModel log = new()
        {
            CreateDt = DateTime.Now,
            //StampDt = stampDt,
            IsMarked = false,
            Version = AppVersion.Version,
            File = filePath,
            Line = lineNumber,
            Member = memberName,
            Direction = (byte)logDirection,
            Url = url,
            Params = parameters,
            Headers = headers,
            DataType = dataType,
            DataXml = dataXml,
            DataString = dataString,
            CountAll = countAll,
            CountSuccess = countSuccess,
            CountErrors = countErrors,
        };
        SaveAsync(log).ConfigureAwait(false);
    }

    public void LogWebError(ServiceLogDirection logDirection, string url, string parameters, string headers,
        byte dataType, XmlDocument dataXml, string dataString, int countAll, int countSuccess, int countErrors,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") => 
        LogWebCore(logDirection, url, parameters, headers,
            dataType, dataXml, dataString, countAll, countSuccess, countErrors,
            LogType.Error, filePath, lineNumber, memberName);
	
    public void LogWebInformation(ServiceLogDirection logDirection, string url, string parameters, string headers,
        byte dataType, XmlDocument dataXml, string dataString, int countAll, int countSuccess, int countErrors,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") => 
        LogWebCore(logDirection, url, parameters, headers,
            dataType, dataXml, dataString, countAll, countSuccess, countErrors,
            LogType.Information, filePath, lineNumber, memberName);
	
	#endregion
}