using WsWebApiCore.Common;

namespace WebApiTerra1000.Controllers;

public sealed class DeliveryPlaceControllerV2 : WsServiceControllerBase
{
    #region Constructor and destructor

    public DeliveryPlaceControllerV2(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetDeliveryPlacesV2)]
    public ContentResult GetDeliveryPlaces([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int offset = 0,
        [FromQuery] int rowCount = 100, [FromQuery(Name = "format")] string format = "") =>
        GetDeliveryPlacesWork(WsWebSqlQueriesV2.GetDeliveryPlaces, startDate, endDate, offset, rowCount, format);

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetDeliveryPlacesV2Preview)]
    public ContentResult GetDeliveryPlacesPreview([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int offset = 0, 
        [FromQuery] int rowCount = 100, [FromQuery(Name = "format")] string format = "") =>
        GetDeliveryPlacesWork(WsWebSqlQueriesV2.GetDeliveryPlacesPreview, startDate, endDate, offset, rowCount, format);

    private ContentResult GetDeliveryPlacesWork([FromQuery] string url, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
        [FromQuery] int offset = 0, [FromQuery] int rowCount = 100, [FromQuery(Name = "format")] string format = "")
    {
        return WsServiceUtilsGetXmlContent.GetContentResult(() =>
        {
            string response = WsServiceUtilsSql.GetResponse<string>(SessionFactory, url,
                WsServiceUtilsSql.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.DeliveryPlaces} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    #endregion
}
