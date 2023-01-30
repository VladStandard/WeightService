// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DevExpress.Xpo.Logger.Transport;

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
    /// Log the request.
    /// </summary>
    /// <param name="serviceLogType"></param>
    /// <param name="appName"></param>
    /// <param name="dtStamp"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    private async Task LogCore(ServiceLogType serviceLogType, string appName, DateTime dtStamp, string text)
    {
        string dtString = StringUtils.FormatDtEng(dtStamp, true).Replace(':', '.');
        // Get directory name.
        if (!Directory.Exists(RootDirectory)) return;
        string directory = RootDirectory + @$"{Environment.MachineName}";
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        // Get file name.
        string filePath = @$"{directory}\{appName}_{dtString}";
        filePath += serviceLogType switch
        {
            ServiceLogType.Request => ".request",
            ServiceLogType.Response => ".response",
            ServiceLogType.MetaData => ".metadata",
            _ => ".txt"
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
    /// <param name="dtStamp"></param>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public async Task LogRequest(string appName, DateTime dtStamp, string xml, string format, string host, string version)
    {
        await LogCore(ServiceLogType.Request, appName, dtStamp, xml).ConfigureAwait(false);

        string text = $"DateTime stamp: {DateTime.Now}" + Environment.NewLine;
        text += $"{nameof(format)}: {format}" + Environment.NewLine;
        text += $"{nameof(host)}: {host}" + Environment.NewLine;
        text += $"{nameof(version)}: {version}" + Environment.NewLine;
        text += $"Request data: {nameof(xml)}.{nameof(string.Length)}: {xml.Length} B | {xml.Length / 1024} KB | {xml.Length / 1024 / 1024} MB" + Environment.NewLine;
        await LogCore(ServiceLogType.MetaData, appName, dtStamp, text).ConfigureAwait(false);
    }

    /// <summary>
    /// Log the request.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="dtStamp"></param>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public async Task LogRequest(string appName, DateTime dtStamp, XElement xml, string format, string host, string version) =>
        await LogRequest(appName, dtStamp, xml.ToString(), format, host, version).ConfigureAwait(false);

    /// <summary>
    /// Log the response.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="dtStamp"></param>
    /// <param name="result"></param>
    /// <param name="format"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task LogResponse(string appName, DateTime dtStamp, ContentResult result, string format, string version)
    {
        await LogCore(ServiceLogType.Response, appName, dtStamp, result.Content).ConfigureAwait(false);

        string text = $"DateTime stamp: {DateTime.Now}" + Environment.NewLine;
        text += $"{nameof(format)}: {format}" + Environment.NewLine;
        text += $"{nameof(version)}: {version}" + Environment.NewLine;
        text += $"Response data: {nameof(result.Content)}.{nameof(string.Length)}: {result.Content.Length} B | {result.Content.Length / 1024} KB | {result.Content.Length / 1024 / 1024} MB" + Environment.NewLine;
        await LogCore(ServiceLogType.MetaData, appName, dtStamp, text).ConfigureAwait(false);
    }

    #endregion
}