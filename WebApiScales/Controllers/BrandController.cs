// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System.Xml.Linq;
using WebApiCore.Controllers;

namespace WebApiScales.Controllers;

/// <summary>
/// Brand controller.
/// </summary>
public class BrandController : WebControllerBase //ApiController
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
    [HttpPost()]
    [Route("api/send_brands/")]
    [Route("api/v1/send_brands/")]
    [Route("api/v2/send_brands/")]
    [Route("api/v3/send_brands/")]
    public ContentResult SendBrandList([FromBody] XElement request, 
        [FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() => {
            return ControllerHelp.NewResponse1CFromAction(SessionFactory, request, formatString, false);
        }, formatString);

    #endregion
}
