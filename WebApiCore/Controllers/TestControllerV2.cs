// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using DataCore.Models;
using DataCore.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using WebApiCore.Models;
using WebApiCore.Utils;

namespace WebApiCore.Controllers;

/// <summary>
/// Test controller v2.
/// </summary>
public class TestControllerV2 : BaseController
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
    /// <param name="format"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/info/")]
    public ContentResult GetInfo(Assembly? assembly, FormatTypeEnum format = FormatTypeEnum.Xml) =>
        ControllerHelp.RunTask(new(() =>
        {
            AppVersion.Setup(assembly ?? Assembly.GetExecutingAssembly());

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
            .GetResult(format, HttpStatusCode.OK);
        }), format);

    /// <summary>
    /// Get exception.
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/exception/")]
    public ContentResult GetException(FormatTypeEnum format = FormatTypeEnum.Xml) =>
        ControllerHelp.RunTask(new(() =>
        {
            string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetException);

            return new SqlSimpleV1Model(response).GetResult(format, HttpStatusCode.OK);
        }), format);

    /// <summary>
    /// Get simple.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/simple/")]
    public ContentResult GetSimple(FormatTypeEnum format = FormatTypeEnum.Xml, int version = 0)
    {
        return ControllerHelp.RunTask(new(() =>
        {
            switch (version)
            {
                case 1:
                    string response1 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV1);
                    return SqlSimpleV1Model.DeserializeFromXml(response1).GetResult(format, HttpStatusCode.OK);
                case 2:
                    string response2 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV2);
                    return SqlSimpleV2Model.DeserializeFromXml(response2).GetResult(format, HttpStatusCode.OK);
                case 3:
                    string response3 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV3);
                    return SqlSimpleV3Model.DeserializeFromXml(response3).GetResult(format, HttpStatusCode.OK);
                case 4:
                    string response4 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV4);
                    return SqlSimpleV4Model.DeserializeFromXml(response4).GetResult(format, HttpStatusCode.OK);
            }
            
            return new SqlSimpleV1Model("Simple method from C Sharp").GetResult(format, HttpStatusCode.OK);
        }), format);
    }

    #endregion
}
