// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Xml.Linq;
using WebApiCore.Controllers;
using WebApiCore.Enums;

namespace WebApiScales.Controllers;

/// <summary>
/// Nomenclature Group controller.
/// </summary>
public class NomenclatureController : WebControllerBase //ApiController
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
        [FromQuery(Name = "format")] string formatString = "", [FromHeader] AcceptVersion acceptVersion = AcceptVersion.V1) =>
	    acceptVersion switch
	    {
		    AcceptVersion.V2 => ControllerHelp.GetContentResult(
			    () => ControllerHelp.NewResponse1CNomenclaturesDeprecated(SessionFactory, request, formatString),
			    formatString),
		    _ => ControllerHelp.GetContentResult(() => ControllerHelp.NewResponse1C(SessionFactory, formatString),
			    formatString)
	    };

    #endregion
}
