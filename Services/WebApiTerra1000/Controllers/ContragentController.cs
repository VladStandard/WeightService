// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsWebApiCore.Common;

namespace WebApiTerra1000.Controllers;

public sealed class ContragentController : WsServiceControllerBase
{
    #region Constructor and destructor

    public ContragentController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetContragent)]
    public ContentResult GetContragent([FromQuery] long id, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "is_debug")] bool isDebug = false)
    {
        return GetContentResult(() =>
        {
            string response = WsServiceSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetContragent, new SqlParameter("ID", id));
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.Contragents} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetContragents)]
    public ContentResult GetContragents([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int offset = 0,
        [FromQuery] int rowCount = 10, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "is_debug")] bool isDebug = false)
    {
        return GetContentResult(() =>
        {
            string response = WsServiceSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetContragents,
                WsServiceSqlUtils.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.Contragents} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    #endregion
}
