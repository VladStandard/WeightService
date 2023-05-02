// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsWebApiCore.Helpers;

public sealed class WsPlusCharacteristicsHelper : WsContentBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsPlusCharacteristicsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsPlusCharacteristicsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    internal WsPlusCharacteristicsHelper(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    private List<WsXmlContentRecord<PluCharacteristicModel>> GetXmlPluCharacteristicsList(XElement xml) =>
        WsContentUtils.GetNodesListCore<PluCharacteristicModel>(xml, LocaleCore.WebService.XmlItemCharacteristic, (xmlNode, itemXml) =>
        {
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "AttachmentsCount");
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "NomenclatureGuid");
        });

    /// <summary>
    /// Добавить характеристику ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluCharacteristicsDb"></param>
    /// <param name="pluCharacteristicXml"></param>
    /// <param name="pluDb"></param>
    private void AddResponse1cPluCharacteristics(WsResponse1cShortModel response, List<PluCharacteristicModel> pluCharacteristicsDb,
        PluCharacteristicModel pluCharacteristicXml, PluModel pluDb)
    {
        try
        {
            // Найдено по Identity -> Обновить найденную запись.
            PluCharacteristicModel? itemDb = pluCharacteristicsDb.Find(x => x.IdentityValueUid.Equals(pluCharacteristicXml.IdentityValueUid));
            if (UpdateItem1cDb(response, pluCharacteristicXml, itemDb, true, pluDb.Number.ToString())) return;

            // Не найдено -> Добавить новую запись.
            bool isSave = SaveItemDb(response, pluCharacteristicXml, true);

            // Обновить список БД.
            if (isSave && !pluCharacteristicsDb.Select(x => x.IdentityValueUid).Contains(pluCharacteristicXml.IdentityValueUid))
                pluCharacteristicsDb.Add(pluCharacteristicXml);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluCharacteristicXml.Uid1c, ex);
        }
    }

    /// <summary>
    /// Добавить связь характеристики ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluCharacteristicsFksDb"></param>
    /// <param name="pluCharacteristicXml"></param>
    private void AddResponse1cPluCharacteristicsFks(WsResponse1cShortModel response,
        List<PluCharacteristicsFkModel> pluCharacteristicsFksDb, PluCharacteristicModel pluCharacteristicXml)
    {
        try
        {
            if (Equals(pluCharacteristicXml.NomenclatureGuid, Guid.Empty)) return;

            if (!GetPluDb(response, pluCharacteristicXml.NomenclatureGuid, pluCharacteristicXml.Uid1c,
                LocaleCore.WebService.FieldNomenclature, out PluModel? pluDb)) return;
            if (!GetPluCharacteristicDb(response, pluCharacteristicXml.Uid1c, pluCharacteristicXml.Uid1c,
                LocaleCore.WebService.FieldNomenclatureCharacteristic, out PluCharacteristicModel? pluCharacteristicDb)) return;
            if (pluDb is null || pluCharacteristicDb is null) return;

            PluCharacteristicsFkModel pluCharacteristicsFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Characteristic = pluCharacteristicDb,
            };

            // Найдено по Identity -> Обновить найденную запись.
            PluCharacteristicsFkModel? pluCharacteristicFkDb = pluCharacteristicsFksDb.Find(item =>
                Equals(item.Plu.Uid1c, pluCharacteristicsFk.Plu.Uid1c) &&
                Equals(item.Characteristic.Uid1c, pluCharacteristicsFk.Characteristic.Uid1c));
            if (UpdatePluCharacteristicFk(response, pluCharacteristicXml.Uid1c, pluCharacteristicsFk, pluCharacteristicFkDb, 
                    false, pluDb.Number)) return;

            // Не найдено -> Добавить новую запись.
            bool isSave = SaveItemDb(response, pluCharacteristicsFk, false, pluCharacteristicXml.Uid1c);

            // Обновить список БД.
            if (isSave && !pluCharacteristicsFksDb.Select(x => x.IdentityValueUid).Contains(pluCharacteristicsFk.IdentityValueUid))
                pluCharacteristicsFksDb.Add(pluCharacteristicsFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluCharacteristicXml.Uid1c, ex);
        }
    }

    /// <summary>
    /// Отправить номенклатурные характеристик и получить ответ.
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponse1cPluCharacteristics(XElement xml, string format, bool isDebug, ISessionFactory sessionFactory) =>
        NewResponse1cCore<WsResponse1cShortModel>(response =>
        {
            // Прогреть кеш.
            Cache.Load();
            List<WsXmlContentRecord<PluCharacteristicModel>> pluCharacteristicsXml = GetXmlPluCharacteristicsList(xml);
            foreach (WsXmlContentRecord<PluCharacteristicModel> record in pluCharacteristicsXml)
            {
                PluCharacteristicModel itemXml = record.Item;
                // Обновить данные в таблице связей обмена номенклатуры 1С.
                List<WsSqlPlu1cFkModel> plus1cFksDb = UpdatePlus1cFksDb(response, record);
                PluModel pluDb = ContextManager.ContextPlu.GetItemByUid1c(record.Item.NomenclatureGuid);
                // Проверить номер ПЛУ в списке доступа к выгрузке.
                if (itemXml.ParseResult.IsStatusSuccess)
                    CheckIsEnabledPlu(itemXml, plus1cFksDb);
                // Добавить характеристику ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                    AddResponse1cPluCharacteristics(response, Cache.PluCharacteristicsDb, itemXml, pluDb);
                // Добавить связь характеристики ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                    AddResponse1cPluCharacteristicsFks(response, Cache.PluCharacteristicsFksDb, itemXml);
                // Исключение.
                if (itemXml.ParseResult.IsStatusError)
                    AddResponse1cExceptionString(response, itemXml.Uid1c,
                        itemXml.ParseResult.Exception, itemXml.ParseResult.InnerException);
            }
        }, format, isDebug, sessionFactory);

    #endregion
}