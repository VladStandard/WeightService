// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using WebApiCore.Models;
using WebApiCore.Utils;

namespace WebApiCore.Controllers;

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
    [HttpGet()]
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

    /// <summary>
    /// Get exception.
    /// </summary>
    /// <param name="formatString"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/exception/")]
    public ContentResult GetException([FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetException);

            return new SqlSimpleV1Model(response).GetContentResult<SqlSimpleV1Model>(formatString, HttpStatusCode.OK);
        }, formatString);

    /// <summary>
    /// Get simple.
    /// </summary>
    /// <param name="version"></param>
    /// <param name="formatString"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/simple/")]
    public ContentResult GetSimple([FromQuery(Name = "format")] string formatString = "", int version = 0)
    {
        return ControllerHelp.GetContentResult(() =>
        {
            switch (version)
            {
                case 1:
                    string response1 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV1);
                    return new SqlSimpleV1Model().DeserializeFromXml<SqlSimpleV1Model>(response1)
                        .GetContentResult<SqlSimpleV1Model>(formatString, HttpStatusCode.OK);
                case 2:
                    string response2 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV2);
                    return new SqlSimpleV2Model().DeserializeFromXml<SqlSimpleV2Model>(response2)
                        .GetContentResult<SqlSimpleV2Model>(formatString, HttpStatusCode.OK);
                case 3:
                    string response3 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV3);
                    return new SqlSimpleV3Model().DeserializeFromXml<SqlSimpleV3Model>(response3)
                        .GetContentResult<SqlSimpleV3Model>(formatString, HttpStatusCode.OK);
                case 4:
                    string response4 = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV4);
                    return new SqlSimpleV4Model().DeserializeFromXml<SqlSimpleV4Model>(response4)
                        .GetContentResult<SqlSimpleV4Model>(formatString, HttpStatusCode.OK);
            }
            return new SqlSimpleV1Model("Simple method from C Sharp")
                .GetContentResult<SqlSimpleV1Model>(formatString, HttpStatusCode.OK);
        }, formatString);
    }

    #endregion
}
