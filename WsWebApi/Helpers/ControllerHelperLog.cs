// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApi.Helpers;

/// <summary>
/// Web API Controller helper.
/// </summary>
public partial class ControllerHelper
{
    #region Public and private fields, properties, constructor

    private static string RootDirectory => @"\\ds4tb\Dev\WebServicesLogs\";

    #endregion

    #region Public and private methods

    /// <summary>
    /// Log the request into the file.
    /// </summary>
    /// <param name="serviceLogType"></param>
    /// <param name="appName"></param>
    /// <param name="api"></param>
    /// <param name="dtStamp"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    private async Task LogToFileCore(ServiceLogDirection serviceLogType, string appName, string api, DateTime dtStamp, string text)
    {
        string dtString = StringUtils.FormatDtEng(dtStamp, true).Replace(':', '.');
        // Get directory name.
        if (!Directory.Exists(RootDirectory)) return;
        // Machine dir.
        string directory = RootDirectory + @$"{Environment.MachineName}";
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
            ServiceLogDirection.Request => @$"{directory}\{dtString}_request.txt",
            ServiceLogDirection.Response => @$"{directory}\{dtString}_response.txt",
            ServiceLogDirection.MetaData => @$"{directory}\{dtString}_metadata.txt",
            _ => @$"{directory}\{dtString}_default.txt"
        };

        // Store data into the log.
        if (!File.Exists(filePath))
        {
            await File.WriteAllTextAsync(filePath, text, Encoding.UTF8);
        }
        else
        {
            string textExists = await File.ReadAllTextAsync(filePath);
            text = textExists + Environment.NewLine + text;
            File.Delete(filePath);
            await File.WriteAllTextAsync(filePath, text, Encoding.UTF8);
        }
    }

    /// <summary>
    /// Log the request into the DB.
    /// </summary>
    /// <param name="logDirection"></param>
    /// <param name="appName"></param>
    /// <param name="api"></param>
    /// <param name="dtStamp"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    private void LogToDbCore(ServiceLogDirection logDirection, string appName, string api, DateTime dtStamp, string text)
    {
        // Get file name.
        text = logDirection switch
        {
            ServiceLogDirection.Request => 
                $"{LocaleCore.WebService.LogTypeRequest}: {api}" + Environment.NewLine +
                $"{LocaleCore.WebService.DtStamp}: {dtStamp}" + Environment.NewLine + text,
            ServiceLogDirection.Response => 
                $"{LocaleCore.WebService.LogTypeResponse}: {api}" + Environment.NewLine +
                $"{LocaleCore.WebService.DtStamp}: {dtStamp}" + Environment.NewLine + text,
            ServiceLogDirection.MetaData => 
                $"{LocaleCore.WebService.LogTypeMetaData}: {api}" + Environment.NewLine +
                $"{LocaleCore.WebService.DtStamp}: {dtStamp}" + Environment.NewLine + text,
            _ => $"{LocaleCore.WebService.LogTypeRequest}: {api}" + Environment.NewLine +
                 $"{LocaleCore.WebService.DtStamp}: {dtStamp}" + Environment.NewLine + text,
        };
        //DataContext.DataAccess.LogWebInformation(text, logDirection);
    }

    /// <summary>
    /// Log the request.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="query"></param>
    /// <param name="dtStamp"></param>
    /// <param name="request"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public async Task LogRequest(string appName, string query, DateTime dtStamp, string request, string format, string host, string version)
    {
        // Add meta data.
        string metaDataText = $"DateTime stamp: {DateTime.Now}" + Environment.NewLine;
        metaDataText += $"{nameof(query)}: {host}/{query}" + Environment.NewLine;
        metaDataText += $"{nameof(format)}: {format}" + Environment.NewLine;
        metaDataText += $"{nameof(version)}: {version}" + Environment.NewLine;
        metaDataText += $"Request data: {request.Length:### ### 000} B | {request.Length / 1024:### ###} KB" + Environment.NewLine;
        metaDataText += "Request body:" + Environment.NewLine;
        request = metaDataText + request;

        await LogToFileCore(ServiceLogDirection.Request, appName, query, dtStamp, request).ConfigureAwait(false);
        LogToDbCore(ServiceLogDirection.Request, appName, query, dtStamp, request);
    }

    /// <summary>
    /// Log the request.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="query"></param>
    /// <param name="dtStamp"></param>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public async Task LogRequest(string appName, string query, DateTime dtStamp, XElement xml, string format, string host, string version) =>
        await LogRequest(appName, query, dtStamp, xml.ToString(), format, host, version).ConfigureAwait(false);

    /// <summary>
    /// Log the response.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="query"></param>
    /// <param name="dtStamp"></param>
    /// <param name="result"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task LogResponse(string appName, string query, DateTime dtStamp, ContentResult result, string format, 
        string host, string version)
    {
        // Add meta data.
        string metaDataText = $"DateTime stamp: {DateTime.Now}" + Environment.NewLine;
        metaDataText += $"{nameof(query)}: {host}/{query}" + Environment.NewLine;
        metaDataText += $"{nameof(format)}: {format}" + Environment.NewLine;
        metaDataText += $"{nameof(version)}: {version}" + Environment.NewLine;
        metaDataText += $"Response data: {result.Content.Length:### ### 000} B | {result.Content.Length / 1024:### ###} KB" + Environment.NewLine;
        metaDataText += "Response body:" + Environment.NewLine;
        string response = metaDataText + result.Content;

        await LogToFileCore(ServiceLogDirection.Response, appName, query, dtStamp, response).ConfigureAwait(false);
        LogToDbCore(ServiceLogDirection.Response, appName, query, dtStamp, response);
    }

    #endregion
}
