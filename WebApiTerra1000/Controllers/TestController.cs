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
using static DataShareCore.ShareEnums;

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
                //return TerraUtils.GetResultWithWrap(format, serviceInfo, HttpStatusCode.OK);
                return serviceInfo.GetResult(format, HttpStatusCode.OK);
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
                SqlSimpleV1Entity simple = new(response);
                //return TerraUtils.GetResultWithWrap(format, simple, HttpStatusCode.OK);
                return simple.GetResult(format, HttpStatusCode.OK);
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
                        SqlSimpleV1Entity item1 = SqlSimpleV1Entity.DeserializeFromXml(response1);
                        //return TerraUtils.GetResultWithWrap(format, item1, HttpStatusCode.OK);
                        return item1.GetResult(format, HttpStatusCode.OK);
                    case 2:
                        string response2 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV2);
                        SqlSimpleV2Entity item2 = SqlSimpleV2Entity.DeserializeFromXml(response2);
                        //return TerraUtils.GetResultWithWrap(format, item2, HttpStatusCode.OK);
                        return item2.GetResult(format, HttpStatusCode.OK);
                    case 3:
                        string response3 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV3);
                        SqlSimpleV3Entity item3 = SqlSimpleV3Entity.DeserializeFromXml(response3);
                        //return TerraUtils.GetResultWithWrap(format, item3, HttpStatusCode.OK);
                        return item3.GetResult(format, HttpStatusCode.OK);
                    case 4:
                        string response4 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV4);
                        SqlSimpleV4Entity item4 = SqlSimpleV4Entity.DeserializeFromXml(response4);
                        //return TerraUtils.GetResultWithWrap(format, item4, HttpStatusCode.OK);
                        return item4.GetResult(format, HttpStatusCode.OK);
                }
                SqlSimpleV1Entity item = new("Simple method from C Sharp");
                //return TerraUtils.GetResultWithWrap(format, item, HttpStatusCode.OK);
                return item.GetResult(format, HttpStatusCode.OK);
            }), format);
        }

        #endregion
    }
}
