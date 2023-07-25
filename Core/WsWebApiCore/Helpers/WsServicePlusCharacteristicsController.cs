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
                WsSqlPluCharacteristicModel characteristicXml = recordXml.Item;
                // Получить список ПЛУ.
                List<WsSqlPluModel> plusDb = WsServiceUtils.ContextManager.PluRepository.GetListByUid1C(characteristicXml.NomenclatureGuid);
                if (!plusDb.Any())
                {
                    characteristicXml.ParseResult.Status = WsEnumParseStatus.Error;
                    characteristicXml.ParseResult.Exception = WsLocaleCore.WebService.PluNotFound();
                }
                else if (plusDb.Count > 1)
                {
                    characteristicXml.ParseResult.Status = WsEnumParseStatus.Error;
                    characteristicXml.ParseResult.Exception =
                        WsLocaleCore.WebService.PluFoundMoreThen1() + " | " +
                        string.Join(", ", plusDb.Select(x => x.Number));
                }
                if (characteristicXml.AttachmentsCount % 1 != 0)
                {
                    characteristicXml.ParseResult.Status = WsEnumParseStatus.Error;
                    characteristicXml.ParseResult.Exception =
                        WsLocaleCore.WebService.AttachmentsCountMustBeInt() + " | " +
                        string.Join(", ", plusDb.Select(x => x.Number));
                }
                if (characteristicXml.ParseResult.IsStatusSuccess)
                {
                    // Обновить таблицу связей ПЛУ для обмена.
                    List<WsSqlPlu1CFkModel> plus1CFksDb = WsServiceUtilsUpdate.UpdatePlus1CFksDb(response, recordXml);
                    // Проверить разрешение обмена для ПЛУ.
                    if (characteristicXml.ParseResult.IsStatusSuccess) WsServiceUtilsCheck.CheckEnabledPlu(characteristicXml, plus1CFksDb);
                }
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
                        // Вложенность является по-умолчанию.
                        if (pluNestingFkDefault.IsExists && pluNestingFkDefault.BundleCount.Equals((short)characteristicXml.AttachmentsCount))
                        {
                            characteristicXml.ParseResult.Status = WsEnumParseStatus.Error;
                            characteristicXml.ParseResult.Exception = WsLocaleCore.WebService.FieldPluCharacteristicMustBeNotDefault();
                        }
                        else
                        {
                            WsSqlPluModel pluDb = plusDb.First();
                            // Получить список вложенностей ПЛУ.
                            List<WsSqlPluNestingFkModel> pluNestingFks = WsServiceUtilsGet.GetListPluNestingFks(
                                WsSqlEnumContextType.Cache, response, pluDb.Uid1C, characteristicXml.NomenclatureGuid, "Вложенности ПЛУ");
                            // Отфильтровать список вложенностей ПЛУ.
                            List<WsSqlPluNestingFkModel> pluNestingFksOther =
                                pluNestingFks.Where(item => !item.IdentityValueUid.Equals(pluNestingFkDefault.IdentityValueUid)).ToList();
                            // Поиск вложенности.
                            WsSqlPluNestingFkModel? pluNestingFkOther = pluNestingFksOther.Find(
                                item => item.BundleCount.Equals((short)characteristicXml.AttachmentsCount));
                            // Найдена эта же вложенность.
                            if (pluNestingFkOther is not null)
                            {
                                pluNestingFkOther.IsMarked = characteristicXml.IsMarked;
                            }
                            // Вложенность не найдена -> создать.
                            else
                            {
                                // Создать копию из вложенности по-умолчанию.
                                pluNestingFkOther = new(pluNestingFkDefault)
                                {
                                    // Задать новое кол-во.
                                    BundleCount = (short)characteristicXml.AttachmentsCount,
                                    IdentityValueUid = Guid.Empty,
                                };
                            }
                            // Снять флаг по-умолчанию.
                            pluNestingFkOther.IsDefault = false;
                            // Сохранить связь вложенности и ПЛУ.
                            WsServiceUtilsSave.SavePluNestingFk(response, pluNestingFkOther);
                            // Обновить кэш.
                            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PlusNestingFks);
                            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.ViewPlusNesting);
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