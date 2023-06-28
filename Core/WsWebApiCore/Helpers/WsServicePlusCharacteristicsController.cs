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
    /// Загрузить номенклатурные характеристик и получить ответ.
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponsePluCharacteristics(XElement xml, string format, bool isDebug, 
        ISessionFactory sessionFactory) => WsServiceUtilsResponse.NewResponse1CCore<WsResponse1CShortModel>(response =>
        {
            // Заполнить таблицу связей разрешённых для загрузки ПЛУ из 1С.
            WsServiceUtilsUpdate.FillPlus1CFksDb();
            // Загрузить кэш.
            WsServiceUtils.ContextCache.Load();
            // Заполнить список характеристик ПЛУ из XML.
            List<WsXmlContentRecord<WsSqlPluCharacteristicModel>> pluCharacteristicsXml = WsServiceUtilsGetXml.GetXmlPluCharacteristicsList(xml);
            foreach (WsXmlContentRecord<WsSqlPluCharacteristicModel> recordXml in pluCharacteristicsXml)
            {
                WsSqlPluCharacteristicModel itemXml = recordXml.Item;
                // Обновить таблицу связей ПЛУ для обмена.
                List<WsSqlPlu1CFkModel> plus1CFksDb = WsServiceUtilsUpdate.UpdatePlus1CFksDb(response, recordXml);
                WsSqlPluModel pluDb = WsServiceUtils.ContextManager.ContextPlus.GetItemByUid1C(recordXml.Item.NomenclatureGuid);
                // Проверить разрешение обмена для ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess) WsServiceUtilsCheck.CheckEnabledPlu(itemXml, plus1CFksDb);
                // Сохранить характеристику ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SavePluCharacteristics(response, itemXml);
                // Сохранить связь характеристики ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SavePluCharacteristicsFks(response, itemXml);
                // Исключение.
                if (itemXml.ParseResult.IsStatusError)
                    WsServiceUtilsResponse.AddResponseExceptionString(response, itemXml.Uid1C,
                        itemXml.ParseResult.Exception, itemXml.ParseResult.InnerException);
            }
        }, format, isDebug, sessionFactory);

    #endregion
}