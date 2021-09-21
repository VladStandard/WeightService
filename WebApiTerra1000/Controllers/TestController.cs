// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using WebApiTerra1000.Common;
using WebApiTerra1000.Utils;
using static WebApiTerra1000.Utils.TerraEnums;

namespace WebApiTerra1000.Controllers
{
    public class TestController : BaseController
    {
        #region Public and private fields and properties

        private readonly AppVersionHelper _appVersion = AppVersionHelper.Instance;

        #endregion

        #region Constructor and destructor

        public TestController(ILogger<TestController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
        }

        #endregion

        #region Public and private methods

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/info/")]
        public ContentResult GetInfo(FormatType format = FormatType.Xml)
        {
            _appVersion.Setup(Assembly.GetExecutingAssembly());
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                string response = session.CreateSQLQuery(SqlQueries.GetDateTimeNow).UniqueResult<string>();
                transaction.Commit();
                
                ServiceInfoEntity serviceInfo = new(_appVersion.App, _appVersion.Version,
                    DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    response.ToString(CultureInfo.InvariantCulture),
                    session.Connection.ConnectionString.ToString(),
                    session.Connection.ConnectionTimeout,
                    session.Connection.DataSource,
                    session.Connection.ServerVersion,
                    session.Connection.Database,
                    (ulong)Process.GetCurrentProcess().WorkingSet64 / 1048576,
                    (ulong)Process.GetCurrentProcess().PrivateMemorySize64 / 1048576);
                return format switch
                {
                    FormatType.Json => TerraUtils.GetResult(FormatType.Json, serviceInfo.SerializeAsJson(), HttpStatusCode.OK),
                    FormatType.Xml => TerraUtils.GetResult(FormatType.Xml, serviceInfo.SerializeAsXml(), HttpStatusCode.OK),
                    FormatType.Html => TerraUtils.GetResult(FormatType.Html, serviceInfo.SerializeAsHtml(), HttpStatusCode.OK),
                    FormatType.Text => TerraUtils.GetResult(FormatType.Text, serviceInfo.SerializeAsText(), HttpStatusCode.OK),
                    FormatType.Raw => TerraUtils.GetResult(FormatType.Text, response, HttpStatusCode.OK),
                    _ => throw TerraUtils.GetArgumentException(nameof(format)),
                };
            }), format);
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/exception/")]
        public ContentResult GetException(FormatType format = FormatType.Xml)
        {
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetException);
                SqlSimpleEntity simple = new(response);
                return format switch
                {
                    FormatType.Json => TerraUtils.GetResult(FormatType.Json, simple.SerializeAsJson(), HttpStatusCode.OK),
                    FormatType.Xml => TerraUtils.GetResult(FormatType.Xml, simple.SerializeAsXml(), HttpStatusCode.OK),
                    FormatType.Html => TerraUtils.GetResult(FormatType.Html, simple.SerializeAsHtml(), HttpStatusCode.OK),
                    FormatType.Text => TerraUtils.GetResult(FormatType.Text, simple.SerializeAsText(), HttpStatusCode.OK),
                    FormatType.Raw => TerraUtils.GetResult(FormatType.Text, simple.SerializeAsText(), HttpStatusCode.OK),
                    _ => throw TerraUtils.GetArgumentException(nameof(format)),
                };
            }), format);
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/simple/")]
        public ContentResult GetSimple(FormatType format = FormatType.Xml, int version = 0)
        {
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                switch (version)
                {
                    case 1:
                        string response1 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV1);
                        SqlSimpleEntity item1 = SqlSimpleEntity.DeserializeFromXml(response1);
                        return format switch
                        {
                            FormatType.Json => TerraUtils.GetResult(FormatType.Json, item1.SerializeAsJson(), HttpStatusCode.OK),
                            FormatType.Xml => TerraUtils.GetResult(FormatType.Xml, item1.SerializeAsXml(), HttpStatusCode.OK),
                            FormatType.Html => TerraUtils.GetResult(FormatType.Html, item1.SerializeAsHtml(), HttpStatusCode.OK),
                            FormatType.Text => TerraUtils.GetResult(FormatType.Text, item1.SerializeAsText(), HttpStatusCode.OK),
                            FormatType.Raw => TerraUtils.GetResult(FormatType.Raw, response1, HttpStatusCode.OK),
                            _ => throw TerraUtils.GetArgumentException(nameof(format)),
                        };
                    case 2:
                        string response2 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV2);
                        SqlResponseSimpleEntity item2 = SqlResponseSimpleEntity.DeserializeFromXml(response2);
                        return format switch
                        {
                            FormatType.Json => TerraUtils.GetResult(FormatType.Json, item2.SerializeAsJson(), HttpStatusCode.OK),
                            FormatType.Xml => TerraUtils.GetResult(FormatType.Xml, item2.SerializeAsXml(), HttpStatusCode.OK),
                            FormatType.Html => TerraUtils.GetResult(FormatType.Html, item2.SerializeAsHtml(), HttpStatusCode.OK),
                            FormatType.Text => TerraUtils.GetResult(FormatType.Text, item2.SerializeAsText(), HttpStatusCode.OK),
                            FormatType.Raw => TerraUtils.GetResult(FormatType.Raw, response2, HttpStatusCode.OK),
                            _ => throw TerraUtils.GetArgumentException(nameof(format)),
                        };
                    case 3:
                        string response3 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV3);
                        SqlResponseSimplesEntity item3 = SqlResponseSimplesEntity.DeserializeFromXml(response3);
                        return format switch
                        {
                            FormatType.Json => TerraUtils.GetResult(FormatType.Json, item3.SerializeAsJson(), HttpStatusCode.OK),
                            FormatType.Xml => TerraUtils.GetResult(FormatType.Xml, item3.SerializeAsXml(), HttpStatusCode.OK),
                            FormatType.Html => TerraUtils.GetResult(FormatType.Html, item3.SerializeAsHtml(), HttpStatusCode.OK),
                            FormatType.Text => TerraUtils.GetResult(FormatType.Text, item3.SerializeAsText(), HttpStatusCode.OK),
                            FormatType.Raw => TerraUtils.GetResult(FormatType.Raw, response3, HttpStatusCode.OK),
                            _ => throw TerraUtils.GetArgumentException(nameof(format)),
                        };
                }
                SqlSimpleEntity item = new("Simple method from C Sharp");
                return format switch
                {
                    FormatType.Json => TerraUtils.GetResult(FormatType.Json, item.SerializeAsJson(), HttpStatusCode.OK),
                    FormatType.Xml => TerraUtils.GetResult(FormatType.Xml, item.SerializeAsXml(), HttpStatusCode.OK),
                    FormatType.Html => TerraUtils.GetResult(FormatType.Html, item.SerializeAsHtml(), HttpStatusCode.OK),
                    FormatType.Text => TerraUtils.GetResult(FormatType.Text, item.SerializeAsText(), HttpStatusCode.OK),
                    FormatType.Raw => TerraUtils.GetResult(FormatType.Raw, item.ToString(), HttpStatusCode.OK),
                    _ => throw TerraUtils.GetArgumentException(nameof(format)),
                };
            }), format);
        }

        #endregion
    }
}
