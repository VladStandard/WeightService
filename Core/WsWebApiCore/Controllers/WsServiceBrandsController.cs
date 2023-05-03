// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Controllers;

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

    private List<WsXmlContentRecord<BrandModel>> GetXmlBrandList(XElement xml) =>
        WsServiceContentUtils.GetNodesListCore<BrandModel>(xml, LocaleCore.WebService.XmlItemBrand,
            (xmlNode, itemXml) =>
            {
                WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
                WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
                WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
                WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
            });

    private void AddResponse1cBrand(WsResponse1CShortModel response, BrandModel brandXml)
    {
        try
        {
            // Найдено по Uid1C -> Обновить найденную запись.
            BrandModel? brandDb = Cache.BrandsDb.Find(item => Equals(item.Uid1C, brandXml.IdentityValueUid));
            if (UpdateBrandDb(response, brandXml.Uid1C, brandXml, brandDb, true)) return;

            // Найдено по Code -> Обновить найденную запись.
            brandDb = Cache.BrandsDb.Find(item => Equals(item.Code, brandXml.Code));
            if (UpdateBrandDb(response, brandXml.Uid1C, brandXml, brandDb, true)) return;

            // Найдено по Name -> Обновить найденную запись.
            brandDb = Cache.BrandsDb.Find(item => Equals(item.Name, brandXml.Name));
            if (UpdateBrandDb(response, brandXml.Uid1C, brandXml, brandDb, true)) return;

            // Не найдено -> Добавить новую запись.
            if (SaveItemDb(response, brandXml, true))
                // Обновить список БД.
                Cache.Load(WsSqlTableName.Brands);
        }
        catch (Exception ex)
        {
            AddResponse1CException(response, brandXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Загрузить бренды и получить ответ.
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="formatString"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponse1cBrands(XElement xml, string formatString, bool isDebug, ISessionFactory sessionFactory) =>
        NewResponse1CCore<WsResponse1CShortModel>(response =>
        {
            // Прогреть кэш.
            Cache.Load();
            List<WsXmlContentRecord<BrandModel>> itemsXml = GetXmlBrandList(xml);
            foreach (WsXmlContentRecord<BrandModel> record in itemsXml)
            {
                BrandModel brandXml = record.Item;
                switch (brandXml.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cBrand(response, brandXml);
                        break;
                    case ParseStatus.Error:
                        AddResponse1CException(response, brandXml);
                        break;
                }
            }
        }, formatString, isDebug, sessionFactory);

    #endregion
}