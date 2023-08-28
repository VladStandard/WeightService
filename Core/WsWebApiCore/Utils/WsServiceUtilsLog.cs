namespace WsWebApiCore.Utils;

/// <summary>
/// Утилиты логов веб-сервиса.
/// </summary>
public static class WsServiceUtilsLog
{
    #region Public and private methods

    /// <summary>
    /// Логирование в файл.
    /// </summary>
    /// <param name="serviceLogType"></param>
    /// <param name="appName"></param>
    /// <param name="api"></param>
    /// <param name="stampDt"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    private static void LogToFileCore(WsEnumServiceLogDirection serviceLogType, string appName, string api, DateTime stampDt, string text)
    {
        string dtString = WsStrUtils.FormatDtEng(stampDt, true).Replace(':', '.');
        // Get directory name.
        if (!Directory.Exists(WsServiceUtils.RootDirectory)) return;
        // Machine dir.
        string directory = WsServiceUtils.RootDirectory + @$"{Environment.MachineName}";
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        // App dir.
        directory = Path.Combine(directory, appName);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        // API dir.
        if (api.StartsWith("api/")) api = api.Remove(0, 4);
        directory = Path.Combine(directory, api);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        // Get file name.
        string filePath = serviceLogType switch
        {
            WsEnumServiceLogDirection.Request => @$"{directory}\{dtString}_request.txt",
            WsEnumServiceLogDirection.Response => @$"{directory}\{dtString}_response.txt",
            WsEnumServiceLogDirection.MetaData => @$"{directory}\{dtString}_metadata.txt",
            _ => @$"{directory}\{dtString}_default.txt"
        };

        // Store data into the log.
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, text, Encoding.UTF8);
        }
        else
        {
            string textExists = File.ReadAllText(filePath);
            text = textExists + Environment.NewLine + text;
            File.Delete(filePath);
            File.WriteAllText(filePath, text, Encoding.UTF8);
        }
    }

    /// <summary>
    /// Логирование запроса и ответа.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="url"></param>
    /// <param name="requestStampDt"></param>
    /// <param name="requestData"></param>
    /// <param name="responseData"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public static void LogWebServiceFk(string appName, string url, DateTime requestStampDt, string requestData,
        string responseData, string format, string host, string version)
    {
        _ = Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            DateTime responseStampDt = DateTime.Now;
            // Parse counts.
            int countAll = WsServiceUtilsGetXmlContent.GetAttributeValueAsInt(requestData, "Count");
            int countSuccess = WsServiceUtilsGetXmlContent.GetAttributeValueAsInt(responseData, nameof(WsResponse1CShortModel.SuccessesCount));
            int countErrors = WsServiceUtilsGetXmlContent.GetAttributeValueAsInt(responseData, nameof(WsResponse1CShortModel.ErrorsCount));

            // Log into DB.
            WsServiceUtils.ContextManager.ContextItem.SaveLogWebService(requestStampDt, requestData, responseStampDt, responseData, WsEnumLogType.Information,
                $"{host}/{url}", "", "", format, countAll, countSuccess, countErrors);

            // Add meta data.
            string metaDataRequest = $"DateTime stamp: {requestStampDt}" + Environment.NewLine;
            metaDataRequest += $"{nameof(url)}: {host}/{url}" + Environment.NewLine;
            metaDataRequest += $"{nameof(format)}: {format}" + Environment.NewLine;
            metaDataRequest += $"{nameof(version)}: {version}" + Environment.NewLine;
            metaDataRequest += $"Request data: {requestData.Length:### ### 000} B | {requestData.Length / 1024:### ###} KB" + Environment.NewLine;
            metaDataRequest += "Request body:" + Environment.NewLine;
            string metaDataResponse = $"DateTime stamp: {responseStampDt}" + Environment.NewLine;
            metaDataResponse += $"{nameof(url)}: " + Environment.NewLine;
            metaDataResponse += $"{nameof(format)}: {format}" + Environment.NewLine;
            metaDataResponse += $"{nameof(version)}: {version}" + Environment.NewLine;
            metaDataResponse += $"Response data: {responseData.Length:### ### 000} B | {responseData.Length / 1024:### ###} KB" + Environment.NewLine;
            metaDataResponse += "Response body:" + Environment.NewLine;

            // Логирование в файл.
            LogToFileCore(WsEnumServiceLogDirection.Request, appName, url, requestStampDt, metaDataRequest + requestData);
            LogToFileCore(WsEnumServiceLogDirection.Response, appName, url, responseStampDt, metaDataResponse + responseData);

            // Log memory into DB.
            //PluginMemory.MemorySize.Execute();
            //WsDataContext.DataAccess.SaveLogMemory(PluginMemory.GetMemorySizeAppMb(), PluginMemory.GetMemorySizeFreeMb());
        }).ConfigureAwait(true);
    }

    /// <summary>
    /// Логирование запроса и ответа.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="url"></param>
    /// <param name="requestStampDt"></param>
    /// <param name="requestXml"></param>
    /// <param name="responseData"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public static void LogWebServiceFk(string appName, string url, DateTime requestStampDt, XElement requestXml,
        string responseData, string format, string host, string version) =>
        LogWebServiceFk(appName, url, requestStampDt, requestXml.ToString(), responseData,
            format, host, version);


    #endregion
}
