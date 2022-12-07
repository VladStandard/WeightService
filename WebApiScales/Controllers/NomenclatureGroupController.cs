// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System.Xml.Linq;
using WebApiCore.Controllers;

namespace WebApiScales.Controllers;

/// <summary>
/// Nomenclature Group controller.
/// </summary>
public class NomenclatureGroupController : WebControllerBase //ApiController
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public NomenclatureGroupController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost()]
    [Route("api/send_nomenclatures_groups/")]
    [Route("api/v1/send_nomenclatures_groups/")]
    [Route("api/v2/send_nomenclatures_groups/")]
    [Route("api/v3/send_nomenclatures_groups/")]
    public ContentResult SendNomenclaturesGroupsList([FromBody] XElement request, 
        [FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() => ControllerHelp
            .NewResponse1CNomenclaturesGroupsFromAction(SessionFactory, request, formatString), formatString);

    #endregion
}
