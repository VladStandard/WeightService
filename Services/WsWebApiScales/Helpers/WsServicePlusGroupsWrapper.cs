using WsStorageCore.Common;

namespace WsWebApiScales.Helpers;

/// <summary>
/// Nomenclatures groups controller.
/// </summary>
[Tags(WsLocaleWebServiceUtils.Tag1CNomenclaturesGroups)]
public sealed class WsServicePlusGroupsWrapper : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    private WsServicePlusGroupsController PlusGroupsController { get; }

    public WsServicePlusGroupsWrapper(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        PlusGroupsController = new(sessionFactory);
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route(WsLocaleWebServiceUtils.SendNomenclaturesGroups)]
    public ContentResult SendPluGroups([FromBody] XElement xml, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "debug")] bool isDebug = false,
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "")
    {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = WsServiceUtilsGet.GetAcceptVersion(version) switch
        {
            // Новый ответ 1С - не найдено.
            WsSqlEnumAcceptVersion.V2 => WsServiceUtilsGetXmlContent.GetContentResult(() =>
                WsServiceUtilsResponse.NewResponse1CIsNotFound($"Version {version} {WsLocaleCore.WebService.IsNotFound}!", format, isDebug, SessionFactory), format),
            _ => WsServiceUtilsGetXmlContent.GetContentResult(() => PlusGroupsController.NewResponsePluGroups(xml, format, isDebug, SessionFactory), format)
            // Находится в разработке, свяжитесь с разработчиком.
            //_ => WsServiceResponseUtils.NewResponse1CIsNotFound($"{WsLocaleCore.WebService.Underdevelopment}!", format, isDebug, SessionFactory)
        };
        WsServiceUtilsLog.LogWebServiceFk(nameof(WsWebApiScales), WsLocaleWebServiceUtils.SendNomenclaturesGroups,
            requestStampDt, xml, result.Content ?? string.Empty, format, host, version);
        return result;
    }

    #endregion
}