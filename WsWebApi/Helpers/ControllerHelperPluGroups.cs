// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusGroupsFks;
using DataCore.Sql.TableScaleModels.PlusGroups;

namespace WsWebApi.Helpers;

public partial class ControllerHelper
{
    #region Public and private methods

    private List<PluGroupModel> GetPluGroupsList(XElement xml) =>
        GetNodesListCore<PluGroupModel>(xml, "NomenclatureGroup", (xmlNode, itemXml) =>
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
    private void AddResponse1cPluGroupsFks(Response1cShortModel response, List<PluGroupFkModel> itemsDb, PluGroupModel itemXml)
    {
        try
        {
            if (Equals(itemXml.GroupGuid, Guid.Empty)) return;

            PluGroupModel parent = new() { IdentityValueUid = itemXml.GroupGuid };
            parent = DataContext.GetItemNotNullable<PluGroupModel>(parent.Identity);
            if (parent.IsNew)
            {
                AddResponse1cException(response, itemXml.IdentityValueUid,
                    new($"Parent PLU group for '{itemXml.GroupGuid}' is not found!"));
                return;
            }
            PluGroupModel pluGroup = new() { IdentityValueUid = itemXml.IdentityValueUid };
            pluGroup = DataContext.GetItemNotNullable<PluGroupModel>(pluGroup.Identity);
            if (pluGroup.IsNew)
            {
                AddResponse1cException(response, itemXml.IdentityValueUid,
                    new($"PLU group for '{itemXml.GroupGuid}' is not found!"));
                return;
            }
            PluGroupFkModel itemGroupFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                PluGroup = pluGroup,
                Parent = parent
            };

            // Find by Identity -> Update exists.
            PluGroupFkModel? itemDb = itemsDb.Find(x => 
                x.PluGroup.IdentityValueUid.Equals(itemGroupFk.PluGroup.IdentityValueUid) &&
                x.Parent.IdentityValueUid.Equals(itemGroupFk.Parent.IdentityValueUid));
            if (UpdateItemDb(response, itemGroupFk, itemDb, false, false)) return;

            // Not find -> Add new.
            SaveItemDb(response, itemGroupFk, false);

            // Update db list.
            if (!itemsDb.Select(x => x.IdentityValueUid).Contains(itemGroupFk.IdentityValueUid))
                itemsDb.Add(itemGroupFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, itemXml.IdentityValueUid, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPluGroups(Response1cShortModel response, List<PluGroupModel> itemsDb, PluGroupModel itemXml)
    {
        try
        {
            // Find by Identity -> Update exists.
            PluGroupModel? itemDb = itemsDb.Find(x => x.IdentityValueUid.Equals(itemXml.IdentityValueUid));
            if (UpdateItemDb(response, itemXml, itemDb, false, true)) return;

            // Find by Code -> Update exists.
            itemDb = itemsDb.Find(x => x.Code.Equals(itemXml.Code));
            if (UpdateItemDb(response, itemXml, itemDb, true, true)) return;

            // Not find -> Add new.
            SaveItemDb(response, itemXml, true);

            // Update db list.
            if (!itemsDb.Select(x => x.IdentityValueUid).Contains(itemXml.IdentityValueUid))
                itemsDb.Add(itemXml);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, itemXml.IdentityValueUid, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public ContentResult NewResponse1cPluGroups(ISessionFactory sessionFactory, XElement xml, string format) =>
        NewResponse1cCore<Response1cShortModel>(sessionFactory, response =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<PluGroupModel> itemsDb = DataContext.GetListNotNullable<PluGroupModel>(sqlCrudConfig);
            List<PluGroupFkModel> pluGroupsFksDb = DataContext.GetListNotNullable<PluGroupFkModel>(sqlCrudConfig);
            List<PluGroupModel> itemsXml = GetPluGroupsList(xml);
            foreach (PluGroupModel itemXml in itemsXml)
            {
                switch (itemXml.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cPluGroups(response, itemsDb, itemXml);
                        AddResponse1cPluGroupsFks(response, pluGroupsFksDb, itemXml);
                        break;
                    case ParseStatus.Error:
                        AddResponse1cException(response, itemXml.IdentityValueUid,
                            itemXml.ParseResult.Exception, itemXml.ParseResult.InnerException);
                        break;
                }
            }
        }, format, false);

    #endregion
}