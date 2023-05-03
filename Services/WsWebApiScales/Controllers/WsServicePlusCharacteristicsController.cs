// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiScales.Controllers;

/// <summary>
/// Nomenclatures characteristics controller.
/// </summary>
[Tags(WsWebServiceConsts.Ref1CNomenclaturesCharacteristics)]
public sealed class WsServicePlusCharacteristicsController : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    private WsServiceControllerHelper ControllerHelper { get; }

    public WsServicePlusCharacteristicsController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        ControllerHelper = new(sessionFactory);
    }

    #endregion

    #region Public and public methods

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route(WsWebServiceUrls.SendNomenclaturesCharacteristics)]
    public ContentResult SendPluCharacteristics([FromBody] XElement xml, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "debug")] bool isDebug = false,
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "")
    {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = GetAcceptVersion(version) switch
        {
            WsAcceptVersion.V2 => GetContentResult(() => // Новый ответ 1С - не найдено.
                NewResponse1CIsNotFound($"Version {version} {LocaleCore.WebService.IsNotFound}!", format, isDebug, SessionFactory), format),
            _ => GetContentResult(() => ControllerHelper.PlusCharacteristicsController.NewResponse1cPluCharacteristics(xml, format, isDebug, SessionFactory), format)
        };
        LogWebServiceFk(nameof(WsWebApiScales), WsWebServiceUrls.SendNomenclaturesCharacteristics,
            requestStampDt, xml, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    #endregion
}