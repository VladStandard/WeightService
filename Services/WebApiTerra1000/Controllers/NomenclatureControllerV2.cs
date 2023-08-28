using WsWebApiCore.Common;

namespace WebApiTerra1000.Controllers;

public sealed class NomenclatureControllerV2 : WsServiceControllerBase
{
    #region Constructor and destructor

    public NomenclatureControllerV2(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetNomenclatureV2)]
    public ContentResult GetNomenclatureFromCodeIdProd([FromQuery] string code, [FromQuery] long id,
        [FromQuery(Name = "format")] string format = "") =>
        GetNomenclatureFromCodeIdWork(code != null
            ? WsServiceUtilsSqlQueriesNomenclaturesV2.GetNomenclatureFromCodeProd : WsServiceUtilsSqlQueriesNomenclaturesV2.GetNomenclatureFromIdProd,
            code, id, format);

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetNomenclatureV2Preview)]
    public ContentResult GetNomenclatureFromCodeIdPreview([FromQuery] string code, [FromQuery] long id,
        [FromQuery(Name = "format")] string format = "") =>
        GetNomenclatureFromCodeIdWork(code != null
            ? WsServiceUtilsSqlQueriesNomenclaturesV2.GetNomenclatureFromCodePreview : WsServiceUtilsSqlQueriesNomenclaturesV2.GetNomenclatureFromIdPreview,
            code, id, format);

    private ContentResult GetNomenclatureFromCodeIdWork([FromQuery] string url, [FromQuery] string code, [FromQuery] long id,
        [FromQuery(Name = "format")] string format = "")
    {
        return WsServiceUtilsGetXmlContent.GetContentResult(() =>
        {
            string response = WsServiceUtilsSql.GetResponse<string>(SessionFactory, url,
                code != null ? WsServiceUtilsSql.GetParametersV2(code) : WsServiceUtilsSql.GetParametersV2(id));
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetNomenclaturesV2)]
    public ContentResult GetNomenclaturesProd([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate = null,
        [FromQuery] int? offset = null, [FromQuery] int? rowCount = null, [FromQuery(Name = "format")] string format = "")
    {
        if (startDate != null && endDate != null && offset != null && rowCount != null)
            return GetNomenclaturesWork(WsServiceUtilsSqlQueriesNomenclaturesV2.GetNomenclaturesFromDatesOffsetProd, startDate, endDate, offset, rowCount, format);
        if (startDate != null && endDate != null)
            return GetNomenclaturesWork(WsServiceUtilsSqlQueriesNomenclaturesV2.GetNomenclaturesFromDatesProd, startDate, endDate, offset, rowCount, format);
        if (startDate != null && endDate == null)
            return GetNomenclaturesWork(WsServiceUtilsSqlQueriesNomenclaturesV2.GetNomenclaturesFromStartDateProd, startDate, endDate, offset, rowCount, format);

        return GetNomenclaturesEmptyWork(WsServiceUtilsSqlQueriesNomenclaturesV2.GetNomenclaturesEmptyProd, format);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetNomenclaturesCostsV2)]
    public ContentResult GetNomenclaturesProdDeprecated([FromQuery(Name = "format")] string format = "") =>
        WsServiceUtilsGetXmlContent.GetContentResult(() => WsDataFormatUtils.GetContentResult<WsServiceReplyModel>(
            new WsServiceReplyModel("Deprecated method. Use: api/nomenclatures/"), format, HttpStatusCode.OK), format);

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetNomenclaturesPreviewV2)]
    public ContentResult GetNomenclaturesPreview([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate = null,
        [FromQuery] int? offset = null, [FromQuery] int? rowCount = null, [FromQuery(Name = "format")] string format = "")
    {
        if (startDate != null && endDate != null && offset != null && rowCount != null)
            return GetNomenclaturesWork(WsServiceUtilsSqlQueriesNomenclaturesV2.GetNomenclaturesFromDatesOffsetPreview, startDate, endDate, offset, rowCount, format);
        if (startDate != null && endDate != null)
            return GetNomenclaturesWork(WsServiceUtilsSqlQueriesNomenclaturesV2.GetNomenclaturesFromDatesPreview, startDate, endDate, offset, rowCount, format);
        if (startDate != null && endDate == null)
            return GetNomenclaturesWork(WsServiceUtilsSqlQueriesNomenclaturesV2.GetNomenclaturesFromStartDatePreview, startDate, endDate, offset, rowCount, format);

        return GetNomenclaturesEmptyWork(WsServiceUtilsSqlQueriesNomenclaturesV2.GetNomenclaturesEmptyPreview, format);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route(WsLocaleWebServiceUtils.GetNomenclaturesCostsPreviewV2)]
    public ContentResult GetNomenclaturesPreviewDeprecated([FromQuery(Name = "format")] string format = "") =>
        WsServiceUtilsGetXmlContent.GetContentResult(() => WsDataFormatUtils.GetContentResult<WsServiceReplyModel>(
            new WsServiceReplyModel("Deprecated method. Use: api/nomenclatures_preview/"), format, HttpStatusCode.OK), format);

    private ContentResult GetNomenclaturesEmptyWork(string url, string format = "")
    {
        return WsServiceUtilsGetXmlContent.GetContentResult(() =>
        {
            string response = WsServiceUtilsSql.GetResponse<string>(SessionFactory, url);
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    private ContentResult GetNomenclaturesWork(string url, DateTime? startDate = null, DateTime? endDate = null,
        int? offset = null, int? rowCount = null, [FromQuery(Name = "format")] string format = "")
    {
        return WsServiceUtilsGetXmlContent.GetContentResult(() =>
        {
            List<SqlParameter> parameters = null;
            if (startDate != null && endDate != null && offset != null && rowCount != null)
                parameters = WsServiceUtilsSql.GetParametersV2(startDate, endDate, offset, rowCount);
            else if (startDate != null && endDate != null)
                parameters = WsServiceUtilsSql.GetParametersV2(startDate, endDate);
            else if (startDate != null && endDate == null)
                parameters = WsServiceUtilsSql.GetParametersV2(startDate);
            string response = WsServiceUtilsSql.GetResponse<string>(SessionFactory, url, parameters);
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    #endregion
}