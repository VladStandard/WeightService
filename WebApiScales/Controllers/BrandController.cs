// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System.Xml.Linq;
using WebApiCore.Controllers;
using WebApiCore.Models;

namespace WebApiScales.Controllers;

/// <summary>
/// Brand controller.
/// </summary>
public class BrandController : BaseController //ApiController
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

    //[AllowAnonymous]
    //[Produces("application/xml")]
    //[HttpPost()]
    //[Route("api/send_brand/")]
    //[Route("api/v1/send_brand/")]
    //[Route("api/v2/send_brand/")]
    //[Route("api/v3/send_brand/")]
    //public ContentResult SendBrand([FromBody] BrandModel brand, FormatTypeEnum format = FormatTypeEnum.Xml,
    //    bool showQuery = false) =>
    //    ControllerHelp.RunTask(new(() => ControllerHelp.NewResponse1CFromAction(
    //        SessionFactory, ControllerHelp.GetBrandList(brand), format, showQuery, false)), format);

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost()]
    [Route("api/send_brands/")]
    [Route("api/v1/send_brands/")]
    [Route("api/v2/send_brands/")]
    [Route("api/v3/send_brands/")]
    public ContentResult SendBrandList([FromBody] XElement request, FormatTypeEnum format = FormatTypeEnum.Xml,
        bool showQuery = false) =>
        ControllerHelp.RunTask(new(() => ControllerHelp.NewResponse1CFromAction(
            SessionFactory, request, format, showQuery, false)), format);

    #endregion
}
