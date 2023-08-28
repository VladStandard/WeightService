using WsWebApiCore.Common;

namespace WebApiTerra1000.Controllers;

public sealed class NomenclatureController : WsServiceControllerBase
{
    #region Constructor and destructor

    public NomenclatureController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetNomenclature)]
    public ContentResult GetNomenclature([FromQuery] string code, [FromQuery] long id, [FromQuery(Name = "format")] string format = "")
    {
        return WsServiceUtilsGetXmlContent.GetContentResult(() =>
        {
            string response = string.IsNullOrEmpty(code)
                ? WsServiceUtilsSql.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetNomenclatureFromId, new SqlParameter("id", id))
                : WsServiceUtilsSql.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetNomenclatureFromCode, new SqlParameter("code", code));
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetNomenclatures)]
    public ContentResult GetNomenclatures([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, 
        [FromQuery] int offset = 0, [FromQuery] int rowCount = 10, [FromQuery(Name = "format")] string format = "")
    {
        return WsServiceUtilsGetXmlContent.GetContentResult(() =>
        {
            string response = WsServiceUtilsSql.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetNomenclatures,
                WsServiceUtilsSql.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetNomenclaturesCosts)]
    public ContentResult GetNomenclaturesCosts([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, 
        [FromQuery] int offset = 0, [FromQuery] int rowCount = 10, [FromQuery(Name = "format")] string format = "")
    {
        return WsServiceUtilsGetXmlContent.GetContentResult(() =>
        {
            string response = WsServiceUtilsSql.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetNomenclaturesCosts,
                WsServiceUtilsSql.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    #endregion
}
