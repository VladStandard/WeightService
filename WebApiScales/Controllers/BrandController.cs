// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.Enums;
using WsWebApi.Controllers;
using static WsWebApi.Models.WebUtils;

namespace WebApiScales.Controllers;

/// <summary>
/// Brand controller.
/// </summary>
public class BrandController : WebControllerBase
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
    [Route("api/send_brands/")]
    public ContentResult SendBrandList([FromBody] XElement xml, 
        [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "accept")] string version = "")
    {
        ControllerHelp.LogRequestXml(nameof(WebApiScales), xml, format, version).ConfigureAwait(false);
        return GetAcceptVersion(version) switch
        {
            AcceptVersion.V2 =>
                ControllerHelp.GetContentResult(() => ControllerHelp
                    .NewResponse1cIsNotFound(SessionFactory, version, format), format),
            _ => ControllerHelp.GetContentResult(() => ControllerHelp
                .NewResponse1cBrands(SessionFactory, xml, format), format)
        };
    }

    #endregion
}