// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Helpers;

/// <summary>
/// Веб-контроллер номенклатурных групп.
/// </summary>
public sealed class WsServicePlusGroupsController : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    public WsServicePlusGroupsController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Загрузить номенклатурные группы и получить ответ.
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponsePluGroups(XElement xml, string format, bool isDebug, ISessionFactory sessionFactory) =>
        WsServiceUtilsResponse.NewResponse1CCore<WsResponse1CShortModel>(response =>
        {
            // Загрузить кэш.
            WsServiceUtils.ContextCache.Load();
            // Получить список групп из XML.
            List<WsXmlContentRecord<WsSqlPluGroupModel>> itemsXml = WsServiceUtilsGetXml.GetXmlPluGroupsList(xml);
            foreach (WsXmlContentRecord<WsSqlPluGroupModel> record in itemsXml)
            {
                WsSqlPluGroupModel pluGroup = record.Item;
                switch (pluGroup.ParseResult.Status)
                {
                    case WsEnumParseStatus.Success:
                        // Добавить группу в таблицу БД.
                        WsServiceUtilsSave.SavePluGroups(response, pluGroup);
                        // Добавить связь группы в таблицу БД.
                        WsServiceUtilsSave.SavePluGroupsFks(response, pluGroup);
                        break;
                    case WsEnumParseStatus.Error:
                        WsServiceUtilsResponse.AddResponseExceptionString(response, pluGroup.Uid1C,
                            pluGroup.ParseResult.Exception, pluGroup.ParseResult.InnerException);
                        break;
                }
            }
        }, format, isDebug, sessionFactory);

    #endregion
}