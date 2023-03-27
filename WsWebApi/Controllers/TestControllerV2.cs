// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalization.Utils;

namespace WsWebApi.Controllers;

/// <summary>
/// Test controller v2.
/// </summary>
public class TestControllerV2 : WebControllerBase
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public TestControllerV2(ISessionFactory sessionFactory) : base(sessionFactory)
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
    [Route(UrlWebService.GetInfoV2)]
    public ContentResult GetInfo([FromQuery(Name = "format")] string format = "") =>
        ControllerHelp.GetContentResult(() => 
            DataFormatUtils.GetContentResult<ServiceInfoModel>(
            WebResponseUtils.NewServiceInfo(Assembly.GetExecutingAssembly(), SessionFactory), format, HttpStatusCode.OK), format);

    /// <summary>
    /// Get exception.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route(UrlWebService.GetExceptionV2)]
    public ContentResult GetException([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "is_debug")] bool isDebug = false) =>
        ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetException);
            SqlSimpleV1Model sqlSimpleV1 = new(response, isDebug);
            ContentResult content = DataFormatUtils.GetContentResult<SqlSimpleV1Model>(sqlSimpleV1, format, HttpStatusCode.OK);
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
    [Route(UrlWebService.GetSimpleV2)]
    public ContentResult GetSimple([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "is_debug")] bool isDebug = false, 
        int version = 0)
    {
        return ControllerHelp.GetContentResult(() =>
        {
            switch (version)
            {
                case 1:
                    string response1 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV1);
                    SqlSimpleV1Model sqlSimpleV1 = DataFormatUtils.DeserializeFromXml<SqlSimpleV1Model>(response1);
                    return DataFormatUtils.GetContentResult<SqlSimpleV1Model>(sqlSimpleV1, format, HttpStatusCode.OK);
                case 2:
                    string response2 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV2);
                    SqlSimpleV2Model sqlSimpleV2 = DataFormatUtils.DeserializeFromXml<SqlSimpleV2Model>(response2);
                    return DataFormatUtils.GetContentResult<SqlSimpleV2Model>(sqlSimpleV2, format, HttpStatusCode.OK);
                case 3:
                    string response3 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV3);
                    SqlSimpleV3Model sqlSimpleV3 = DataFormatUtils.DeserializeFromXml<SqlSimpleV3Model>(response3);
                    return DataFormatUtils.GetContentResult<SqlSimpleV3Model>(sqlSimpleV3, format, HttpStatusCode.OK);
                case 4:
                    string response4 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV4);
                    SqlSimpleV4Model sqlSimpleV4 = DataFormatUtils.DeserializeFromXml<SqlSimpleV4Model>(response4);
                    return DataFormatUtils.GetContentResult<SqlSimpleV4Model>(sqlSimpleV4, format, HttpStatusCode.OK);
            }
            SqlSimpleV1Model sqlSimpleV1Default = new("Simple method from C Sharp", isDebug);
            return DataFormatUtils.GetContentResult<SqlSimpleV1Model>(sqlSimpleV1Default, format, HttpStatusCode.OK);
        }, format);
    }

    #endregion
}