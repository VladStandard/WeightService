// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

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
    /// <param name="pluDb"></param>
    private void AddResponsePluCharacteristics(WsResponse1CShortModel response, WsSqlPluCharacteristicModel pluCharacteristicXml,
        WsSqlPluModel pluDb)
    {
        try
        {
            // Найдено по Identity -> Обновить найденную запись.
            WsSqlPluCharacteristicModel? itemDb = Cache.PlusCharacteristics.Find(item => item.IdentityValueUid.Equals(pluCharacteristicXml.IdentityValueUid));
            if (UpdateItemDb(response, pluCharacteristicXml, itemDb, true, pluDb.Number.ToString())) return;

            // Не найдено -> Добавить новую запись.
            if (SaveItemDb(response, pluCharacteristicXml, true))
                // Обновить кэш.
                Cache.Load(WsSqlEnumTableName.PluCharacteristics);
        }
        catch (Exception ex)
        {
            AddResponseException(response, pluCharacteristicXml.Uid1C, ex);
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

            if (!GetPluDb(response, pluCharacteristicXml.NomenclatureGuid, pluCharacteristicXml.Uid1C,
                    WsLocaleCore.WebService.FieldNomenclature, out WsSqlPluModel? pluDb)) return;
            if (!GetPluCharacteristicDb(response, pluCharacteristicXml.Uid1C, pluCharacteristicXml.Uid1C,
                    WsLocaleCore.WebService.FieldNomenclatureCharacteristic, out WsSqlPluCharacteristicModel? pluCharacteristicDb)) return;
            if (pluDb is null || pluCharacteristicDb is null) return;

            WsSqlPluCharacteristicsFkModel pluCharacteristicsFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Characteristic = pluCharacteristicDb,
            };

            // Найдено по Identity -> Обновить найденную запись.
            WsSqlPluCharacteristicsFkModel? pluCharacteristicFkDb = Cache.PlusCharacteristicsFks.Find(item =>
                Equals(item.Plu.Uid1C, pluCharacteristicsFk.Plu.Uid1C) &&
                Equals(item.Characteristic.Uid1C, pluCharacteristicsFk.Characteristic.Uid1C));
            if (UpdatePluCharacteristicFk(response, pluCharacteristicXml.Uid1C, pluCharacteristicsFk, pluCharacteristicFkDb,
                    false, pluDb.Number)) return;

            // Не найдено -> Добавить новую запись.
            if (SaveItemDb(response, pluCharacteristicsFk, false, pluCharacteristicXml.Uid1C))
                // Обновить кэш.
                Cache.Load(WsSqlEnumTableName.PluCharacteristicsFks);
        }
        catch (Exception ex)
        {
            AddResponseException(response, pluCharacteristicXml.Uid1C, ex);
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
    public ContentResult NewResponsePluCharacteristics(XElement xml, string format, bool isDebug, ISessionFactory sessionFactory) =>
        NewResponse1CCore<WsResponse1CShortModel>(response =>
        {
            // Загрузить кэш.
            Cache.Load();
            List<WsXmlContentRecord<WsSqlPluCharacteristicModel>> pluCharacteristicsXml = GetXmlPluCharacteristicsList(xml);
            foreach (WsXmlContentRecord<WsSqlPluCharacteristicModel> record in pluCharacteristicsXml)
            {
                WsSqlPluCharacteristicModel itemXml = record.Item;
                // Обновить данные в таблице связей обмена номенклатуры 1С.
                List<WsSqlPlu1CFkModel> plus1CFksDb = UpdatePlus1CFksDb(response, record);
                WsSqlPluModel pluDb = ContextManager.ContextPlus.GetItemByUid1C(record.Item.NomenclatureGuid);
                // Проверить номер ПЛУ в списке доступа к выгрузке.
                if (itemXml.ParseResult.IsStatusSuccess)
                    CheckIsEnabledPlu(itemXml, plus1CFksDb);
                // Добавить характеристику ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                    AddResponsePluCharacteristics(response, itemXml, pluDb);
                // Добавить связь характеристики ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                    AddResponsePluCharacteristicsFks(response, itemXml);
                // Исключение.
                if (itemXml.ParseResult.IsStatusError)
                    AddResponseExceptionString(response, itemXml.Uid1C,
                        itemXml.ParseResult.Exception, itemXml.ParseResult.InnerException);
            }
        }, format, isDebug, sessionFactory);

    #endregion
}