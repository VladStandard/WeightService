// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
    [Route("api/v1/info/")]
    public ContentResult GetInfo([FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() =>
        {
            AppVersion.Setup(Assembly.GetExecutingAssembly());

            using ISession session = SessionFactory.OpenSession();
            using ITransaction transaction = session.BeginTransaction();
            ISQLQuery sqlQuery = session.CreateSQLQuery(SqlQueries.GetDateTimeNow);
            sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
            string response = sqlQuery.UniqueResult<string>();
            transaction.Commit();

            return new ServiceInfoModel(AppVersion.App, AppVersion.Version,
                DateTime.Now.ToString(CultureInfo.InvariantCulture),
                response.ToString(CultureInfo.InvariantCulture),
                session.Connection.ConnectionString.ToString(),
                session.Connection.ConnectionTimeout,
                session.Connection.DataSource,
                session.Connection.ServerVersion,
                session.Connection.Database,
                (ulong)Process.GetCurrentProcess().WorkingSet64 / 1048576,
                (ulong)Process.GetCurrentProcess().PrivateMemorySize64 / 1048576)
            .GetContentResult<ServiceInfoModel>(formatString, HttpStatusCode.OK);
        }, formatString);

    [AllowAnonymous]
    [HttpGet]
    [Route("api/v1/exception/")]
    public ContentResult GetException([FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetException);

            return new SqlSimpleV1Model(response).GetContentResult<SqlSimpleV1Model>(formatString, HttpStatusCode.OK);
        }, formatString);

    [AllowAnonymous]
    [HttpGet]
    [Route("api/v1/simple/")]
    public ContentResult GetSimple([FromQuery(Name = "format")] string formatString = "", int version = 0)
    {
        return ControllerHelp.GetContentResult(() =>
        {
            switch (version)
            {      
                case 1:
                    string response1 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV1);
                    return DataFormatUtils.DeserializeFromXml<SqlSimpleV1Model>(response1)
                        .GetContentResult<SqlSimpleV1Model>(formatString, HttpStatusCode.OK);
                case 2:
                    string response2 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV2);
                    return DataFormatUtils.DeserializeFromXml<SqlSimpleV2Model>(response2)
                        .GetContentResult<SqlSimpleV2Model>(formatString, HttpStatusCode.OK);
                case 3:
                    string response3 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV3);
                    return DataFormatUtils.DeserializeFromXml<SqlSimpleV3Model>(response3)
                        .GetContentResult<SqlSimpleV3Model>(formatString, HttpStatusCode.OK);
                case 4:
                    string response4 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV4);
                    return DataFormatUtils.DeserializeFromXml<SqlSimpleV4Model>(response4)
                        .GetContentResult<SqlSimpleV4Model>(formatString, HttpStatusCode.OK);
            }

            return new SqlSimpleV1Model("Simple method from C Sharp")
                .GetContentResult<SqlSimpleV1Model>(formatString, HttpStatusCode.OK);
        }, formatString);
    }

    #endregion
}
