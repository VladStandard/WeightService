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
        DateTime dtStamp = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/get_barcode/top/", dtStamp, barcode, format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            new BarcodeTopModel(barcode, useCrc).GetContentResult<BarcodeTopModel>(format, HttpStatusCode.OK), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/get_barcode/top/", dtStamp, result, format, host, string.Empty).ConfigureAwait(false);
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
        DateTime dtStamp = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/get_barcode/bottom/", dtStamp, barcode, format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            new BarcodeBottomModel(barcode).GetContentResult<BarcodeBottomModel>(format, HttpStatusCode.OK), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/get_barcode/bottom/", dtStamp, result, format, host, string.Empty).ConfigureAwait(false);
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
        DateTime dtStamp = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/get_barcode/right/", dtStamp, barcode, format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            new BarcodeRightModel(barcode).GetContentResult<BarcodeRightModel>(format, HttpStatusCode.OK), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/get_barcode/right/", dtStamp, result, format, host, string.Empty).ConfigureAwait(false);
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
        DateTime dtStamp = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/get_barcodes/", dtStamp, $"{nameof(dtStart)}: {dtStart} & {nameof(dtEnd)}: {dtEnd}", 
            format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponseBarCodes(SessionFactory, dtStart, dtEnd, format), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/get_barcodes/", dtStamp, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/send_test/")]
    [Route("api/v3/send_test/")]
    public ContentResult SendTest([FromQuery(Name = "format")] string format = "", [FromHeader(Name = "host")] string host = "")
    {
        DateTime dtStamp = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/send_test/", dtStamp, string.Empty, format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(SessionFactory, string.Empty, null, format, false), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/send_test/", dtStamp, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/send_query")]
    [Route("api/v3/send_query")]
    public ContentResult SendSqlquery([FromBody] string query, [FromQuery(Name = "format")] string format = "", 
        [FromHeader(Name = "host")] string host = "")
    {
        DateTime dtStamp = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/send_query", dtStamp, query, format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(SessionFactory, query, null, format, false), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/send_query", dtStamp, result, format, host, string.Empty).ConfigureAwait(false);
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
        DateTime dtStamp = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/send_barcode/bottom/", dtStamp,
            DataFormatUtils.GetContent<BarcodeBottomModel>(barcodeBottom, DataCore.Enums.FormatType.XmlUtf8, true),
            format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(SessionFactory,
                SqlQueriesBarcodes.FindBottom, new("VALUE_BOTTOM", barcodeBottom.GetValue()), format, false), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/send_barcode/bottom/", dtStamp, result, format, host, string.Empty).ConfigureAwait(false);
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
        DateTime dtStamp = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/send_barcode/right/", dtStamp, 
            DataFormatUtils.GetContent<BarcodeRightModel>(barcodeRight, DataCore.Enums.FormatType.XmlUtf8, true), 
            format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(SessionFactory, SqlQueriesBarcodes.FindRight,
                new("VALUE_RIGHT", barcodeRight.GetValue()), format, false), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/send_barcode/right/", dtStamp, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/send_barcode/top/")]
    [Route("api/v3/send_barcode/top/")]
    public ContentResult SendBarcodeTop([FromBody] BarcodeTopModel barcodeTop, [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "")
    {
        DateTime dtStamp = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/send_barcode/top/", dtStamp,
            DataFormatUtils.GetContent<BarcodeTopModel>(barcodeTop, DataCore.Enums.FormatType.XmlUtf8, true), 
            format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(SessionFactory, SqlQueriesBarcodes.FindTop,
                new("VALUE_TOP", barcodeTop.GetValue()), format, false), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/send_barcode/top/", dtStamp, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/send_barcode/top_v2/")]
    [Route("api/v3/send_barcode/top_v2/")]
    public ContentResult SendBarcodeTopV2([FromBody] string barcodeTop, [FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "")
    {
        DateTime dtStamp = DateTime.Now;
        ControllerHelp.LogRequest(nameof(WsWebApiScales), "api/send_barcode/top_v2/", dtStamp, barcodeTop, format, host, string.Empty).ConfigureAwait(false);
        ContentResult result = ControllerHelp.GetContentResult(() =>
            ControllerHelp.NewResponse1cFromQuery(SessionFactory, SqlQueriesBarcodes.FindTop,
                new("VALUE_TOP", DataFormatUtils.DeserializeFromXml<BarcodeTopModel>(barcodeTop).GetValue()),
                format, false), format);
        ControllerHelp.LogResponse(nameof(WsWebApiScales), "api/send_barcode/top_v2/", dtStamp, result, format, host, string.Empty).ConfigureAwait(false);
        return result;
    }

    #endregion
}