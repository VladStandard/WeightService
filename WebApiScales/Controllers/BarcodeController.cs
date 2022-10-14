// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net;
using DataCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using WebApiCore.Common;
using WebApiCore.Controllers;

namespace WebApiScales.Controllers;

/// <summary>
/// Barcode controller v1.
/// </summary>
public class BarcodeController : BaseController
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public BarcodeController(ISessionFactory sessionFactory) : base(sessionFactory)
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
        {
            //string response1 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueriesV2.GetXmlSimpleV1);
            //return SqlSimpleV1Entity.DeserializeFromXml(response1).GetResult(format, HttpStatusCode.OK);
            return new BarcodeTopEntity(barcode, useCrc).GetResult(format, HttpStatusCode.OK);
        }), format);
    }

    /// <summary>
    /// Get down barcode.
    /// </summary>
    /// <param name="barcode"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v3/barcode/down/")]
    public ContentResult GetBarcodeDown(string barcode, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return ControllerHelp.RunTask(new(() =>
        {
            return new BarcodeDownEntity(barcode).GetResult(format, HttpStatusCode.OK);
        }), format);
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
        {
            return new BarcodeRightEntity(barcode).GetResult(format, HttpStatusCode.OK);
        }), format);
    }

    #endregion
}
