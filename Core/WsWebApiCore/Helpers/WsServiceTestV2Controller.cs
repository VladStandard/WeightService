namespace WsWebApiCore.Helpers;

/// <summary>
/// Веб-контроллер тест 2.
/// </summary>
public sealed class WsServiceTestV2Controller : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    public WsServiceTestV2Controller(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Get info.
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetInfoV2)]
    public ContentResult GetInfo([FromQuery(Name = "format")] string format = "") =>
        WsServiceUtilsGetXmlContent.GetContentResult(() =>
            WsDataFormatUtils.GetContentResult<WsServiceInfoModel>(
                WsServiceUtilsResponse.NewServiceInfo(Assembly.GetExecutingAssembly(), SessionFactory), format, HttpStatusCode.OK), format);

    /// <summary>
    /// Get exception.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetExceptionV2)]
    public ContentResult GetException([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false) =>
        WsServiceUtilsGetXmlContent.GetContentResult(() =>
        {
            string response = WsServiceUtilsSql.GetResponse<string>(SessionFactory, WsWebSqlQueriesV2.GetException);
            WsSqlSimpleV1Model sqlSimpleV1 = new(response, isDebug);
            ContentResult content = WsDataFormatUtils.GetContentResult<WsSqlSimpleV1Model>(sqlSimpleV1, format, HttpStatusCode.OK);
            return content;
        }, format);

    /// <summary>
    /// Get simple.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetSimpleV2)]
    public ContentResult GetSimple([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false,
        int version = 0) =>
        WsServiceUtilsGetXmlContent.GetContentResult(() =>
        {
            switch (version)
            {
                case 1:
                    string response1 = WsServiceUtilsSql.GetResponse<string>(SessionFactory, WsWebSqlQueriesV2.GetXmlSimpleV1);
                    WsSqlSimpleV1Model sqlSimpleV1 = WsDataFormatUtils.DeserializeFromXml<WsSqlSimpleV1Model>(response1);
                    return WsDataFormatUtils.GetContentResult<WsSqlSimpleV1Model>(sqlSimpleV1, format, HttpStatusCode.OK);
                case 2:
                    string response2 = WsServiceUtilsSql.GetResponse<string>(SessionFactory, WsWebSqlQueriesV2.GetXmlSimpleV2);
                    WsSqlSimpleV2Model sqlSimpleV2 = WsDataFormatUtils.DeserializeFromXml<WsSqlSimpleV2Model>(response2);
                    return WsDataFormatUtils.GetContentResult<WsSqlSimpleV2Model>(sqlSimpleV2, format, HttpStatusCode.OK);
                case 3:
                    string response3 = WsServiceUtilsSql.GetResponse<string>(SessionFactory, WsWebSqlQueriesV2.GetXmlSimpleV3);
                    WsSqlSimpleV3Model sqlSimpleV3 = WsDataFormatUtils.DeserializeFromXml<WsSqlSimpleV3Model>(response3);
                    return WsDataFormatUtils.GetContentResult<WsSqlSimpleV3Model>(sqlSimpleV3, format, HttpStatusCode.OK);
                case 4:
                    string response4 = WsServiceUtilsSql.GetResponse<string>(SessionFactory, WsWebSqlQueriesV2.GetXmlSimpleV4);
                    WsSqlSimpleV4Model sqlSimpleV4 = WsDataFormatUtils.DeserializeFromXml<WsSqlSimpleV4Model>(response4);
                    return WsDataFormatUtils.GetContentResult<WsSqlSimpleV4Model>(sqlSimpleV4, format, HttpStatusCode.OK);
            }
            WsSqlSimpleV1Model sqlSimpleV1Default = new("Simple method from C Sharp", isDebug);
            return WsDataFormatUtils.GetContentResult<WsSqlSimpleV1Model>(sqlSimpleV1Default, format, HttpStatusCode.OK);
        }, format);

    #endregion
}