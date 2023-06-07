// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Controllers;

/// <summary>
/// Веб-контроллер тест 1.
/// </summary>
public sealed class WsServiceTestV1Controller : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public WsServiceTestV1Controller(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetInfoV1)]
    internal ContentResult GetInfo([FromQuery(Name = "format")] string format = "") =>
        GetContentResult(() =>
        {
            AppVersion.Setup(Assembly.GetExecutingAssembly());

            using ISession session = SessionFactory.OpenSession();
            //using ITransaction transaction = session.BeginTransaction();
            ISQLQuery sqlQuery = session.CreateSQLQuery(WsWebSqlQueries.GetDateTimeNow);
            sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
            string response = sqlQuery.UniqueResult<string>();
            //transaction.Commit();
            return WsDataFormatUtils.GetContentResult<WsServiceInfoModel>(new WsServiceInfoModel(Environment.MachineName, AppVersion.App,
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
    [Route(WsLocaleWebServiceUtils.GetExceptionV1)]
    public ContentResult GetException([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false) =>
        GetContentResult(() =>
        {
            string response = WsServiceSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetException);
            return WsDataFormatUtils.GetContentResult<WsSqlSimpleV1Model>(new WsSqlSimpleV1Model(response, isDebug), format, HttpStatusCode.OK);
        }, format);

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetSimpleV1)]
    public ContentResult GetSimple([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false,
        int version = 0) =>
        GetContentResult(() =>
        {
            switch (version)
            {
                case 1:
                    string response1 = WsServiceSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetXmlSimpleV1);
                    return WsDataFormatUtils.GetContentResult<WsSqlSimpleV1Model>(
                        WsDataFormatUtils.DeserializeFromXml<WsSqlSimpleV1Model>(response1), format, HttpStatusCode.OK);
                case 2:
                    string response2 = WsServiceSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetXmlSimpleV2);
                    return WsDataFormatUtils.GetContentResult<WsSqlSimpleV2Model>(
                        WsDataFormatUtils.DeserializeFromXml<WsSqlSimpleV2Model>(response2), format, HttpStatusCode.OK);
                case 3:
                    string response3 = WsServiceSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetXmlSimpleV3);
                    return WsDataFormatUtils.GetContentResult<WsSqlSimpleV3Model>(
                        WsDataFormatUtils.DeserializeFromXml<WsSqlSimpleV3Model>(response3),
                        format, HttpStatusCode.OK);
                case 4:
                    string response4 = WsServiceSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetXmlSimpleV4);
                    return WsDataFormatUtils.GetContentResult<WsSqlSimpleV4Model>(
                        WsDataFormatUtils.DeserializeFromXml<WsSqlSimpleV4Model>(response4),
                        format, HttpStatusCode.OK);
            }
            return WsDataFormatUtils.GetContentResult<WsSqlSimpleV1Model>(
                new WsSqlSimpleV1Model("Simple method from C Sharp", isDebug),
                format, HttpStatusCode.OK);
        }, format);

    #endregion
}