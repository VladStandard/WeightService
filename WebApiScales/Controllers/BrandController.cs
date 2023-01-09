// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
    public ContentResult SendBrandList([FromBody] XElement request, 
        [FromQuery(Name = "format")] string formatString = "",
        [FromHeader(Name = "accept")] string version = "") =>
        GetAcceptVersion(version) switch
        {
            AcceptVersion.V2 =>
                ControllerHelp.GetContentResult(() => ControllerHelp
                .NewResponse1cIsNotFound(SessionFactory, version, formatString), formatString),
            _ => ControllerHelp.GetContentResult(() => ControllerHelp
                .NewResponse1cBrandsFromAction(SessionFactory, request, formatString), formatString)
        };

    #endregion
}
