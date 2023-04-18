// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiScales.Controllers;

/// <summary>
/// Nomenclatures controller.
/// </summary>
[Tags(WsWebServiceConsts.Ref1cNomenclatures)]
public sealed class PlusController : WsControllerBase
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public PlusController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
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
            WsAcceptVersion.V2 => GetContentResult(() => NewResponse1cIsNotFound(
                $"Version {version} {LocaleCore.WebService.IsNotFound}!", format, isDebug, SessionFactory), format),
            _ => GetContentResult(() => WsPlus.NewResponse1cPlus(xml, format, isDebug, SessionFactory), format)
        };
        LogWebServiceFk(nameof(WsWebApiScales), WsWebServiceUrls.SendNomenclatures,
            requestStampDt, xml, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    #endregion
}