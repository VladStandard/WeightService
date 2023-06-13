// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsWebApiCore.Common;

namespace WebApiTerra1000.Controllers;

public sealed class SummaryControllerV2 : WsServiceControllerBase
{
    #region Constructor and destructor

    public SummaryControllerV2(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetSummaryV2)]
    public ContentResult GetSummary([FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
        [FromQuery(Name = "format")] string format = "") =>
        GetSummaryCore(WsWebSqlQueriesV2.GetSummary, startDate, endDate, format);

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetSummaryV2Preview)]
    public ContentResult GetSummaryPreview([FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
        [FromQuery(Name = "format")] string format = "") =>
        GetSummaryCore(WsWebSqlQueriesV2.GetSummaryPreview, startDate, endDate, format);

    private ContentResult GetSummaryCore(string url, DateTime startDate, DateTime endDate, string format) => 
        GetContentResult(() =>
        {
            string response = WsServiceSqlUtils.GetResponse<string>(SessionFactory, url,
                WsServiceSqlUtils.GetParameters(startDate, endDate));
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.Summary} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);

    #endregion
}
