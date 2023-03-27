// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalization.Utils;

namespace WsWebApi.Controllers;

/// <summary>
/// Test controller.
/// </summary>
public class TestControllerV1 : WebControllerBase
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public TestControllerV1(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route(UrlWebService.GetInfoV1)]
    public ContentResult GetInfo([FromQuery(Name = "format")] string format = "") =>
        ControllerHelp.GetContentResult(() =>
        {
            AppVersion.Setup(Assembly.GetExecutingAssembly());

            using ISession session = SessionFactory.OpenSession();
            using ITransaction transaction = session.BeginTransaction();
            ISQLQuery sqlQuery = session.CreateSQLQuery(SqlQueries.GetDateTimeNow);
            sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
            string response = sqlQuery.UniqueResult<string>();
            transaction.Commit();
            return DataFormatUtils.GetContentResult<ServiceInfoModel>(new ServiceInfoModel(Environment.MachineName, AppVersion.App,
                AppVersion.Version,
                DateTime.Now.ToString(CultureInfo.InvariantCulture),
                response.ToString(CultureInfo.InvariantCulture),
                session.Connection.ConnectionString,
                session.Connection.ConnectionTimeout,
                session.Connection.DataSource,
                session.Connection.ServerVersion,
                session.Connection.Database,
                (ulong)Process.GetCurrentProcess().WorkingSet64 / 1048576,
                (ulong)Process.GetCurrentProcess().PrivateMemorySize64 / 1048576), format, HttpStatusCode.OK);
        }, format);

    [AllowAnonymous]
    [HttpGet]
    [Route(UrlWebService.GetExceptionV1)]
    public ContentResult GetException([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "is_debug")] bool isDebug = false) =>
        ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetException);
            return DataFormatUtils.GetContentResult<SqlSimpleV1Model>(new SqlSimpleV1Model(response, isDebug), format, HttpStatusCode.OK);
        }, format);

    [AllowAnonymous]
    [HttpGet]
    [Route(UrlWebService.GetSimpleV1)]
    public ContentResult GetSimple([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "is_debug")] bool isDebug = false, 
        int version = 0)
    {
        return ControllerHelp.GetContentResult(() =>
        {
            switch (version)
            {      
                case 1:
                    string response1 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV1);
                    return DataFormatUtils.GetContentResult<SqlSimpleV1Model>(
                        DataFormatUtils.DeserializeFromXml<SqlSimpleV1Model>(response1), format, HttpStatusCode.OK);
                case 2:
                    string response2 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV2);
                    return DataFormatUtils.GetContentResult<SqlSimpleV2Model>(
                        DataFormatUtils.DeserializeFromXml<SqlSimpleV2Model>(response2), format, HttpStatusCode.OK);
                case 3:
                    string response3 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV3);
                    return DataFormatUtils.GetContentResult<SqlSimpleV3Model>(
                        DataFormatUtils.DeserializeFromXml<SqlSimpleV3Model>(response3), 
                        format, HttpStatusCode.OK);
                case 4:
                    string response4 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV4);
                    return DataFormatUtils.GetContentResult<SqlSimpleV4Model>(
                        DataFormatUtils.DeserializeFromXml<SqlSimpleV4Model>(response4),
                        format, HttpStatusCode.OK);
            }
            return DataFormatUtils.GetContentResult<SqlSimpleV1Model>(
                new SqlSimpleV1Model("Simple method from C Sharp", isDebug),
                format, HttpStatusCode.OK);
        }, format);
    }

    #endregion
}
