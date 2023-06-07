// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Controllers;

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

    private void AddResponsePluGroupsFks(WsResponse1CShortModel response, WsSqlPluGroupModel pluGroupXml)
    {
        try
        {
            if (Equals(pluGroupXml.ParentGuid, Guid.Empty)) return;

            WsSqlPluGroupModel parent = new() { IdentityValueUid = pluGroupXml.ParentGuid };
            parent = AccessManager.AccessItem.GetItemNotNullable<WsSqlPluGroupModel>(parent.Identity);
            if (parent.IsNew)
            {
                AddResponseException(response, pluGroupXml.Uid1C, new($"Parent PLU group for '{pluGroupXml.ParentGuid}' {WsLocaleCore.WebService.IsNotFound}!"));
                return;
            }
            WsSqlPluGroupModel pluGroup = new() { IdentityValueUid = pluGroupXml.IdentityValueUid };
            pluGroup = AccessManager.AccessItem.GetItemNotNullable<WsSqlPluGroupModel>(pluGroup.Identity);
            if (pluGroup.IsNew)
            {
                AddResponseException(response, pluGroupXml.Uid1C, new($"PLU group for '{pluGroupXml.ParentGuid}' {WsLocaleCore.WebService.IsNotFound}!"));
                return;
            }

            WsSqlPluGroupFkModel itemGroupFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                PluGroup = pluGroup,
                Parent = parent
            };

            // Найдено по Identity -> Обновить найденную запись.
            WsSqlPluGroupFkModel? itemDb = Cache.PluGroupsFks.Find(x =>
                x.PluGroup.IdentityValueUid.Equals(itemGroupFk.PluGroup.IdentityValueUid) &&
                x.Parent.IdentityValueUid.Equals(itemGroupFk.Parent.IdentityValueUid));
            if (UpdatePluGroupFkDb(response, pluGroupXml.Uid1C, itemGroupFk, itemDb, false)) return;

            // Не найдено -> Добавить новую запись.
            if (SaveItemDb(response, itemGroupFk, false, pluGroupXml.Uid1C))
                // Обновить список БД.
                Cache.Load(WsSqlTableName.PluGroupsFks);
        }
        catch (Exception ex)
        {
            AddResponseException(response, pluGroupXml.Uid1C, ex);
        }
    }

    private void AddResponsePluGroups(WsResponse1CShortModel response, WsSqlPluGroupModel pluGroupXml)
    {
        try
        {
            // Найдено по Uid1C -> Обновить найденную запись.
            WsSqlPluGroupModel? pluGroupDb = Cache.PluGroups.Find(item => Equals(item.Uid1C, pluGroupXml.IdentityValueUid));
            if (UpdatePluGroupDb(response, pluGroupXml.Uid1C, pluGroupXml, pluGroupDb, true)) return;

            // Найдено по Code -> Обновить найденную запись.
            pluGroupDb = Cache.PluGroups.Find(item => Equals(item.Code, pluGroupXml.Code));
            if (UpdatePluGroupDb(response, pluGroupXml.Uid1C, pluGroupXml, pluGroupDb, true)) return;

            // Найдено по Name -> Обновить найденную запись.
            pluGroupDb = Cache.PluGroups.Find(item => Equals(item.Name, pluGroupXml.Name));
            if (UpdatePluGroupDb(response, pluGroupXml.Uid1C, pluGroupXml, pluGroupDb, true)) return;

            // Не найдено -> Добавить новую запись.
            if (SaveItemDb(response, pluGroupXml, true))
                // Обновить список БД.
                Cache.Load(WsSqlTableName.PluGroups);
        }
        catch (Exception ex)
        {
            AddResponseException(response, pluGroupXml.Uid1C, ex);
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
        NewResponse1CCore<WsResponse1CShortModel>(response =>
        {
            // Прогреть кэш.
            Cache.Load();
            List<WsXmlContentRecord<WsSqlPluGroupModel>> itemsXml = GetXmlPluGroupsList(xml);
            foreach (WsXmlContentRecord<WsSqlPluGroupModel> record in itemsXml)
            {
                WsSqlPluGroupModel pluGroup = record.Item;
                switch (pluGroup.ParseResult.Status)
                {
                    case WsEnumParseStatus.Success:
                        AddResponsePluGroups(response, pluGroup);
                        AddResponsePluGroupsFks(response, pluGroup);
                        break;
                    case WsEnumParseStatus.Error:
                        AddResponseExceptionString(response, pluGroup.Uid1C,
                            pluGroup.ParseResult.Exception, pluGroup.ParseResult.InnerException);
                        break;
                }
            }
        }, format, isDebug, sessionFactory);

    #endregion
}