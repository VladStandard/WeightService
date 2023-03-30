// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalization.Utils;
using WsStorage.Enums;
using WsWebApi.Helpers;

namespace WsWebApiScales.Controllers;

/// <summary>
/// Brand controller.
/// </summary>
public sealed class BrandController : WsWebControllerBase
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public BrandController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route(UrlWebService.SendBrands)]
    public ContentResult SendBrands([FromBody] XElement xml, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "is_debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "")
    {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = GetAcceptVersion(version) switch
        {
            AcceptVersion.V2 =>
                ControllerHelp.GetContentResult(() => ControllerHelp.NewResponse1cIsNotFound(version, format, isDebug, SessionFactory), format),
            _ => ControllerHelp.GetContentResult(() => ControllerHelp.NewResponse1cBrands(xml, format, isDebug, SessionFactory), format)
        };
        ControllerHelp.LogWebServiceFk(nameof(WsWebApiScales), UrlWebService.SendBrands,
            requestStampDt, xml, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    #endregion
}