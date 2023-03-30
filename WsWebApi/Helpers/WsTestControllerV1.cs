// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalization.Utils;

namespace WsWebApi.Helpers;

/// <summary>
/// Test controller.
/// </summary>
public sealed class WsTestControllerV1 : WsWebControllerBase
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public WsTestControllerV1(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route(UrlWebService.GetInfoV1)]
    public ContentResult GetInfo([FromQuery(Name = "format")] string format = "") =>
        GetContentResult(() =>
        {
            AppVersion.Setup(Assembly.GetExecutingAssembly());

            using ISession session = SessionFactory.OpenSession();
            using ITransaction transaction = session.BeginTransaction();
            ISQLQuery sqlQuery = session.CreateSQLQuery(SqlQueries.GetDateTimeNow);
            sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
            string response = sqlQuery.UniqueResult<string>();
            transaction.Commit();
            return DataFormatUtils.GetContentResult<WsServiceInfoModel>(new WsServiceInfoModel(Environment.MachineName, AppVersion.App,
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
    public ContentResult GetException([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false) =>
        GetContentResult(() =>
        {
            string response = WsWebSqlUtils.GetResponse<string>(SessionFactory, SqlQueries.GetException);
            return DataFormatUtils.GetContentResult<WsSqlSimpleV1Model>(new WsSqlSimpleV1Model(response, isDebug), format, HttpStatusCode.OK);
        }, format);

    [AllowAnonymous]
    [HttpGet]
    [Route(UrlWebService.GetSimpleV1)]
    public ContentResult GetSimple([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false,
        int version = 0) =>
        GetContentResult(() =>
        {
            switch (version)
            {
                case 1:
                    string response1 = WsWebSqlUtils.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV1);
                    return DataFormatUtils.GetContentResult<WsSqlSimpleV1Model>(
                        DataFormatUtils.DeserializeFromXml<WsSqlSimpleV1Model>(response1), format, HttpStatusCode.OK);
                case 2:
                    string response2 = WsWebSqlUtils.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV2);
                    return DataFormatUtils.GetContentResult<WsSqlSimpleV2Model>(
                        DataFormatUtils.DeserializeFromXml<WsSqlSimpleV2Model>(response2), format, HttpStatusCode.OK);
                case 3:
                    string response3 = WsWebSqlUtils.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV3);
                    return DataFormatUtils.GetContentResult<WsSqlSimpleV3Model>(
                        DataFormatUtils.DeserializeFromXml<WsSqlSimpleV3Model>(response3),
                        format, HttpStatusCode.OK);
                case 4:
                    string response4 = WsWebSqlUtils.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV4);
                    return DataFormatUtils.GetContentResult<WsSqlSimpleV4Model>(
                        DataFormatUtils.DeserializeFromXml<WsSqlSimpleV4Model>(response4),
                        format, HttpStatusCode.OK);
            }
            return DataFormatUtils.GetContentResult<WsSqlSimpleV1Model>(
                new WsSqlSimpleV1Model("Simple method from C Sharp", isDebug),
                format, HttpStatusCode.OK);
        }, format);

    #endregion
}