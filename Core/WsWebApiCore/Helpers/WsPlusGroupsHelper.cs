// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsWebApiCore.Helpers;

public sealed class WsPlusGroupsHelper : WsContentBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsPlusGroupsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsPlusGroupsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    internal WsPlusGroupsHelper(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    private List<WsXmlContentRecord<PluGroupModel>> GetXmlPluGroupsList(XElement xml) =>
        WsContentUtils.GetNodesListCore<PluGroupModel>(xml, LocaleCore.WebService.XmlItemNomenclatureGroup, (xmlNode, itemXml) =>
        {
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsGroup));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "AttachmentsCount");
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "BoxTypeGuid");
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "BrandGuid");
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "CategoryGuid");
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ClipTypeGuid");
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "GroupGuid");
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "PackageTypeGuid");
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ParentGroupGuid");
        });

    private void AddResponse1cPluGroupsFks(WsResponse1cShortModel response, List<PluGroupFkModel> itemsDb,
        PluGroupModel pluGroupXml)
    {
        try
        {
            if (Equals(pluGroupXml.ParentGuid, Guid.Empty)) return;

            PluGroupModel parent = new() { IdentityValueUid = pluGroupXml.ParentGuid };
            parent = AccessManager.AccessItem.GetItemNotNullable<PluGroupModel>(parent.Identity);
            if (parent.IsNew)
            {
                AddResponse1cException(response, pluGroupXml.Uid1c, new($"Parent PLU group for '{pluGroupXml.ParentGuid}' {LocaleCore.WebService.IsNotFound}!"));
                return;
            }
            PluGroupModel pluGroup = new() { IdentityValueUid = pluGroupXml.IdentityValueUid };
            pluGroup = AccessManager.AccessItem.GetItemNotNullable<PluGroupModel>(pluGroup.Identity);
            if (pluGroup.IsNew)
            {
                AddResponse1cException(response, pluGroupXml.Uid1c, new($"PLU group for '{pluGroupXml.ParentGuid}' {LocaleCore.WebService.IsNotFound}!"));
                return;
            }

            PluGroupFkModel itemGroupFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                PluGroup = pluGroup,
                Parent = parent
            };

            // Найдено по Identity -> Обновить найденную запись.
            PluGroupFkModel? itemDb = itemsDb.Find(x =>
                x.PluGroup.IdentityValueUid.Equals(itemGroupFk.PluGroup.IdentityValueUid) &&
                x.Parent.IdentityValueUid.Equals(itemGroupFk.Parent.IdentityValueUid));
            if (UpdatePluGroupFkDb(response, pluGroupXml.Uid1c, itemGroupFk, itemDb, false)) return;

            // Не найдено -> Добавить новую запись.
            bool isSave = SaveItemDb(response, itemGroupFk, false, pluGroupXml.Uid1c);

            // Обновить список БД.
            if (isSave && !itemsDb.Select(x => x.IdentityValueUid).Contains(itemGroupFk.IdentityValueUid))
                itemsDb.Add(itemGroupFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluGroupXml.Uid1c, ex);
        }
    }

    private void AddResponse1cPluGroups(WsResponse1cShortModel response, List<PluGroupModel> pluGroupsDb, PluGroupModel pluGroupXml)
    {
        try
        {
            // Найдено по Uid1C -> Обновить найденную запись.
            PluGroupModel? pluGroupDb = pluGroupsDb.Find(item => Equals(item.Uid1c, pluGroupXml.IdentityValueUid));
            if (UpdatePluGroupDb(response, pluGroupXml.Uid1c, pluGroupXml, pluGroupDb, true)) return;

            // Найдено по Code -> Обновить найденную запись.
            pluGroupDb = pluGroupsDb.Find(item => Equals(item.Code, pluGroupXml.Code));
            if (UpdatePluGroupDb(response, pluGroupXml.Uid1c, pluGroupXml, pluGroupDb, true)) return;

            // Найдено по Name -> Обновить найденную запись.
            pluGroupDb = pluGroupsDb.Find(item => Equals(item.Name, pluGroupXml.Name));
            if (UpdatePluGroupDb(response, pluGroupXml.Uid1c, pluGroupXml, pluGroupDb, true)) return;

            // Не найдено -> Добавить новую запись.
            bool isSave = SaveItemDb(response, pluGroupXml, true);

            // Обновить список БД.
            if (pluGroupDb is not null && isSave && !pluGroupsDb.Select(x => x.IdentityValueUid).Contains(pluGroupDb.IdentityValueUid))
                pluGroupsDb.Add(pluGroupDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluGroupXml.Uid1c, ex);
        }
    }

    /// <summary>
    /// Отправить номенклатурные группы и получить ответ.
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponse1cPluGroups(XElement xml, string format, bool isDebug, ISessionFactory sessionFactory) =>
        NewResponse1cCore<WsResponse1cShortModel>(response =>
        {
            // Прогреть кеш.
            Cache.Load();
            List<WsXmlContentRecord<PluGroupModel>> itemsXml = GetXmlPluGroupsList(xml);
            PluGroupModel pluGroup;
            foreach (WsXmlContentRecord<PluGroupModel> record in itemsXml)
            {
                pluGroup = record.Item;
                switch (pluGroup.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cPluGroups(response, Cache.PluGroupsDb, pluGroup);
                        AddResponse1cPluGroupsFks(response, Cache.PluGroupsFksDb, pluGroup);
                        break;
                    case ParseStatus.Error:
                        AddResponse1cException(response, pluGroup.Uid1c, pluGroup.ParseResult.Exception, pluGroup.ParseResult.InnerException);
                        break;
                }
            }
        }, format, isDebug, sessionFactory);

    #endregion
}