// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
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
public class BarCodeController : BaseController //ApiController
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
    /// <param name="format"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v3/get_barcode/top/")]
    public ContentResult GetBarcodeTop(string barcode, bool useCrc = false, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return ControllerHelp.RunTask(new(() =>
            new BarcodeTopModel(barcode, useCrc).GetResult<BarcodeTopModel>(format, HttpStatusCode.OK)), format);
    }

    /// <summary>
    /// Get down barcode.
    /// </summary>
    /// <param name="barcode"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v3/get_barcode/bottom/")]
    public ContentResult GetBarcodeBottom(string barcode, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return ControllerHelp.RunTask(new(() =>
            new BarcodeBottomModel(barcode).GetResult<BarcodeBottomModel>(format, HttpStatusCode.OK)), format);
    }

    /// <summary>
    /// Get right barcode.
    /// </summary>
    /// <param name="barcode"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v3/get_barcode/right/")]
    public ContentResult GetBarcodeRight(string barcode, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return ControllerHelp.RunTask(new(() =>
            new BarcodeRightModel(barcode).GetResult<BarcodeRightModel>(format, HttpStatusCode.OK)), format);
    }

    [AllowAnonymous]
    [HttpPost()]
    [Route("api/v3/send_test/")]
    public ContentResult SendTest(FormatTypeEnum format = FormatTypeEnum.Xml, bool showQuery = false) =>
        ControllerHelp.RunTask(new(() => ControllerHelp.NewResponse1CFromQuery(SessionFactory, string.Empty,
            null, format, showQuery, false)), format);

    [AllowAnonymous]
    [HttpPost()]
    [Route("api/v3/send_query")]
    public ContentResult SendSqlquery([FromBody] string query, FormatTypeEnum format = FormatTypeEnum.Xml, bool showQuery = false) =>
        ControllerHelp.RunTask(new(() => ControllerHelp.NewResponse1CFromQuery(SessionFactory, query,
            null, format, showQuery, false)), format);

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost()]
    [Route("api/v3/send_barcode/bottom/")]
    public ContentResult SendBarcodeBottom([FromBody] BarcodeBottomModel barcodeBottom, FormatTypeEnum format = FormatTypeEnum.Xml,
        bool showQuery = false) =>
        ControllerHelp.RunTask(new(() => ControllerHelp.NewResponse1CFromQuery(
            SessionFactory, SqlQueriesBarcodes.FindBottom,
            new("VALUE_BOTTOM", barcodeBottom.GetValue()), format, showQuery, false)), format);

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost()]
    [Route("api/v3/send_barcode/right/")]
    public ContentResult SendBarcodeRight([FromBody] BarcodeRightModel barcodeRight, FormatTypeEnum format = FormatTypeEnum.Xml,
        bool showQuery = false) =>
        ControllerHelp.RunTask(new(() => ControllerHelp.NewResponse1CFromQuery(
            SessionFactory, SqlQueriesBarcodes.FindRight,
            new("VALUE_RIGHT", barcodeRight.GetValue()), format, showQuery, false)), format);

    [AllowAnonymous]
    [HttpPost()]
    [Route("api/v3/send_barcode/top/")]
    public ContentResult SendBarcodeTop([FromBody] BarcodeTopModel barcodeTop, FormatTypeEnum format = FormatTypeEnum.Xml, bool showQuery = false) =>
        ControllerHelp.RunTask(new(() => ControllerHelp.NewResponse1CFromQuery(
            SessionFactory, SqlQueriesBarcodes.FindTop,
            new("VALUE_TOP", barcodeTop.GetValue()), format, showQuery, false)), format);

    [AllowAnonymous]
    [HttpPost()]
    [Route("api/v3/send_barcode/top_v2/")]
    public ContentResult SendBarcodeTopV2([FromBody] string barcodeTop, FormatTypeEnum format = FormatTypeEnum.Xml, bool showQuery = false)
    {
        //Encoding = Encoding.Unicode;
        BarcodeTopModel barcodeTop1 = new BarcodeTopModel().DeserializeFromXml<BarcodeTopModel>(barcodeTop);
        //barcodeTop.GetResult<BarcodeTopModel>(format, HttpStatusCode.OK);
        //XDocument xdoc = XDocument.Parse(barcodeTop.ToString());
        ////HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
        //StringContent content = new StringContent(xdoc.ToString(), Encoding.Unicode, "application/xml");
        //string xml = content.ToString() ?? string.Empty;
        //BarcodeTopModel barcodeTop1 = new BarcodeTopModel().DeserializeFromXml<BarcodeTopModel>(xml);
        return ControllerHelp.RunTask(new(() => ControllerHelp.NewResponse1CFromQuery(
            SessionFactory, SqlQueriesBarcodes.FindTop,
            new("VALUE_TOP", barcodeTop1.GetValue()), format, showQuery, false)), format);
    }

    #endregion
}
