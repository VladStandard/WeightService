// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebApiTerra1000.Controllers;

public sealed class DeliveryPlaceController : WsServiceControllerBase
{
    #region Constructor and destructor

    public DeliveryPlaceController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route(WsWebServiceUrls.GetDeliveryPlaces)]
    public ContentResult GetDeliveryPlaces([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, 
        [FromQuery] int offset = 0, [FromQuery] int rowCount = 100, [FromQuery(Name = "format")] string format = "")
    {
        return GetContentResult(() =>
        {
            string response = WsServiceSqlUtils.GetResponse<string>(SessionFactory, WsWebSqlQueries.GetDeliveryPlaces,
                WsServiceSqlUtils.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.DeliveryPlaces} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    #endregion
}
