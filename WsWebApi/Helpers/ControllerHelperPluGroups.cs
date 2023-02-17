// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusGroupsFks;
using DataCore.Sql.TableScaleModels.PlusGroups;

namespace WsWebApi.Helpers;

public partial class ControllerHelper
{
    #region Public and private methods

    private List<PluGroupModel> GetXmlPluGroupsList(XElement xml) =>
        GetNodesListCore<PluGroupModel>(xml, LocaleCore.WebService.XmlItemNomenclatureGroup, (xmlNode, itemXml) =>
        {
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsGroup));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "AttachmentsCount");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "BoxTypeGuid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "BrandGuid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "CategoryGuid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ClipTypeGuid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "GroupGuid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "PackageTypeGuid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ParentGroupGuid");
        });

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPluGroupsFks(Response1cShortModel response, List<PluGroupFkModel> itemsDb, 
        PluGroupModel pluGroupXml)
    {
        try
        {
            if (Equals(pluGroupXml.ParentGuid, Guid.Empty)) return;

            PluGroupModel parent = new() { IdentityValueUid = pluGroupXml.ParentGuid };
            parent = DataContext.GetItemNotNullable<PluGroupModel>(parent.Identity);
            if (parent.IsNew)
            {
                AddResponse1cException(response, pluGroupXml.Uid1c, new($"Parent PLU group for '{pluGroupXml.ParentGuid}' {LocaleCore.WebService.IsNotFound}!"));
                return;
            }
            PluGroupModel pluGroup = new() { IdentityValueUid = pluGroupXml.IdentityValueUid };
            pluGroup = DataContext.GetItemNotNullable<PluGroupModel>(pluGroup.Identity);
            if (pluGroup.IsNew)
            {
                AddResponse1cException(response, pluGroupXml.Uid1c, new($"PLU group for '{pluGroupXml.ParentGuid}' {LocaleCore.WebService.IsNotFound}!"));
                return;
            }
            
            PluGroupFkModel itemGroupFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = null,
                PluGroup = pluGroup,
                Parent = parent
            };

            // Find by Identity -> Update exists.
            PluGroupFkModel? itemDb = itemsDb.Find(x => 
                x.PluGroup.IdentityValueUid.Equals(itemGroupFk.PluGroup.IdentityValueUid) &&
                x.Parent.IdentityValueUid.Equals(itemGroupFk.Parent.IdentityValueUid));
            if (UpdateItemDb(response, pluGroupXml.Uid1c, itemGroupFk, itemDb, false)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluGroupXml.Uid1c, itemGroupFk, false);

            // Update db list.
            if (isSave && !itemsDb.Select(x => x.IdentityValueUid).Contains(itemGroupFk.IdentityValueUid))
                itemsDb.Add(itemGroupFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluGroupXml.Uid1c, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPluGroups(Response1cShortModel response, List<PluGroupModel> pluGroupsDb, PluGroupModel pluGroupXml)
    {
        try
        {
            // Find by Uid1C -> Update exists.
            PluGroupModel? pluGroupDb = pluGroupsDb.Find(item => Equals(item.Uid1c, pluGroupXml.IdentityValueUid));
            if (UpdateItemDb(response, pluGroupXml.Uid1c, pluGroupXml, pluGroupDb, true)) return;

            // Find by Code -> Update exists.
            pluGroupDb = pluGroupsDb.Find(item => Equals(item.Code, pluGroupXml.Code));
            if (UpdateItemDb(response, pluGroupXml.Uid1c, pluGroupXml, pluGroupDb, true)) return;

            // Find by Name -> Update exists.
            pluGroupDb = pluGroupsDb.Find(item => Equals(item.Name, pluGroupXml.Name));
            if (UpdateItemDb(response, pluGroupXml.Uid1c, pluGroupXml, pluGroupDb, true)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluGroupXml.Uid1c, pluGroupXml, true);

            // Update db list.
            if (pluGroupDb is not null && isSave && !pluGroupsDb.Select(x => x.IdentityValueUid).Contains(pluGroupDb.IdentityValueUid))
                pluGroupsDb.Add(pluGroupDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluGroupXml.Uid1c, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public ContentResult NewResponse1cPluGroups(ISessionFactory sessionFactory, XElement xml, string format) =>
        NewResponse1cCore<Response1cShortModel>(sessionFactory, response =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<PluGroupModel> itemsDb = DataContext.GetListNotNullable<PluGroupModel>(sqlCrudConfig);
            List<PluGroupFkModel> pluGroupsFksDb = DataContext.GetListNotNullable<PluGroupFkModel>(sqlCrudConfig);
            List<PluGroupModel> itemsXml = GetXmlPluGroupsList(xml);
            foreach (PluGroupModel itemXml in itemsXml)
            {
                switch (itemXml.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cPluGroups(response, itemsDb, itemXml);
                        AddResponse1cPluGroupsFks(response, pluGroupsFksDb, itemXml);
                        break;
                    case ParseStatus.Error:
                        AddResponse1cException(response, itemXml.Uid1c, itemXml.ParseResult.Exception, itemXml.ParseResult.InnerException);
                        break;
                }
            }
        }, format, false);

    #endregion
}