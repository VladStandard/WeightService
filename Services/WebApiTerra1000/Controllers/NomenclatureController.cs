// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
        return GetContentResult(() =>
        {
            string response = string.IsNullOrEmpty(code)
                ? WsServiceSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetNomenclatureFromId, new SqlParameter("id", id))
                : WsServiceSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetNomenclatureFromCode, new SqlParameter("code", code));
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
        return GetContentResult(() =>
        {
            string response = WsServiceSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetNomenclatures,
                WsServiceSqlUtils.GetParameters(startDate, endDate, offset, rowCount));
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
        return GetContentResult(() =>
        {
            string response = WsServiceSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetNomenclaturesCosts,
                WsServiceSqlUtils.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    #endregion
}
