// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiScales.Controllers;

/// <summary>
/// Nomenclatures controller.
/// </summary>
[Tags(WsWebServiceConsts.Ref1CNomenclatures)]
public sealed class WsServicePlusController : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    private WsServiceControllerHelper ControllerHelper { get; }

    public WsServicePlusController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        ControllerHelper = new(sessionFactory);
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route(WsWebServiceUrls.SendNomenclatures)]
    public ContentResult SendPlus([FromBody] XElement xml, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "")
    {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = GetAcceptVersion(version) switch
        {
            WsAcceptVersion.V2 => GetContentResult(() => // Новый ответ 1С - не найдено.
                NewResponse1CIsNotFound($"Version {version} {LocaleCore.WebService.IsNotFound}!", format, isDebug, SessionFactory), format),
            _ => GetContentResult(() => ControllerHelper.PlusController.NewResponsePlus(xml, format, isDebug, SessionFactory), format)
        };
        LogWebServiceFk(nameof(WsWebApiScales), WsWebServiceUrls.SendNomenclatures,
            requestStampDt, xml, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    #endregion
}