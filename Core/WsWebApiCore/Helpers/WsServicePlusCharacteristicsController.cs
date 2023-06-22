// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Helpers;

/// <summary>
/// Веб-контроллер номенклатурных характеристик.
/// </summary>
public sealed class WsServicePlusCharacteristicsController : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    public WsServicePlusCharacteristicsController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Заполнить список характеристик ПЛУ из XML.
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    private List<WsXmlContentRecord<WsSqlPluCharacteristicModel>> GetXmlPluCharacteristicsList(XElement xml) =>
        WsServiceContentUtils.GetNodesListCore<WsSqlPluCharacteristicModel>(xml, WsLocaleCore.WebService.XmlItemCharacteristic, (xmlNode, itemXml) =>
        {
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "AttachmentsCount");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "NomenclatureGuid");
        });

    /// <summary>
    /// Добавить характеристику ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluCharacteristicXml"></param>
    private void AddResponsePluCharacteristics(WsResponse1CShortModel response, 
        WsSqlPluCharacteristicModel pluCharacteristicXml)
    {
        try
        {
            // Получить характеристику ПЛУ.
            WsSqlPluCharacteristicModel pluCharacteristicDb = WsServiceGetUtils.GetItemPluCharacteristic(
                WsSqlEnumContextType.Cache, response, pluCharacteristicXml.Uid1C,
                pluCharacteristicXml.Uid1C, WsLocaleCore.WebService.FieldNomenclatureCharacteristic);
            if (pluCharacteristicDb.IsExists)
            {
                // Обновить найденную запись.
                WsServiceUpdateUtils.UpdateItemDb(response, pluCharacteristicXml, pluCharacteristicDb);
                return;
            };
            // Не найдено -> Добавить новую запись.
            WsServiceUpdateUtils.SaveItemDb(response, pluCharacteristicXml, true, pluCharacteristicXml.Uid1C);
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.PluCharacteristics);
        }
        catch (Exception ex)
        {
            WsServiceResponseUtils.AddResponseException(response, pluCharacteristicXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Добавить связь характеристики ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluCharacteristicXml"></param>
    private void AddResponsePluCharacteristicsFks(WsResponse1CShortModel response, WsSqlPluCharacteristicModel pluCharacteristicXml)
    {
        try
        {
            if (Equals(pluCharacteristicXml.NomenclatureGuid, Guid.Empty)) return;
            // Получить ПЛУ.
            WsSqlPluModel pluDb = WsServiceGetUtils.GetItemPlu(WsSqlEnumContextType.Cache, response, 
                pluCharacteristicXml.NomenclatureGuid, pluCharacteristicXml.Uid1C, WsLocaleCore.WebService.FieldNomenclature);
            if (pluDb.IsNotExists) return;
            // Получить характеристику ПЛУ.
            WsSqlPluCharacteristicModel pluCharacteristicDb = WsServiceGetUtils.GetItemPluCharacteristic(
                WsSqlEnumContextType.Cache, response, pluCharacteristicXml.Uid1C, 
                pluCharacteristicXml.Uid1C, WsLocaleCore.WebService.FieldNomenclatureCharacteristic);
            if (pluCharacteristicDb.IsNotExists) return;
            // Связь характеристики и ПЛУ.
            WsSqlPluCharacteristicsFkModel pluCharacteristicsFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Characteristic = pluCharacteristicDb,
            };

            // Поиск по Identity.
            WsSqlPluCharacteristicsFkModel? pluCharacteristicFkDb = ContextCache.PlusCharacteristicsFks.Find(item =>
                Equals(item.Plu.Uid1C, pluCharacteristicsFk.Plu.Uid1C) &&
                Equals(item.Characteristic.Uid1C, pluCharacteristicsFk.Characteristic.Uid1C));
            if (pluCharacteristicFkDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUpdateUtils.UpdatePluCharacteristicFk(response, pluCharacteristicXml.Uid1C, pluCharacteristicsFk, pluCharacteristicFkDb);
                return;
            }

            // Не найдено -> Добавить новую запись.
            WsServiceUpdateUtils.SaveItemDb(response, pluCharacteristicsFk, false, pluCharacteristicXml.Uid1C);
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.PluCharacteristicsFks);
        }
        catch (Exception ex)
        {
            WsServiceResponseUtils.AddResponseException(response, pluCharacteristicXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Загрузить номенклатурные характеристик и получить ответ.
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponsePluCharacteristics(XElement xml, string format, bool isDebug, 
        ISessionFactory sessionFactory) => WsServiceResponseUtils.NewResponse1CCore<WsResponse1CShortModel>(response =>
        {
            // Заполнить таблицу связей разрешённых для загрузки ПЛУ из 1С.
            WsServiceUpdateUtils.FillPlus1CFksDb();
            // Загрузить кэш.
            ContextCache.Load();
            // Заполнить список характеристик ПЛУ из XML.
            List<WsXmlContentRecord<WsSqlPluCharacteristicModel>> pluCharacteristicsXml = GetXmlPluCharacteristicsList(xml);
            foreach (WsXmlContentRecord<WsSqlPluCharacteristicModel> recordXml in pluCharacteristicsXml)
            {
                WsSqlPluCharacteristicModel itemXml = recordXml.Item;
                // Обновить таблицу связей ПЛУ для обмена.
                List<WsSqlPlu1CFkModel> plus1CFksDb = WsServiceUpdateUtils.UpdatePlus1CFksDb(response, recordXml);
                WsSqlPluModel pluDb = ContextManager.ContextPlus.GetItemByUid1C(recordXml.Item.NomenclatureGuid);
                // Проверить разрешение обмена для ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                    WsServiceCheckUtils.CheckEnabledPlu(itemXml, plus1CFksDb);
                
                // Добавить характеристику ПЛУ.
                //if (itemXml.ParseResult.IsStatusSuccess)
                //    AddResponsePluCharacteristics(response, itemXml);
                // TODO: FIX HERE
                if (itemXml.ParseResult.IsStatusSuccess)
                {
                    itemXml.ParseResult.Status = WsEnumParseStatus.Error;
                    itemXml.ParseResult.Exception =
                        WsLocaleCore.WebService.FieldPluNumberTemplate(pluDb.Number) + WsLocaleCore.WebService.Underdevelopment(40);
                }
                //// Добавить связь характеристики ПЛУ.
                //if (itemXml.ParseResult.IsStatusSuccess)
                //    AddResponsePluCharacteristicsFks(response, itemXml);
                // Исключение.
                if (itemXml.ParseResult.IsStatusError)
                    WsServiceResponseUtils.AddResponseExceptionString(response, itemXml.Uid1C,
                        itemXml.ParseResult.Exception, itemXml.ParseResult.InnerException);
            }
        }, format, isDebug, sessionFactory);

    #endregion
}