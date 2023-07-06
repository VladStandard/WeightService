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
                WsSqlPluModel pluXml = record.Item;
                // Обновить таблицу связей ПЛУ для обмена.
                List<WsSqlPlu1CFkModel> plus1CFksDb = WsServiceUtilsUpdate.UpdatePlus1CFksDb(response, record);
                // Проверить разрешение обмена для ПЛУ.
                if (pluXml.ParseResult.IsStatusSuccess) WsServiceUtilsCheck.CheckEnabledPlu(pluXml, plus1CFksDb);

                // Сохранить клипсу.
                if (pluXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SaveClip(response, pluXml);
                // Сохранить коробку.
                if (pluXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SaveBox(response, pluXml);
                // Сохранить пакет.
                if (pluXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SaveBundle(response, pluXml);
                // Проверить корректность группы и номера ПЛУ.
                if (pluXml.ParseResult.IsStatusSuccess) WsServiceUtilsCheck.CheckCorrectPluNumberForNonGroup(pluXml);
                // Проверить валидацию ПЛУ.
                if (pluXml.ParseResult.IsStatusSuccess) WsServiceUtilsCheck.CheckPluValidation(pluXml, pluValidator);
                // Проверить дубликат номера ПЛУ для не групп.
                if (pluXml.ParseResult.IsStatusSuccess) WsServiceUtilsCheck.CheckPluDublicateForNonGroup(response, pluXml);
                // Сохранить ПЛУ в БД.
                if (pluXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SavePlu(response, pluXml);
                // Сохранить связь ПЛУ.
                if (pluXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SavePluFks(response, pluXml);
                // Сохранить связь бренда.
                if (pluXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SavePluBrandFk(response, pluXml);
                // Сохранить связь клипсы ПЛУ.
                if (pluXml.ParseResult.IsStatusSuccess) WsServiceUtilsSave.SavePluClipFk(response, pluXml);
                
                if (pluXml.ParseResult.IsStatusSuccess)
                {
                    // Сохранить связь пакета и ПЛУ.
                    WsSqlPluBundleFkModel pluBundleFk = WsServiceUtilsSave.SavePluBundleFk(response, pluXml);
                    if (pluXml.ParseResult.IsStatusSuccess)
                    {
                        // Сохранить вложенность ПЛУ по-умолчанию.
                        WsSqlPluNestingFkModel pluNestingFkDefault =
                            WsServiceUtilsSave.SavePluNestingFkDefault(response, pluBundleFk, pluXml);
                        // Сохранить остальные вложенности и ПЛУ.
                        if (pluXml.ParseResult.IsStatusSuccess)
                            WsServiceUtilsSave.SavePluNestingFkOther(response, pluNestingFkDefault, pluXml);
                    }
                }

                // Исключение.
                if (pluXml.ParseResult.IsStatusError)
                    WsServiceUtilsResponse.AddResponseExceptionString(response, pluXml.Uid1C,
                        pluXml.ParseResult.Exception, pluXml.ParseResult.InnerException);
            }
        }, format, isDebug, sessionFactory);

    #endregion
}