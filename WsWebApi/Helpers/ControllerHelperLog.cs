// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// ReSharper disable InconsistentNaming

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
    /// Log the request.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="url"></param>
    /// <param name="dtStamp"></param>
    /// <param name="request"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public async Task LogRequest(string appName, string url, DateTime dtStamp, string request, string format, string host, string version)
    {
        // Parse counts.
        int countAll = GetAttributeValueAsInt(request, "Count");

        // Log into DB.
        DataContext.DataAccess.LogWebService(DateTime.Now, ServiceLogDirection.Request, $"{host}/{url}", "", "",
            format, request, LogType.Information, 
            countAll, 0, 0);
        
        // Add meta data.
        string metaDataText = $"DateTime stamp: {DateTime.Now}" + Environment.NewLine;
        metaDataText += $"{nameof(url)}: {host}/{url}" + Environment.NewLine;
        metaDataText += $"{nameof(format)}: {format}" + Environment.NewLine;
        metaDataText += $"{nameof(version)}: {version}" + Environment.NewLine;
        metaDataText += $"Request data: {request.Length:### ### 000} B | {request.Length / 1024:### ###} KB" + Environment.NewLine;
        metaDataText += "Request body:" + Environment.NewLine;

        // Log into FS.
        request = metaDataText + request;
        await LogToFileCore(ServiceLogDirection.Request, appName, url, dtStamp, request).ConfigureAwait(false);
    }

    /// <summary>
    /// Log the request.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="url"></param>
    /// <param name="dtStamp"></param>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public async Task LogRequest(string appName, string url, DateTime dtStamp, XElement xml, string format, string host, string version) =>
        await LogRequest(appName, url, dtStamp, xml.ToString(), format, host, version).ConfigureAwait(false);

    /// <summary>
    /// Log the response.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="url"></param>
    /// <param name="dtStamp"></param>
    /// <param name="contentResult"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task LogResponse(string appName, string url, DateTime dtStamp, ContentResult contentResult, string format, 
        string host, string version)
    {
        // Parse counts.
        int countSuccess = GetAttributeValueAsInt(contentResult.Content,  nameof(Response1cShortModel.SuccessesCount));
        int countErrors = GetAttributeValueAsInt(contentResult.Content,  nameof(Response1cShortModel.ErrorsCount));
        
        // Log into DB.
        DataContext.DataAccess.LogWebService(DateTime.Now, ServiceLogDirection.Response, $"{host}/{url}", "", "",
            format, contentResult.Content, LogType.Information,
            0, countSuccess, countErrors);
        
        // Add meta data.
        string metaDataText = $"DateTime stamp: {DateTime.Now}" + Environment.NewLine;
        metaDataText += $"{nameof(url)}: " + Environment.NewLine;
        metaDataText += $"{nameof(format)}: {format}" + Environment.NewLine;
        metaDataText += $"{nameof(version)}: {version}" + Environment.NewLine;
        metaDataText += $"Response data: {contentResult.Content.Length:### ### 000} B | {contentResult.Content.Length / 1024:### ###} KB" + Environment.NewLine;
        metaDataText += "Response body:" + Environment.NewLine;

        // Log into FS.
        string response = metaDataText + contentResult.Content;
        await LogToFileCore(ServiceLogDirection.Response, appName, url, dtStamp, response).ConfigureAwait(false);
    }

    #endregion
}