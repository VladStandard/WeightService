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

    private List<WsXmlContentRecord<PluGroupModel>> GetXmlPluGroupsList(XElement xml) =>
        WsServiceContentUtils.GetNodesListCore<PluGroupModel>(xml, LocaleCore.WebService.XmlItemNomenclatureGroup, (xmlNode, itemXml) =>
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

    private void AddResponse1cPluGroupsFks(WsResponse1CShortModel response, PluGroupModel pluGroupXml)
    {
        try
        {
            if (Equals(pluGroupXml.ParentGuid, Guid.Empty)) return;

            PluGroupModel parent = new() { IdentityValueUid = pluGroupXml.ParentGuid };
            parent = AccessManager.AccessItem.GetItemNotNullable<PluGroupModel>(parent.Identity);
            if (parent.IsNew)
            {
                AddResponse1CException(response, pluGroupXml.Uid1C, new($"Parent PLU group for '{pluGroupXml.ParentGuid}' {LocaleCore.WebService.IsNotFound}!"));
                return;
            }
            PluGroupModel pluGroup = new() { IdentityValueUid = pluGroupXml.IdentityValueUid };
            pluGroup = AccessManager.AccessItem.GetItemNotNullable<PluGroupModel>(pluGroup.Identity);
            if (pluGroup.IsNew)
            {
                AddResponse1CException(response, pluGroupXml.Uid1C, new($"PLU group for '{pluGroupXml.ParentGuid}' {LocaleCore.WebService.IsNotFound}!"));
                return;
            }

            PluGroupFkModel itemGroupFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                PluGroup = pluGroup,
                Parent = parent
            };

            // Найдено по Identity -> Обновить найденную запись.
            PluGroupFkModel? itemDb = Cache.PluGroupsFksDb.Find(x =>
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
            AddResponse1CException(response, pluGroupXml.Uid1C, ex);
        }
    }

    private void AddResponse1cPluGroups(WsResponse1CShortModel response, PluGroupModel pluGroupXml)
    {
        try
        {
            // Найдено по Uid1C -> Обновить найденную запись.
            PluGroupModel? pluGroupDb = Cache.PluGroupsDb.Find(item => Equals(item.Uid1C, pluGroupXml.IdentityValueUid));
            if (UpdatePluGroupDb(response, pluGroupXml.Uid1C, pluGroupXml, pluGroupDb, true)) return;

            // Найдено по Code -> Обновить найденную запись.
            pluGroupDb = Cache.PluGroupsDb.Find(item => Equals(item.Code, pluGroupXml.Code));
            if (UpdatePluGroupDb(response, pluGroupXml.Uid1C, pluGroupXml, pluGroupDb, true)) return;

            // Найдено по Name -> Обновить найденную запись.
            pluGroupDb = Cache.PluGroupsDb.Find(item => Equals(item.Name, pluGroupXml.Name));
            if (UpdatePluGroupDb(response, pluGroupXml.Uid1C, pluGroupXml, pluGroupDb, true)) return;

            // Не найдено -> Добавить новую запись.
            if (SaveItemDb(response, pluGroupXml, true))
                // Обновить список БД.
                Cache.Load(WsSqlTableName.PluGroups);
        }
        catch (Exception ex)
        {
            AddResponse1CException(response, pluGroupXml.Uid1C, ex);
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
    public ContentResult NewResponse1cPluGroups(XElement xml, string format, bool isDebug, ISessionFactory sessionFactory) =>
        NewResponse1CCore<WsResponse1CShortModel>(response =>
        {
            // Прогреть кэш.
            Cache.Load();
            List<WsXmlContentRecord<PluGroupModel>> itemsXml = GetXmlPluGroupsList(xml);
            foreach (WsXmlContentRecord<PluGroupModel> record in itemsXml)
            {
                PluGroupModel pluGroup = record.Item;
                switch (pluGroup.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cPluGroups(response, pluGroup);
                        AddResponse1cPluGroupsFks(response, pluGroup);
                        break;
                    case ParseStatus.Error:
                        AddResponse1CExceptionString(response, pluGroup.Uid1C,
                            pluGroup.ParseResult.Exception, pluGroup.ParseResult.InnerException);
                        break;
                }
            }
        }, format, isDebug, sessionFactory);

    #endregion
}