namespace WsWebApiScales.Helpers;

/// <summary>
/// Test controller v3.
/// </summary>
[Tags(WsLocaleWebServiceUtils.TagTests)]
public sealed class WsServiceTestWrapper : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    public WsServiceTestWrapper(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Get info.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetInfo)]
    public ContentResult GetInfo([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "")
    {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = WsServiceUtilsGetXmlContent.GetContentResult(() => 
            WsDataFormatUtils.GetContentResult<WsServiceInfoModel>(
            WsServiceUtilsResponse.NewServiceInfo(Assembly.GetExecutingAssembly(), SessionFactory), format, HttpStatusCode.OK), format);
        WsServiceUtilsLog.LogWebServiceFk(nameof(WsWebApiScales), WsLocaleWebServiceUtils.GetInfo,
            requestStampDt, string.Empty, result.Content ?? string.Empty, format, host, version);
        return result;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetException)]
    public ContentResult GetException([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "",
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        WsServiceUtilsGetXmlContent.GetContentResult(() => 
            WsDataFormatUtils.GetContentResult<WsServiceExceptionModel>(
                new WsServiceExceptionModel(filePath, lineNumber, memberName, "Test Exception!", "Test inner exception!"), 
                format, HttpStatusCode.InternalServerError), format);

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetSimple)]
    public ContentResult GetSimple([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") =>
        new WsServiceTestV2Controller(SessionFactory).GetSimple(format, isDebug);

    #endregion
}