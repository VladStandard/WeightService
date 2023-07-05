// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Helpers;

/// <summary>
/// Веб-контроллер номенклатур.
/// </summary>
public sealed class WsServicePlusController : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    public WsServicePlusController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Загрузить номенклатуру и получить ответ.
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponsePlus(XElement xml, string format, bool isDebug, ISessionFactory sessionFactory) =>
        WsServiceUtilsResponse.NewResponse1CCore<WsResponse1CShortModel>(response =>
        {
            // Заполнить таблицу связей разрешённых для загрузки ПЛУ из 1С.
            WsServiceUtilsUpdate.FillPlus1CFksDb();
            // Обновить весь кэш.
            WsServiceUtils.ContextCache.Load();
            // Заполнить список ПЛУ из XML.
            List<WsXmlContentRecord<WsSqlPluModel>> plusXml = WsServiceUtilsGetXml.GetXmlPluList(xml);
            WsSqlPluValidator pluValidator = new(false, false);
            // Цикл по всем XML-номенклатурам.
            foreach (WsXmlContentRecord<WsSqlPluModel> record in plusXml)
            {
                WsSqlPluModel itemXml = record.Item;
                // Обновить таблицу связей ПЛУ для обмена.
                List<WsSqlPlu1CFkModel> plus1CFksDb = WsServiceUtilsUpdate.UpdatePlus1CFksDb(response, record);
                // Проверить разрешение обмена для ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess) WsServiceUtilsCheck.CheckEnabledPlu(itemXml, plus1CFksDb);

                // Сохранить клипсу.
                if (itemXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SaveClip(response, itemXml);
                // Сохранить коробку.
                if (itemXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SaveBox(response, itemXml);
                // Сохранить пакет.
                if (itemXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SaveBundle(response, itemXml);
                // Проверить корректность группы и номера ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess) WsServiceUtilsCheck.CheckCorrectPluNumberForNonGroup(itemXml);
                // Проверить валидацию ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess) WsServiceUtilsCheck.CheckPluValidation(itemXml, pluValidator);
                // Проверить дубликат номера ПЛУ для не групп.
                if (itemXml.ParseResult.IsStatusSuccess) WsServiceUtilsCheck.CheckPluDublicateForNonGroup(response, itemXml);
                // Сохранить ПЛУ в БД.
                if (itemXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SavePlu(response, itemXml);
                // Сохранить связь ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SavePluFks(response, itemXml);
                // Сохранить связь бренда.
                if (itemXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SavePluBrandFk(response, itemXml);
                // Сохранить связь клипсы ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SavePluClipFk(response, itemXml);
                // Сохранить связь пакета и ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                {
                    // Сохранить связь пакета и ПЛУ.
                    WsSqlPluBundleFkModel pluBundleFk = WsServiceUtilsSave.SavePluBundleFk(response, itemXml);
                    // Получить список связей вложенности ПЛУ.
                    List<WsSqlPluNestingFkModel> pluNestingFks = WsServiceUtilsGet.GetListPluNestingFks(
                        WsSqlEnumContextType.Cache, response, itemXml.Uid1C, itemXml.Uid1C, "Вложенности ПЛУ");
                    // Сохранить связь вложенности и ПЛУ.
                    if (itemXml.ParseResult.IsStatusSuccess)
                    {
                        WsSqlPluNestingFkModel pluNestingFkDb = WsServiceUtilsSave.SavePluNestingFk(response, pluBundleFk, itemXml);
                        // Перебор вложенностей.
                        if (itemXml.ParseResult.IsStatusSuccess)
                        {
                            foreach (WsSqlPluNestingFkModel pluNestingFk in pluNestingFks)
                            {
                                if (itemXml.ParseResult.IsStatusSuccess)
                                {
                                    // Если вложенность не найдена, то добавить новую.

                                    //if (!pluNestingFk.BundleCount.Equals(pluNestingFkDb.BundleCount) ||
                                    //    !pluNestingFk.Box.Uid1C.Equals(pluNestingFkDb.Box.Uid1C) ||
                                    //    !pluNestingFk.PluBundle.Bundle.Uid1C.Equals(pluNestingFkDb.PluBundle.Bundle.Uid1C))
                                    //{
                                    //    // Деактивировать.
                                    //    pluNestingFk.IsDefault = false;
                                    //    // Сохранить связь вложенности и ПЛУ.
                                    //    if (itemXml.ParseResult.IsStatusSuccess)
                                    //        WsServiceUtilsSave.SavePluNestingFk(response, pluNestingFk);
                                    //}
                                }
                            }
                        }
                    }
                }
                // Исключение.
                if (itemXml.ParseResult.IsStatusError)
                    WsServiceUtilsResponse.AddResponseExceptionString(response, itemXml.Uid1C,
                        itemXml.ParseResult.Exception, itemXml.ParseResult.InnerException);
            }
        }, format, isDebug, sessionFactory);

    #endregion
}