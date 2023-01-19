// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.Enums;
using WsWebApi.Controllers;

namespace WebApiScales.Controllers;

/// <summary>
/// Nomenclature Group controller.
/// </summary>
public class NomenclatureController : WebControllerBase
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public NomenclatureController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route("api/send_nomenclatures/")]
    public ContentResult SendNomenclatures([FromBody] XElement request, 
        [FromQuery(Name = "format")] string formatString = "", 
	    [FromHeader(Name = "accept")] string version = "") =>
	    GetAcceptVersion(version) switch
	    {
		    AcceptVersion.V2 =>
				ControllerHelp.GetContentResult(() => ControllerHelp
					.NewResponse1cIsNotFound(SessionFactory, version, formatString), formatString),
		    _ => ControllerHelp.GetContentResult(() => ControllerHelp
					.NewResponse1cNomenclaturesDeprecated(SessionFactory, request, formatString), formatString)
		};

    #endregion
}
