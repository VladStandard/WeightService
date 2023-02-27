// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Utils;
using WsLocalization.Models;
using WsStorage.Models.Tables.BarCodes;
using WsWebApi.Controllers;
using WsWebApi.Utils;

namespace WsWebApiScales.Controllers;

/// <summary>
/// Barcode controller.
/// </summary>
public class BarCodeController : WebControllerBase
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
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("api/get_barcode/top/")]
    public ContentResult GetBarcodeTop([FromQuery] string barcode, bool useCrc = false,
        [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = ControllerHelp.GetContentResult(() =>
            DataFormatUtils.GetContentResult<BarcodeTopModel>(new BarcodeTopModel(barcode, useCrc), format, HttpStatusCode.OK), format);
        ControllerHelp.LogWebServiceFk(nameof(WsWebApiScales), LocaleCore.WebService.UrlGetBarcodeTop,
            requestStampDt, barcode, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// Get down barcode.
    /// </summary>
    /// <param name="barcode"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("api/get_barcode/bottom/")]
    public ContentResult GetBarcodeBottom([FromQuery] string barcode, [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = ControllerHelp.GetContentResult(() =>
            DataFormatUtils.GetContentResult<BarcodeBottomModel>(new BarcodeBottomModel(barcode), format, HttpStatusCode.OK), format);
        ControllerHelp.LogWebServiceFk(nameof(WsWebApiScales), LocaleCore.WebService.UrlGetBarcodeBottom, 
            requestStampDt, barcode, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// Get right barcode.
    /// </summary>
    /// <param name="barcode"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("api/get_barcode/right/")]
    public ContentResult GetBarcodeRight([FromQuery] string barcode, [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = ControllerHelp.GetContentResult(() =>
            DataFormatUtils.GetContentResult<BarcodeRightModel>(new BarcodeRightModel(barcode), format, HttpStatusCode.OK), format);
        ControllerHelp.LogWebServiceFk(nameof(WsWebApiScales), LocaleCore.WebService.UrlGetBarcodeRight, 
            requestStampDt, barcode, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// Get barcode.
    /// </summary>
    /// <param name="dtEnd"></param>
    /// <param name="format"></param>
    /// <param name="dtStart"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("api/get_barcodes/")]
    public ContentResult GetResponseBarCodes([FromQuery(Name = "StartDate")] DateTime dtStart, [FromQuery(Name = "EndDate")] DateTime dtEnd,
        [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponseBarCodes(dtStart, dtEnd, format), format);
        ControllerHelp.LogWebServiceFk(nameof(WsWebApiScales), LocaleCore.WebService.UrlGetBarcodes, 
            requestStampDt, $"{nameof(dtStart)}: {dtStart} & {nameof(dtEnd)}: {dtEnd}",
            result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/send_test/")]
    public ContentResult SendTest([FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(string.Empty, null, format), format);
        ControllerHelp.LogWebServiceFk(nameof(WsWebApiScales), LocaleCore.WebService.UrlSendTest, 
            requestStampDt, string.Empty, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/send_query")]
    public ContentResult SendSqlquery([FromBody] string query, [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(query, null, format), format);
        ControllerHelp.LogWebServiceFk(nameof(WsWebApiScales), LocaleCore.WebService.UrlSendQuery, 
            requestStampDt, query, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route("api/send_barcode/bottom/")]
    public ContentResult SendBarcodeBottom([FromBody] BarcodeBottomModel barcodeBottom, [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(SqlQueriesBarcodes.FindBottom, new("VALUE_BOTTOM", barcodeBottom.GetValue()), format), format);
        ControllerHelp.LogWebServiceFk(nameof(WsWebApiScales), LocaleCore.WebService.UrlSendBarcodeBottom, 
            requestStampDt, DataFormatUtils.GetContent<BarcodeBottomModel>(barcodeBottom, DataCore.Enums.FormatType.XmlUtf8, true),
            result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route("api/send_barcode/right/")]
    public ContentResult SendBarcodeRight([FromBody] BarcodeRightModel barcodeRight, [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(SqlQueriesBarcodes.FindRight, new("VALUE_RIGHT", barcodeRight.GetValue()), format), format);
        ControllerHelp.LogWebServiceFk(nameof(WsWebApiScales), LocaleCore.WebService.UrlSendBarcodeRight, 
            requestStampDt, DataFormatUtils.GetContent<BarcodeRightModel>(barcodeRight, DataCore.Enums.FormatType.XmlUtf8, true),
            result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/send_barcode/top/")]
    public ContentResult SendBarcodeTop([FromBody] BarcodeTopModel barcodeTop, [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "")
    {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(SqlQueriesBarcodes.FindTop, new("VALUE_TOP", barcodeTop.GetValue()), format), format);
        ControllerHelp.LogWebServiceFk(nameof(WsWebApiScales), LocaleCore.WebService.UrlSendBarcodeTop,
            requestStampDt, DataFormatUtils.GetContent<BarcodeTopModel>(barcodeTop, DataCore.Enums.FormatType.XmlUtf8, true),
            result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/send_barcode/top_v2/")]
    public ContentResult SendBarcodeTopV2([FromBody] string barcodeTop, [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(SqlQueriesBarcodes.FindTop,
                new("VALUE_TOP", DataFormatUtils.DeserializeFromXml<BarcodeTopModel>(barcodeTop).GetValue()), format), format);
        ControllerHelp.LogWebServiceFk(nameof(WsWebApiScales), LocaleCore.WebService.UrlSendBarcodeTopV2, 
            requestStampDt, barcodeTop, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    #endregion
}