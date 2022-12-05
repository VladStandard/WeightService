// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System.Net;
using WebApiCore.Controllers;
using WebApiCore.Models;
using WebApiCore.Utils;

namespace WebApiScales.Controllers;

/// <summary>
/// Barcode controller.
/// </summary>
public class BarCodeController : WebControllerBase //ApiController
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public BarCodeController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Get top barcode.
    /// </summary>
    /// <param name="barcode"></param>
    /// <param name="useCrc"></param>
    /// <param name="formatString"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/get_barcode/top/")]
    [Route("api/v3/get_barcode/top/")]
    public ContentResult GetBarcodeTop([FromQuery] string barcode, bool useCrc = false,
        [FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() => 
            new BarcodeTopModel(barcode, useCrc).GetContentResult<BarcodeTopModel>(formatString, HttpStatusCode.OK), formatString);

    /// <summary>
    /// Get down barcode.
    /// </summary>
    /// <param name="barcode"></param>
    /// <param name="formatString"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/get_barcode/bottom/")]
    [Route("api/v3/get_barcode/bottom/")]
    public ContentResult GetBarcodeBottom([FromQuery] string barcode, [FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() => 
            new BarcodeBottomModel(barcode).GetContentResult<BarcodeBottomModel>(formatString, HttpStatusCode.OK), formatString);

    /// <summary>
    /// Get right barcode.
    /// </summary>
    /// <param name="barcode"></param>
    /// <param name="formatString"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/get_barcode/right/")]
    [Route("api/v3/get_barcode/right/")]
    public ContentResult GetBarcodeRight([FromQuery] string barcode, [FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() => 
            new BarcodeRightModel(barcode).GetContentResult<BarcodeRightModel>(formatString, HttpStatusCode.OK), formatString);

    /// <summary>
    /// Get barcode.
    /// </summary>
    /// <param name="dtEnd"></param>
    /// <param name="formatString"></param>
    /// <param name="dtStart"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/get_barcodes/")]
    [Route("api/v3/get_barcodes/")]
    public ContentResult GetResponseBarCodes([FromQuery(Name = "StartDate")] DateTime dtStart, [FromQuery(Name = "EndDate")] DateTime dtEnd, 
        [FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponseBarCodes(SessionFactory, dtStart, dtEnd, formatString), formatString);

    [AllowAnonymous]
    [HttpPost()]
    [Route("api/send_test/")]
    [Route("api/v3/send_test/")]
    public ContentResult SendTest([FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() => 
            ControllerHelp.NewResponse1CFromQuery(SessionFactory, string.Empty, null, formatString, false), formatString);

    [AllowAnonymous]
    [HttpPost()]
    [Route("api/send_query")]
    [Route("api/v3/send_query")]
    public ContentResult SendSqlquery([FromBody] string query, [FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() => 
            ControllerHelp.NewResponse1CFromQuery(SessionFactory, query, null, formatString, false), formatString);

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost()]
    [Route("api/send_barcode/bottom/")]
    [Route("api/v3/send_barcode/bottom/")]
    public ContentResult SendBarcodeBottom([FromBody] BarcodeBottomModel barcodeBottom,
        [FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() => 
            ControllerHelp.NewResponse1CFromQuery(SessionFactory,
            SqlQueriesBarcodes.FindBottom, new("VALUE_BOTTOM", barcodeBottom.GetValue()), formatString, false), formatString);

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost()]
    [Route("api/send_barcode/right/")]
    [Route("api/v3/send_barcode/right/")]
    public ContentResult SendBarcodeRight([FromBody] BarcodeRightModel barcodeRight,
        [FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() => 
            ControllerHelp.NewResponse1CFromQuery(SessionFactory, SqlQueriesBarcodes.FindRight,
            new("VALUE_RIGHT", barcodeRight.GetValue()), formatString, false), formatString);

    [AllowAnonymous]
    [HttpPost()]
    [Route("api/send_barcode/top/")]
    [Route("api/v3/send_barcode/top/")]
    public ContentResult SendBarcodeTop([FromBody] BarcodeTopModel barcodeTop,
        [FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() => 
            ControllerHelp.NewResponse1CFromQuery(SessionFactory, SqlQueriesBarcodes.FindTop,
            new("VALUE_TOP", barcodeTop.GetValue()), formatString, false), formatString);

    [AllowAnonymous]
    [HttpPost()]
    [Route("api/send_barcode/top_v2/")]
    [Route("api/v3/send_barcode/top_v2/")]
    public ContentResult SendBarcodeTopV2([FromBody] string barcodeTop,
        [FromQuery(Name = "format")] string formatString = "") =>
        ControllerHelp.GetContentResult(() => 
            ControllerHelp.NewResponse1CFromQuery(SessionFactory, SqlQueriesBarcodes.FindTop,
            new("VALUE_TOP", new BarcodeTopModel().DeserializeFromXml<BarcodeTopModel>(barcodeTop).GetValue()), formatString, false), formatString);

    #endregion
}
