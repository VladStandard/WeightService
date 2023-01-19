// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Utils;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using WsStorage.Utils;
using WsWebApi.Controllers;
using WsWebApi.Models;

namespace WebApiScales.Controllers;

/// <summary>
/// Test controller v3.
/// </summary>
public class TestControllerV3 : WebControllerBase
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public TestControllerV3(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Get info.
    /// </summary>
    /// <param name="formatString"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("api/info/")]
    [Route("api/v3/info/")]
    public ContentResult GetInfo([FromQuery(Name = "format")] string formatString = "") => 
        ControllerHelp.GetContentResult(() =>
        {
            AppVersion.Setup(Assembly.GetExecutingAssembly());

            using NHibernate.ISession session = SessionFactory.OpenSession();
            using ITransaction transaction = session.BeginTransaction();
            ISQLQuery sqlQuery = session.CreateSQLQuery(SqlQueriesV2.GetDateTimeNow);
            sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
            string response = sqlQuery.UniqueResult<string>();
            transaction.Commit();

            return new ServiceInfoModel(
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
                (ulong)Process.GetCurrentProcess().PrivateMemorySize64 / 1048576)
            .GetContentResult<ServiceInfoModel>(formatString, HttpStatusCode.OK);
        }, formatString);

    [AllowAnonymous]
    [HttpGet]
    [Route("api/exception/")]
    [Route("api/v3/exception/")]
    public ContentResult GetException([FromQuery(Name = "format")] string formatString = "",
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        ControllerHelp.GetContentResult(() =>
        {
            return new ServiceExceptionModel(filePath, lineNumber, memberName, "Test Exception!", "Test inner exception!")
                .GetContentResult<ServiceExceptionModel>(formatString, HttpStatusCode.InternalServerError);
        }, formatString);

    [AllowAnonymous]
    [HttpGet]
    [Route("api/simple/")]
    [Route("api/v3/simple/")]
    public ContentResult GetSimple([FromQuery(Name = "format")] string formatString = "") =>
        new TestControllerV2(SessionFactory).GetSimple(formatString);

    #endregion
}
