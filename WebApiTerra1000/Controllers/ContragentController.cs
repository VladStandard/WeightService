// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using NHibernate;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApiTerra1000.Common;
using WebApiTerra1000.Utils;
using static DataCore.ShareEnums;

namespace WebApiTerra1000.Controllers
{
    public class ContragentController : BaseController
    {
        #region Constructor and destructor

        public ContragentController(ILogger<ContragentController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
            //
        }

        #endregion

        #region Public and private methods

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/contragent/")]
        public ContentResult GetContragent(int id, FormatType format = FormatType.Xml) =>
            GetContragentWork(SqlQueries.GetContragent, id, format);

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/contragent_preview/")]
        public ContentResult GetContragentPreview(int id, FormatType format = FormatType.Xml) =>
            GetContragentWork(SqlQueries.GetContragentPreview, id, format);

        private ContentResult GetContragentWork(string url, int id, FormatType format = FormatType.Xml)
        {
            return Controller.RunTask(new Task<ContentResult>(() =>
            {
                string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, url, new SqlParameter("ID", id));
                XDocument xml = XDocument.Parse(response ?? $"<{TerraConsts.Contragents} />", LoadOptions.None);
                XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
                return BaseSerializeEntity<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
            }), format);
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/contragents/")]
        public ContentResult GetContragents(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10,
            FormatType format = FormatType.Xml) =>
            GetContragentsWork(SqlQueries.GetContragents, startDate, endDate, offset, rowCount, format);

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/contragents_preview/")]
        public ContentResult GetContragentsPreview(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10,
            FormatType format = FormatType.Xml) =>
            GetContragentsWork(SqlQueries.GetContragentsPreview, startDate, endDate, offset, rowCount, format);

        private ContentResult GetContragentsWork(string url, DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10,
            FormatType format = FormatType.Xml)
        {
            return Controller.RunTask(new Task<ContentResult>(() =>
            {
                string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, url,
                    TerraUtils.Sql.GetParameters(startDate, endDate, offset, rowCount));
                XDocument xml = XDocument.Parse(response ?? $"<{TerraConsts.Contragents} />", LoadOptions.None);
                XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
                return BaseSerializeEntity<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
            }), format);
        }

        #endregion
    }
}
