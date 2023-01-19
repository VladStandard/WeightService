// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
    /// <param name="assembly"></param>
    /// <param name="formatString"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("api/v2/info/")]
    public ContentResult GetInfo([FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() =>
        {
            AppVersion.Setup(Assembly.GetExecutingAssembly());

            using ISession session = SessionFactory.OpenSession();
            using ITransaction transaction = session.BeginTransaction();
            ISQLQuery sqlQuery = session.CreateSQLQuery(SqlQueriesV2.GetDateTimeNow);
            sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
            string response = sqlQuery.UniqueResult<string>();
            transaction.Commit();
            ServiceInfoModel serviceInfo = new(
                AppVersion.App,
                AppVersion.Version,
                StringUtils.FormatDtEng(DateTime.Now, true),
                response.ToString(CultureInfo.InvariantCulture),
                session.Connection.ConnectionString.ToString(),
                session.Connection.ConnectionTimeout,
                session.Connection.DataSource,
                session.Connection.ServerVersion,
                session.Connection.Database,
                (ulong)Process.GetCurrentProcess().WorkingSet64 / 1048576,
                (ulong)Process.GetCurrentProcess().PrivateMemorySize64 / 1048576);
            return DataFormatUtils.GetContentResult<ServiceInfoModel>(serviceInfo, formatString, HttpStatusCode.OK);
        }, formatString);

    /// <summary>
    /// Get exception.
    /// </summary>
    /// <param name="formatString"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("api/v2/exception/")]
    public ContentResult GetException([FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetException);
            SqlSimpleV1Model sqlSimpleV1 = new(response);
            ContentResult content = DataFormatUtils.GetContentResult<SqlSimpleV1Model>(sqlSimpleV1, formatString, HttpStatusCode.OK);
            return content;
        }, formatString);

    /// <summary>
    /// Get simple.
    /// </summary>
    /// <param name="version"></param>
    /// <param name="formatString"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("api/v2/simple/")]
    public ContentResult GetSimple([FromQuery(Name = "format")] string formatString = "", int version = 0)
    {
        return ControllerHelp.GetContentResult(() =>
        {
            switch (version)
            {
                case 1:
                    string response1 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV1);
                    SqlSimpleV1Model sqlSimpleV1 = DataFormatUtils.DeserializeFromXml<SqlSimpleV1Model>(response1);
                    return DataFormatUtils.GetContentResult<SqlSimpleV1Model>(sqlSimpleV1, formatString, HttpStatusCode.OK);
                case 2:
                    string response2 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV2);
                    SqlSimpleV2Model sqlSimpleV2 = DataFormatUtils.DeserializeFromXml<SqlSimpleV2Model>(response2);
                    return DataFormatUtils.GetContentResult<SqlSimpleV2Model>(sqlSimpleV2, formatString, HttpStatusCode.OK);
                case 3:
                    string response3 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV3);
                    SqlSimpleV3Model sqlSimpleV3 = DataFormatUtils.DeserializeFromXml<SqlSimpleV3Model>(response3);
                    return DataFormatUtils.GetContentResult<SqlSimpleV3Model>(sqlSimpleV3, formatString, HttpStatusCode.OK);
                case 4:
                    string response4 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV4);
                    SqlSimpleV4Model sqlSimpleV4 = DataFormatUtils.DeserializeFromXml<SqlSimpleV4Model>(response4);
                    return DataFormatUtils.GetContentResult<SqlSimpleV4Model>(sqlSimpleV4, formatString, HttpStatusCode.OK);
            }
            SqlSimpleV1Model sqlSimpleV1Default = new("Simple method from C Sharp");
            return DataFormatUtils.GetContentResult<SqlSimpleV1Model>(sqlSimpleV1Default, formatString, HttpStatusCode.OK);
        }, formatString);
    }

    #endregion
}