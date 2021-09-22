// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApiTerra1000.Utils;
using static DataShareCore.ShareEnums;

namespace WebApiTerra1000.Controllers
{
    public class NomenclatureController : BaseController
    {
        #region Constructor and destructor

        public NomenclatureController(ILogger<NomenclatureController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
        }

        #endregion

        #region Public and private methods

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/nomenclature/")]
        public ContentResult GetNomenclature(string code, int id, FormatType format = FormatType.Xml)
        {
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                string response = string.Empty;
                if (!string.IsNullOrEmpty(code))
                {
                    response = session.CreateSQLQuery(SqlQueries.GetNomenclatureFromCode)
                        .SetParameter("code", code)
                        .UniqueResult<string>();
                }
                else
                {
                    response = session.CreateSQLQuery(SqlQueries.GetNomenclatureFromId)
                        .SetParameter("id", id)
                        .UniqueResult<string>();
                }
                transaction.Commit();
                XDocument xml = XDocument.Parse(response ?? "<Nomenclature />", LoadOptions.None);
                XDocument doc = new(new XElement("response", xml.Root));
                return new ContentResult
                {
                    ContentType = "application/xml",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = doc.ToString()
                };
            }), format);
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/nomenclatures/")]
        public ContentResult GetNomenclatures(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10,
            FormatType format = FormatType.Xml)
        {
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                string response = session.CreateSQLQuery(SqlQueries.GetNomenclatures)
                    .SetParameter("StartDate", startDate)
                    .SetParameter("EndDate", endDate)
                    .SetParameter("Offset", offset)
                    .SetParameter("RowCount", rowCount)
                    .UniqueResult<string>();
                transaction.Commit();
                XDocument xml = XDocument.Parse(response ?? "<Nomenclature />", LoadOptions.None);
                XDocument doc = new(new XElement("response", xml.Root));
                return new ContentResult
                {
                    ContentType = "application/xml",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = doc.ToString()
                };
            }), format);
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/nomenclaturescosts/")]
        public ContentResult GetNomenclaturesCosts(int offset = 0, int rowCount = 10,
            FormatType format = FormatType.Xml)
        {
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                string response = session.CreateSQLQuery(SqlQueries.GetNomenclaturesCosts)
                    .SetParameter("offset", offset)
                    .SetParameter("rowcount", rowCount)
                    .UniqueResult<string>();
                transaction.Commit();
                XDocument xml = XDocument.Parse(response ?? "<Nomenclature />", LoadOptions.None);
                XDocument doc = new(new XElement("response", xml.Root));

                return new ContentResult
                {
                    ContentType = "application/xml",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = doc.ToString()
                };
            }), format);
        }

        #endregion
    }
}
