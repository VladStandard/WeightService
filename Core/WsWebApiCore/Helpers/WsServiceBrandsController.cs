namespace WsWebApiCore.Helpers;

/// <summary>
/// Веб-контроллер брендов.
/// </summary>
public sealed class WsServiceBrandsController : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    public WsServiceBrandsController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Загрузить бренды и получить ответ.
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="formatString"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponseBrands(XElement xml, string formatString, bool isDebug, ISessionFactory sessionFactory) =>
        WsServiceUtilsResponse.NewResponse1CCore<WsResponse1CShortModel>(response =>
        {
            // Загрузить кэш.
            WsServiceUtils.ContextCache.Load();
            List<WsXmlContentRecord<WsSqlBrandModel>> itemsXml = WsServiceUtilsGetXml.GetXmlBrandList(xml);
            foreach (WsXmlContentRecord<WsSqlBrandModel> record in itemsXml)
            {
                WsSqlBrandModel brandXml = record.Item;
                switch (brandXml.ParseResult.Status)
                {
                    case WsEnumParseStatus.Success:
                        WsServiceUtilsSave.SaveBrand(response, brandXml);
                        break;
                    case WsEnumParseStatus.Error:
                        WsServiceUtilsResponse.AddResponseException(response, brandXml);
                        break;
                }
            }
        }, formatString, isDebug, sessionFactory);

    #endregion
}