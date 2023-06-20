// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

namespace WsWebApiScales.Helpers;

/// <summary>
/// Nomenclatures characteristics controller.
/// </summary>
[Tags(WsLocaleWebServiceUtils.Tag1CNomenclaturesCharacteristics)]
public sealed class WsServicePlusCharacteristicsWrapper : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    private WsServicePlusCharacteristicsController PlusCharacteristicsController { get; }

    public WsServicePlusCharacteristicsWrapper(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        PlusCharacteristicsController = new(sessionFactory);
    }

    #endregion

    #region Public and public methods

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route(WsLocaleWebServiceUtils.SendNomenclaturesCharacteristics)]
    public ContentResult SendPluCharacteristics([FromBody] XElement xml, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "debug")] bool isDebug = false,
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "")
    {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = WsServiceGetUtils.GetAcceptVersion(version) switch
        {
            // Новый ответ 1С - не найдено.
            WsSqlEnumAcceptVersion.V2 => WsServiceContentUtils.GetContentResult(() =>
                WsServiceResponseUtils.NewResponse1CIsNotFound($"Version {version} {WsLocaleCore.WebService.IsNotFound}!", format, isDebug, SessionFactory), format),
            _ => WsServiceContentUtils.GetContentResult(() => PlusCharacteristicsController.NewResponsePluCharacteristics(xml, format, isDebug, SessionFactory), format)
            // Находится в разработке, свяжитесь с разработчиком.
            //_ => WsServiceResponseUtils.NewResponse1CIsNotFound($"{WsLocaleCore.WebService.Underdevelopment}!", format, isDebug, SessionFactory)
        };
        WsServiceLogUtils.LogWebServiceFk(nameof(WsWebApiScales), WsLocaleWebServiceUtils.SendNomenclaturesCharacteristics,
            requestStampDt, xml, result.Content ?? string.Empty, format, host, version);
        return result;
    }

    #endregion
}