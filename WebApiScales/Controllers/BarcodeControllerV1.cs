// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using WebApiCore.Common;
using WebApiCore.Controllers;
using static DataCore.ShareEnums;

namespace WebApiScales.Controllers;

/// <summary>
/// Barcode controller v1.
/// </summary>
public class BarcodeControllerV1 : BaseController
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="sessionFactory"></param>
    public BarcodeControllerV1(ILogger<BarcodeControllerV1> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
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
    [Route("api/v1/barcode/top/")]
    public ContentResult GetBarcodeTop(string barcode, bool useCrc = false, FormatType format = FormatType.Xml)
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
    [Route("api/v1/barcode/down/")]
    public ContentResult GetBarcodeDown(string barcode, FormatType format = FormatType.Xml)
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
    [Route("api/v1/barcode/right/")]
    public ContentResult GetBarcodeRight(string barcode, FormatType format = FormatType.Xml)
    {
        return ControllerHelp.RunTask(new(() =>
        {
            return new BarcodeRightEntity(barcode).GetResult(format, HttpStatusCode.OK);
        }), format);
    }

    #endregion
}
