// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiScales.Helpers;

/// <summary>
/// Barcodes controller.
/// </summary>
[Tags(WsLocaleWebServiceUtils.Tag1CBarCodes)]
public sealed class WsServiceBarCodesWrapper : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    public WsServiceBarCodesWrapper(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods
    
    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetBarcodeTop)]
    public ContentResult GetBarcodeTop([FromQuery] string barcode, bool useCrc = false,
        [FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false,
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = WsServiceUtilsGetXmlContent.GetContentResult(() =>
            WsDataFormatUtils.GetContentResult<WsSqlBarcodeTopModel>(new WsSqlBarcodeTopModel(barcode, useCrc), format, HttpStatusCode.OK), format);
        WsServiceUtilsLog.LogWebServiceFk(nameof(WsWebApiScales), WsLocaleWebServiceUtils.GetBarcodeTop,
            requestStampDt, barcode, result.Content ?? string.Empty, format, host, version);
        return result;
    }
    
    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetBarcodeBottom)]
    public ContentResult GetBarcodeBottom([FromQuery] string barcode, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = WsServiceUtilsGetXmlContent.GetContentResult(() =>
            WsDataFormatUtils.GetContentResult<WsSqlBarcodeBottomModel>(new WsSqlBarcodeBottomModel(barcode), format, HttpStatusCode.OK), format);
        WsServiceUtilsLog.LogWebServiceFk(nameof(WsWebApiScales), WsLocaleWebServiceUtils.GetBarcodeBottom, 
            requestStampDt, barcode, result.Content ?? string.Empty, format, host, version);
        return result;
    }
    
    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetBarcodeRight)]
    public ContentResult GetBarcodeRight([FromQuery] string barcode, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = WsServiceUtilsGetXmlContent.GetContentResult(() =>
            WsDataFormatUtils.GetContentResult<WsSqlBarcodeRightModel>(new WsSqlBarcodeRightModel(barcode), format, HttpStatusCode.OK), format);
        WsServiceUtilsLog.LogWebServiceFk(nameof(WsWebApiScales), WsLocaleWebServiceUtils.GetBarcodeRight, 
            requestStampDt, barcode, result.Content ?? string.Empty, format, host, version);
        return result;
    }
    
    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetBarcodes)]
    public ContentResult GetResponseBarCodes([FromQuery(Name = "StartDate")] DateTime dtStart, [FromQuery(Name = "EndDate")] DateTime dtEnd,
        [FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = WsServiceUtilsGetXmlContent.GetContentResult(() =>
            WsServiceUtilsResponse.NewResponseBarCodes(dtStart, dtEnd, format, isDebug, SessionFactory), format);
        WsServiceUtilsLog.LogWebServiceFk(nameof(WsWebApiScales), WsLocaleWebServiceUtils.GetBarcodes, 
            requestStampDt, $"{nameof(dtStart)}: {dtStart} & {nameof(dtEnd)}: {dtEnd}",
            result.Content ?? string.Empty, format, host, version);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route(WsLocaleWebServiceUtils.SendTest)]
    public ContentResult SendTest([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = WsServiceUtilsGetXmlContent.GetContentResult(() =>
            WsServiceUtilsResponse.NewResponse1CFromQuery(string.Empty, null, format, isDebug, SessionFactory), format);
        WsServiceUtilsLog.LogWebServiceFk(nameof(WsWebApiScales), WsLocaleWebServiceUtils.SendTest, 
            requestStampDt, string.Empty, result.Content ?? string.Empty, format, host, version);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route(WsLocaleWebServiceUtils.SendQuery)]
    public ContentResult SendSqlquery([FromBody] string query, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = WsServiceUtilsGetXmlContent.GetContentResult(() =>
            WsServiceUtilsResponse.NewResponse1CFromQuery(query, null, format, isDebug, SessionFactory), format);
        WsServiceUtilsLog.LogWebServiceFk(nameof(WsWebApiScales), WsLocaleWebServiceUtils.SendQuery, 
            requestStampDt, query, result.Content ?? string.Empty, format, host, version);
        return result;
    }

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route(WsLocaleWebServiceUtils.SendBarcodeBottom)]
    public ContentResult SendBarcodeBottom([FromBody] WsSqlBarcodeBottomModel barcodeBottom, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = WsServiceUtilsGetXmlContent.GetContentResult(() =>
            WsServiceUtilsResponse.NewResponse1CFromQuery(WsServiceUtilsSqlQueriesBarcodes.FindBottom, new("VALUE_BOTTOM", barcodeBottom.GetValue()), 
                format, isDebug, SessionFactory), format);
        WsServiceUtilsLog.LogWebServiceFk(nameof(WsWebApiScales), WsLocaleWebServiceUtils.SendBarcodeBottom, 
            requestStampDt, WsDataFormatUtils.GetContent<WsSqlBarcodeBottomModel>(barcodeBottom, WsEnumFormatType.XmlUtf8, true),
            result.Content ?? string.Empty, format, host, version);
        return result;
    }

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route(WsLocaleWebServiceUtils.SendBarcodeRight)]
    public ContentResult SendBarcodeRight([FromBody] WsSqlBarcodeRightModel barcodeRight, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = WsServiceUtilsGetXmlContent.GetContentResult(() =>
            WsServiceUtilsResponse.NewResponse1CFromQuery(WsServiceUtilsSqlQueriesBarcodes.FindRight, new("VALUE_RIGHT", barcodeRight.GetValue()), 
                format, isDebug, SessionFactory), format);
        WsServiceUtilsLog.LogWebServiceFk(nameof(WsWebApiScales), WsLocaleWebServiceUtils.SendBarcodeRight, 
            requestStampDt, WsDataFormatUtils.GetContent<WsSqlBarcodeRightModel>(barcodeRight, WsEnumFormatType.XmlUtf8, true),
            result.Content ?? string.Empty, format, host, version);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route(WsLocaleWebServiceUtils.SendBarcodeTop)]
    public ContentResult SendBarcodeTop([FromBody] WsSqlBarcodeTopModel barcodeTop, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "")
    {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = WsServiceUtilsGetXmlContent.GetContentResult(() =>
            WsServiceUtilsResponse.NewResponse1CFromQuery(WsServiceUtilsSqlQueriesBarcodes.FindTop, new("VALUE_TOP", barcodeTop.GetValue()), 
                format, isDebug, SessionFactory), format);
        WsServiceUtilsLog.LogWebServiceFk(nameof(WsWebApiScales), WsLocaleWebServiceUtils.SendBarcodeTop,
            requestStampDt, WsDataFormatUtils.GetContent<WsSqlBarcodeTopModel>(barcodeTop, WsEnumFormatType.XmlUtf8, true),
            result.Content ?? string.Empty, format, host, version);
        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route(WsLocaleWebServiceUtils.SendBarcodeTopV2)]
    public ContentResult SendBarcodeTopV2([FromBody] string barcodeTop, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = WsServiceUtilsGetXmlContent.GetContentResult(() =>
            WsServiceUtilsResponse.NewResponse1CFromQuery(WsServiceUtilsSqlQueriesBarcodes.FindTop,
                new("VALUE_TOP", WsDataFormatUtils.DeserializeFromXml<WsSqlBarcodeTopModel>(barcodeTop).GetValue()), 
                format, isDebug, SessionFactory), format);
        WsServiceUtilsLog.LogWebServiceFk(nameof(WsWebApiScales), WsLocaleWebServiceUtils.SendBarcodeTopV2, 
            requestStampDt, barcodeTop, result.Content ?? string.Empty, format, host, version);
        return result;
    }

    #endregion
}