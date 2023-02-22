// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Utils;
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
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("api/get_barcode/top/")]
    [Route("api/v3/get_barcode/top/")]
    public ContentResult GetBarcodeTop([FromQuery] string barcode, bool useCrc = false,
        [FromQuery(Name = "format")] string format = "", [FromHeader(Name = "host")] string host = "")
    {
        DateTime stampDt = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/get_barcode/top/", stampDt, barcode, format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            DataFormatUtils.GetContentResult<BarcodeTopModel>(new BarcodeTopModel(barcode, useCrc), format, HttpStatusCode.OK), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/get_barcode/top/", stampDt, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// Get down barcode.
    /// </summary>
    /// <param name="barcode"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("api/get_barcode/bottom/")]
    [Route("api/v3/get_barcode/bottom/")]
    public ContentResult GetBarcodeBottom([FromQuery] string barcode, [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "")
    {
        DateTime stampDt = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/get_barcode/bottom/", stampDt, barcode, format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            DataFormatUtils.GetContentResult<BarcodeBottomModel>(new BarcodeBottomModel(barcode), format, HttpStatusCode.OK), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/get_barcode/bottom/", stampDt, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// Get right barcode.
    /// </summary>
    /// <param name="barcode"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("api/get_barcode/right/")]
    [Route("api/v3/get_barcode/right/")]
    public ContentResult GetBarcodeRight([FromQuery] string barcode, [FromQuery(Name = "format")] string format = "", 
        [FromHeader(Name = "host")] string host = "")
    {
        DateTime stampDt = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/get_barcode/right/", stampDt, barcode, format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            DataFormatUtils.GetContentResult<BarcodeRightModel>(new BarcodeRightModel(barcode), format, HttpStatusCode.OK), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/get_barcode/right/", stampDt, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// Get barcode.
    /// </summary>
    /// <param name="dtEnd"></param>
    /// <param name="format"></param>
    /// <param name="dtStart"></param>
    /// <param name="host"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("api/get_barcodes/")]
    [Route("api/v3/get_barcodes/")]
    public ContentResult GetResponseBarCodes([FromQuery(Name = "StartDate")] DateTime dtStart, [FromQuery(Name = "EndDate")] DateTime dtEnd,
        [FromQuery(Name = "format")] string format = "", [FromHeader(Name = "host")] string host = "")
    {
        DateTime stampDt = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/get_barcodes/", stampDt, $"{nameof(dtStart)}: {dtStart} & {nameof(dtEnd)}: {dtEnd}", 
            format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponseBarCodes(dtStart, dtEnd, format), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/get_barcodes/", stampDt, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/send_test/")]
    [Route("api/v3/send_test/")]
    public ContentResult SendTest([FromQuery(Name = "format")] string format = "", [FromHeader(Name = "host")] string host = "")
    {
        DateTime stampDt = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/send_test/", stampDt, string.Empty, format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(string.Empty, null, format), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/send_test/", stampDt, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/send_query")]
    [Route("api/v3/send_query")]
    public ContentResult SendSqlquery([FromBody] string query, [FromQuery(Name = "format")] string format = "", 
        [FromHeader(Name = "host")] string host = "")
    {
        DateTime stampDt = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/send_query", stampDt, query, format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(query, null, format), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/send_query", stampDt, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route("api/send_barcode/bottom/")]
    [Route("api/v3/send_barcode/bottom/")]
    public ContentResult SendBarcodeBottom([FromBody] BarcodeBottomModel barcodeBottom, [FromQuery(Name = "format")] string format = "", 
        [FromHeader(Name = "host")] string host = "")
    {
        DateTime stampDt = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/send_barcode/bottom/", stampDt,
            DataFormatUtils.GetContent<BarcodeBottomModel>(barcodeBottom, DataCore.Enums.FormatType.XmlUtf8, true),
            format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(SqlQueriesBarcodes.FindBottom, new("VALUE_BOTTOM", barcodeBottom.GetValue()), format), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/send_barcode/bottom/", stampDt, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route("api/send_barcode/right/")]
    [Route("api/v3/send_barcode/right/")]
    public ContentResult SendBarcodeRight([FromBody] BarcodeRightModel barcodeRight, [FromQuery(Name = "format")] string format = "", 
        [FromHeader(Name = "host")] string host = "")
    {
        DateTime stampDt = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/send_barcode/right/", stampDt, 
            DataFormatUtils.GetContent<BarcodeRightModel>(barcodeRight, DataCore.Enums.FormatType.XmlUtf8, true), 
            format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(SqlQueriesBarcodes.FindRight, new("VALUE_RIGHT", barcodeRight.GetValue()), format), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/send_barcode/right/", stampDt, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/send_barcode/top/")]
    [Route("api/v3/send_barcode/top/")]
    public ContentResult SendBarcodeTop([FromBody] BarcodeTopModel barcodeTop, [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "")
    {
        DateTime stampDt = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/send_barcode/top/", stampDt,
            DataFormatUtils.GetContent<BarcodeTopModel>(barcodeTop, DataCore.Enums.FormatType.XmlUtf8, true), 
            format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(SqlQueriesBarcodes.FindTop, new("VALUE_TOP", barcodeTop.GetValue()), format), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/send_barcode/top/", stampDt, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/send_barcode/top_v2/")]
    [Route("api/v3/send_barcode/top_v2/")]
    public ContentResult SendBarcodeTopV2([FromBody] string barcodeTop, [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "")
    {
        DateTime stampDt = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/send_barcode/top_v2/", stampDt, barcodeTop, format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(SqlQueriesBarcodes.FindTop,
                new("VALUE_TOP", DataFormatUtils.DeserializeFromXml<BarcodeTopModel>(barcodeTop).GetValue()), format), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/send_barcode/top_v2/", stampDt, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    #endregion
}