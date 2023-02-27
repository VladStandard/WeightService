// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalization.Models;
using WsStorage.Enums;
using WsWebApi.Controllers;

namespace WsWebApiScales.Controllers;

/// <summary>
/// Nomenclature Group controller.
/// </summary>
public class PluGroupController : WebControllerBase
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public PluGroupController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route("api/send_nomenclatures_groups/")]
    public ContentResult SendPluGroups([FromBody] XElement xml, [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "")
    {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = GetAcceptVersion(version) switch
        {
            AcceptVersion.V2 =>
                ControllerHelp.GetContentResult(() => ControllerHelp
                        .NewResponse1cIsNotFound(version, format),
                    format),
            _ => ControllerHelp.GetContentResult(() => ControllerHelp
                .NewResponse1cPluGroups(xml, format), format)
        };
        ControllerHelp.LogWebServiceFk(nameof(WsWebApiScales), LocaleCore.WebService.UrlSendNomenclaturesGroups,
            requestStampDt, xml, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    #endregion
}