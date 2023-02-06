// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.Enums;
using WsWebApi.Controllers;

namespace WebApiScales.Controllers;

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
        DateTime dtStamp = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WebApiScales), "api/send_nomenclatures_groups/", dtStamp, xml, format, host, version).ConfigureAwait(false);
        ContentResult result = GetAcceptVersion(version) switch
        {
            AcceptVersion.V2 =>
                ControllerHelp.GetContentResult(() => ControllerHelp
                        .NewResponse1cIsNotFound(SessionFactory, version, format),
                    format),
            _ => ControllerHelp.GetContentResult(() => ControllerHelp
                .NewResponse1cPluGroups(SessionFactory, xml, format), format)
        };
        ControllerHelp.LogResponse(nameof(WebApiScales), "api/send_nomenclatures_groups/", dtStamp, result, format, host, version).ConfigureAwait(false);
        return result;
    }

    #endregion
}