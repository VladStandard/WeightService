using WsWebApiCore.Common;

namespace WebApiTerra1000.Controllers;

public sealed class SummaryController : WsServiceControllerBase
{
    #region Constructor and destructor

    public SummaryController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetSummary)]
    public ContentResult GetSummary([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, 
        [FromQuery(Name = "format")] string format = "")
    {
        return WsServiceUtilsGetXmlContent.GetContentResult(() =>
        {
            string response = WsServiceUtilsSql.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetSummary,
                WsServiceUtilsSql.GetParameters(startDate, endDate));
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.Summary} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    #endregion
}
