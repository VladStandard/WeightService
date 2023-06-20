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
    /// Получить список групп из XML.
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    private List<WsXmlContentRecord<WsSqlPluGroupModel>> GetXmlPluGroupsList(XElement xml) =>
        WsServiceContentUtils.GetNodesListCore<WsSqlPluGroupModel>(xml, WsLocaleCore.WebService.XmlItemNomenclatureGroup, (xmlNode, itemXml) =>
        {
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsGroup));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "AttachmentsCount");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "BoxTypeGuid");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "BrandGuid");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "CategoryGuid");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ClipTypeGuid");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "GroupGuid");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "PackageTypeGuid");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ParentGroupGuid");
        });

    /// <summary>
    /// Добавить связь группы в таблицу БД.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluGroupXml"></param>
    private void AddResponsePluGroupsFks(WsResponse1CShortModel response, WsSqlPluGroupModel pluGroupXml)
    {
        try
        {
            if (Equals(pluGroupXml.ParentGuid, Guid.Empty)) return;
            // Родитель.
            WsSqlPluGroupModel parent = new() { IdentityValueUid = pluGroupXml.ParentGuid };
            parent = SqlCore.GetItemNotNullable<WsSqlPluGroupModel>(parent.Identity);
            if (parent.IsNew)
            {
                WsServiceResponseUtils.AddResponseException(response, pluGroupXml.Uid1C, new($"Parent PLU group for '{pluGroupXml.ParentGuid}' {WsLocaleCore.WebService.IsNotFound}!"));
                return;
            }
            // Группа.
            WsSqlPluGroupModel pluGroup = new() { IdentityValueUid = pluGroupXml.IdentityValueUid };
            pluGroup = SqlCore.GetItemNotNullable<WsSqlPluGroupModel>(pluGroup.Identity);
            if (pluGroup.IsNew)
            {
                WsServiceResponseUtils.AddResponseException(response, pluGroupXml.Uid1C, new($"PLU group for '{pluGroupXml.ParentGuid}' {WsLocaleCore.WebService.IsNotFound}!"));
                return;
            }
            // Связь группы.
            WsSqlPluGroupFkModel itemGroupFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                PluGroup = pluGroup,
                Parent = parent
            };
            // Поиск по UID.
            WsSqlPluGroupFkModel? itemDb = ContextCache.PlusGroupsFks.Find(x =>
                x.PluGroup.IdentityValueUid.Equals(itemGroupFk.PluGroup.IdentityValueUid) &&
                x.Parent.IdentityValueUid.Equals(itemGroupFk.Parent.IdentityValueUid));
            if (itemDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUpdateUtils.UpdatePluGroupFkDb(response, pluGroupXml.Uid1C, itemGroupFk, itemDb, false);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUpdateUtils.SaveItemDb(response, itemGroupFk, false, pluGroupXml.Uid1C);
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.PluGroupsFks);
        }
        catch (Exception ex)
        {
            WsServiceResponseUtils.AddResponseException(response, pluGroupXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Добавить группу в таблицу БД.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluGroupXml"></param>
    private void AddResponsePluGroups(WsResponse1CShortModel response, WsSqlPluGroupModel pluGroupXml)
    {
        try
        {
            // Поиск по Uid1C.
            WsSqlPluGroupModel? pluGroupDb = ContextCache.PlusGroups.Find(item => Equals(item.Uid1C, pluGroupXml.IdentityValueUid));
            if (pluGroupDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUpdateUtils.UpdatePluGroupDb(response, pluGroupXml.Uid1C, pluGroupXml, pluGroupDb, true);
                return;
            }
            // Поиск по Code.
            pluGroupDb = ContextCache.PlusGroups.Find(item => Equals(item.Code, pluGroupXml.Code));
            if (pluGroupDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUpdateUtils.UpdatePluGroupDb(response, pluGroupXml.Uid1C, pluGroupXml, pluGroupDb, true);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUpdateUtils.SaveItemDb(response, pluGroupXml, true, pluGroupXml.Uid1C);
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.PluGroups);
        }
        catch (Exception ex)
        {
            WsServiceResponseUtils.AddResponseException(response, pluGroupXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Загрузить номенклатурные группы и получить ответ.
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponsePluGroups(XElement xml, string format, bool isDebug, ISessionFactory sessionFactory) =>
        WsServiceResponseUtils.NewResponse1CCore<WsResponse1CShortModel>(response =>
        {
            // Загрузить кэш.
            ContextCache.Load();
            // Получить список групп из XML.
            List<WsXmlContentRecord<WsSqlPluGroupModel>> itemsXml = GetXmlPluGroupsList(xml);
            foreach (WsXmlContentRecord<WsSqlPluGroupModel> record in itemsXml)
            {
                WsSqlPluGroupModel pluGroup = record.Item;
                switch (pluGroup.ParseResult.Status)
                {
                    case WsEnumParseStatus.Success:
                        // Добавить группу в таблицу БД.
                        AddResponsePluGroups(response, pluGroup);
                        // Добавить связь группы в таблицу БД.
                        AddResponsePluGroupsFks(response, pluGroup);
                        break;
                    case WsEnumParseStatus.Error:
                        WsServiceResponseUtils.AddResponseExceptionString(response, pluGroup.Uid1C,
                            pluGroup.ParseResult.Exception, pluGroup.ParseResult.InnerException);
                        break;
                }
            }
        }, format, isDebug, sessionFactory);

    #endregion
}