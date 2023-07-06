// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using static WsStorageCore.Utils.WsSqlQueriesScales.Tables;

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
                WsSqlPluCharacteristicModel characteristicXml = recordXml.Item;
                // Обновить таблицу связей ПЛУ для обмена.
                List<WsSqlPlu1CFkModel> plus1CFksDb = WsServiceUtilsUpdate.UpdatePlus1CFksDb(response, recordXml);
                WsSqlPluModel pluDb = WsServiceUtils.ContextManager.ContextPlus.GetItemByUid1C(recordXml.Item.NomenclatureGuid);
                // Проверить разрешение обмена для ПЛУ.
                if (characteristicXml.ParseResult.IsStatusSuccess) WsServiceUtilsCheck.CheckEnabledPlu(characteristicXml, plus1CFksDb);
                
                // Сохранить характеристику ПЛУ.
                if (characteristicXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SavePluCharacteristics(response, characteristicXml);
                // Сохранить связь характеристики ПЛУ.
                if (characteristicXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SavePluCharacteristicsFks(response, characteristicXml);
                if (characteristicXml.ParseResult.IsStatusSuccess)
                {
                    // Получить вложенность ПЛУ по-умолчанию.
                    WsSqlPluNestingFkModel pluNestingFkDefault = WsServiceUtilsGet.GetItemPluNestingFkDefault(
                        WsSqlEnumContextType.Cache, response, characteristicXml.NomenclatureGuid, 
                        characteristicXml.Uid1C, "Вложенность ПЛУ по-умолчанию", characteristicXml);
                    if (characteristicXml.ParseResult.IsStatusSuccess)
                    {
                        // Получить список вложенностей ПЛУ.
                        List<WsSqlPluNestingFkModel> pluNestingFks = WsServiceUtilsGet.GetListPluNestingFks(
                            WsSqlEnumContextType.Cache, response, pluDb.Uid1C, characteristicXml.NomenclatureGuid, "Вложенности ПЛУ");
                        // Отфильтровать список вложенностей ПЛУ.
                        List<WsSqlPluNestingFkModel> pluNestingFksOther =
                            pluNestingFks.Where(item => !item.IdentityValueUid.Equals(pluNestingFkDefault.IdentityValueUid)).ToList();
                        // Вложенность является по-умолчанию.
                        if (pluNestingFkDefault.IsExists && pluNestingFkDefault.BundleCount.Equals((short)characteristicXml.AttachmentsCount))
                        {
                            characteristicXml.ParseResult.Status = WsEnumParseStatus.Error;
                            characteristicXml.ParseResult.Exception = WsLocaleCore.WebService.FieldPluCharacteristicMustBeNotDefault();
                        }
                        else
                        {
                            // Перебор прочих вложенностей.
                            WsSqlPluNestingFkModel? pluNestingFkOther = pluNestingFksOther.Find(
                                item => item.BundleCount.Equals((short)characteristicXml.AttachmentsCount));
                            // Есть совпадение.
                            if (pluNestingFkOther is not null && pluNestingFkOther.IsExists)
                            {
                                pluNestingFkOther.IsMarked = characteristicXml.IsMarked;
                                // Снять флаг по-умолчанию, для всех остальных вложенностей ПЛУ.
                                pluNestingFkOther.IsDefault =
                                    pluNestingFkDefault.IsIdentityUid.Equals(pluNestingFkOther.IsIdentityUid);
                                // Сохранить связь вложенности и ПЛУ.
                                WsServiceUtilsSave.SavePluNestingFk(response, pluNestingFkOther);
                            }
                            else
                            {
                                pluNestingFkOther = pluNestingFkDefault.CloneCast();
                                //pluNestingFkOther.Box = pluNestingFkDefault.Box;
                                //pluNestingFkOther.PluBundle = pluNestingFkDefault.PluBundle;
                                pluNestingFkOther.BundleCount = (short)characteristicXml.AttachmentsCount;
                                // Сохранить связь вложенности и ПЛУ.
                                WsServiceUtilsSave.SavePluNestingFk(response, pluNestingFkOther);
                            }
                        }
                    }
                }
                // Исключение.
                if (characteristicXml.ParseResult.IsStatusError)
                    WsServiceUtilsResponse.AddResponseExceptionString(response, characteristicXml.Uid1C,
                        characteristicXml.ParseResult.Exception, characteristicXml.ParseResult.InnerException);
            }
        }, format, isDebug, sessionFactory);

#endregion
}