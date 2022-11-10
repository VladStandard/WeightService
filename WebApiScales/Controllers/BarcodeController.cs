// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net;
using Azure.Core;
using DataCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Cfg.XmlHbmBinding;
using WebApiCore.Common;
using WebApiCore.Controllers;
using WebApiCore.Utils;

namespace WebApiScales.Controllers;

/// <summary>
/// Barcode controller.
/// </summary>
public class BarCodeController : BaseController
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
    [Route("api/v3/barcode/top/")]
    public ContentResult GetBarcodeTop(string barcode, bool useCrc = false, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return ControllerHelp.RunTask(new(() => 
            new BarcodeTopModel(barcode, useCrc).GetResult(format, HttpStatusCode.OK)), format);
    }

    /// <summary>
    /// Get down barcode.
    /// </summary>
    /// <param name="barcode"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v3/barcode/bottom/")]
    public ContentResult GetBarcodeBottom(string barcode, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return ControllerHelp.RunTask(new(() => 
            new BarcodeBottomModel(barcode).GetResult(format, HttpStatusCode.OK)), format);
    }

    /// <summary>
    /// Get right barcode.
    /// </summary>
    /// <param name="barcode"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v3/barcode/right/")]
    public ContentResult GetBarcodeRight(string barcode, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return ControllerHelp.RunTask(new(() => 
            new BarcodeRightModel(barcode).GetResult(format, HttpStatusCode.OK)), format);
    }

    [AllowAnonymous]
    [HttpPost()]
    [Route("api/v3/send_test/")]
    public ContentResult SendTest(FormatTypeEnum format = FormatTypeEnum.Xml) =>
        ControllerHelp.RunTask(new(() => ControllerHelp.GetResponse1C(SessionFactory, string.Empty, 
            null, format, false)), format);

    [AllowAnonymous]
    [HttpPost()]
    [Route("api/v3/send_query")]
    public ContentResult SendSqlquery([FromBody] string query, FormatTypeEnum format = FormatTypeEnum.Xml) =>
        ControllerHelp.RunTask(new(() => ControllerHelp.GetResponse1C(SessionFactory, query, 
            null, format, false)), format);

    [AllowAnonymous]
    [HttpPost()]
    [Route("api/v3/send_barcode/bottom/")]
    public ContentResult SendBarcodeBottom([FromBody] BarcodeBottomModel barcodeBottom, FormatTypeEnum format = FormatTypeEnum.Xml) =>
        ControllerHelp.RunTask(new(() => ControllerHelp.GetResponse1C(
            SessionFactory, SqlQueriesBarcodes.FindBottom,
            new("VALUE_BOTTOM", barcodeBottom.GetValue()), format, true)), format);

    [AllowAnonymous]
    [HttpPost()]
    [Route("api/v3/send_barcode/right/")]
    public ContentResult SendBarcodeRight([FromBody] BarcodeRightModel barcodeRight, FormatTypeEnum format = FormatTypeEnum.Xml) =>
        ControllerHelp.RunTask(new(() => ControllerHelp.GetResponse1C(
            SessionFactory, SqlQueriesBarcodes.FindRight,
            new("VALUE_RIGHT", barcodeRight.GetValue()), format, true)), format);

    [AllowAnonymous]
    [HttpPost()]
    [Route("api/v3/send_barcode/top/")]
    public ContentResult SendBarcodeTop([FromBody] BarcodeTopModel barcodeTop, FormatTypeEnum format = FormatTypeEnum.Xml) =>
        ControllerHelp.RunTask(new(() => ControllerHelp.GetResponse1C(
            SessionFactory, SqlQueriesBarcodes.FindTop,
            new("VALUE_TOP", barcodeTop.GetValue()), format, true)), format);

    #endregion
}
