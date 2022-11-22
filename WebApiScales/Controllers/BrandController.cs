// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using WebApiCore.Controllers;
using WebApiCore.Models;
using WebApiCore.Utils;

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

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost()]
    [Route("api/send_brands/")]
    [Route("api/v3/send_brands/")]
    public ContentResult SendBrands([FromBody] BrandModel brand, FormatTypeEnum format = FormatTypeEnum.Xml,
        bool showQuery = false) =>
        ControllerHelp.RunTask(new(() => ControllerHelp.NewResponse1CFromAction(
            SessionFactory, SqlQueriesBarcodes.FindBottom,
            new("VALUE_BOTTOM", barcodeBottom.GetValue()), format, showQuery, false)), format);

    #endregion
}
