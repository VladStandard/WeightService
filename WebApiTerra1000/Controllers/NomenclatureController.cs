// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using NHibernate;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApiCore.Controllers;
using WebApiCore.Utils;
using static DataCore.ShareEnums;

namespace WebApiTerra1000.Controllers;

public class NomenclatureController : BaseController
{
    #region Constructor and destructor

    public NomenclatureController(ILogger<NomenclatureController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/nomenclature/")]
    public ContentResult GetNomenclature(string code, long id, FormatType format = FormatType.Xml)
    {
        return ControllerHelp.RunTask(new Task<ContentResult>(() =>
        {
            string response = string.IsNullOrEmpty(code)
                ? TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetNomenclatureFromId, new SqlParameter("id", id))
                : TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetNomenclatureFromCode, new SqlParameter("code", code));
            XDocument xml = XDocument.Parse(response ?? $"<{TerraConsts.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
            return BaseSerializeDeprecatedEntity<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
        }), format);
    }

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/nomenclatures/")]
    public ContentResult GetNomenclatures(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10,
        FormatType format = FormatType.Xml)
    {
        return ControllerHelp.RunTask(new Task<ContentResult>(() =>
        {
            string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetNomenclatures,
                TerraUtils.Sql.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{TerraConsts.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
            return BaseSerializeDeprecatedEntity<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
        }), format);
    }

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/nomenclaturescosts/")]
    public ContentResult GetNomenclaturesCosts(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10, FormatType format = FormatType.Xml)
    {
        return ControllerHelp.RunTask(new Task<ContentResult>(() =>
        {
            string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetNomenclaturesCosts,
                TerraUtils.Sql.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{TerraConsts.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
            return BaseSerializeDeprecatedEntity<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
        }), format);
    }

    #endregion
}
