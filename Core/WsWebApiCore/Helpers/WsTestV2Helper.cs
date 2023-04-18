// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Helpers;

/// <summary>
/// Test controller v2.
/// </summary>
public sealed class WsTestV2Helper : WsContentBase
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    internal WsTestV2Helper(ISessionFactory sessionFactory) : base(sessionFactory)
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
    [Route(WsWebServiceUrls.GetInfoV2)]
    public ContentResult GetInfo([FromQuery(Name = "format")] string format = "") =>
        GetContentResult(() =>
            WsDataFormatUtils.GetContentResult<WsServiceInfoModel>(
            WsWebResponseUtils.NewServiceInfo(Assembly.GetExecutingAssembly(), SessionFactory), format, HttpStatusCode.OK), format);

    /// <summary>
    /// Get exception.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route(WsWebServiceUrls.GetExceptionV2)]
    public ContentResult GetException([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false) =>
        GetContentResult(() =>
        {
            string response = WsWebSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueriesV2.GetException);
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
    [Route(WsWebServiceUrls.GetSimpleV2)]
    public ContentResult GetSimple([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false,
        int version = 0) =>
        GetContentResult(() =>
        {
            switch (version)
            {
                case 1:
                    string response1 = WsWebSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueriesV2.GetXmlSimpleV1);
                    WsSqlSimpleV1Model sqlSimpleV1 = WsDataFormatUtils.DeserializeFromXml<WsSqlSimpleV1Model>(response1);
                    return WsDataFormatUtils.GetContentResult<WsSqlSimpleV1Model>(sqlSimpleV1, format, HttpStatusCode.OK);
                case 2:
                    string response2 = WsWebSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueriesV2.GetXmlSimpleV2);
                    WsSqlSimpleV2Model sqlSimpleV2 = WsDataFormatUtils.DeserializeFromXml<WsSqlSimpleV2Model>(response2);
                    return WsDataFormatUtils.GetContentResult<WsSqlSimpleV2Model>(sqlSimpleV2, format, HttpStatusCode.OK);
                case 3:
                    string response3 = WsWebSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueriesV2.GetXmlSimpleV3);
                    WsSqlSimpleV3Model sqlSimpleV3 = WsDataFormatUtils.DeserializeFromXml<WsSqlSimpleV3Model>(response3);
                    return WsDataFormatUtils.GetContentResult<WsSqlSimpleV3Model>(sqlSimpleV3, format, HttpStatusCode.OK);
                case 4:
                    string response4 = WsWebSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueriesV2.GetXmlSimpleV4);
                    WsSqlSimpleV4Model sqlSimpleV4 = WsDataFormatUtils.DeserializeFromXml<WsSqlSimpleV4Model>(response4);
                    return WsDataFormatUtils.GetContentResult<WsSqlSimpleV4Model>(sqlSimpleV4, format, HttpStatusCode.OK);
            }
            WsSqlSimpleV1Model sqlSimpleV1Default = new("Simple method from C Sharp", isDebug);
            return WsDataFormatUtils.GetContentResult<WsSqlSimpleV1Model>(sqlSimpleV1Default, format, HttpStatusCode.OK);
        }, format);

    #endregion
}