// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusFks;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Clips;
using DevExpress.Data.Browsing;

namespace WsWebApi.Helpers;

public partial class ControllerHelper
{
    #region Public and private methods

    private List<PluModel> GetXmlPluList(XElement xml) =>
        GetNodesListCore<PluModel>(xml, "Nomenclature", (xmlNode, itemXml) =>
        {
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsGroup));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.FullName));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Description));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsCheckWeight));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "MeasurementType");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "PluNumber");
        });

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPlusFks(Response1cShortModel response, List<PluFkModel> itemsDb, PluModel itemXml)
    {
        try
        {
            if (Equals(itemXml.ParentGuid, Guid.Empty)) return;

            PluModel parent = new() { IdentityValueUid = itemXml.ParentGuid };
            parent = DataContext.GetItemNotNullable<PluModel>(parent.Identity);
            if (parent.IsNew)
            {
                AddResponse1cException(response, itemXml.IdentityValueUid,
                    new($"Parent PLU group for '{itemXml.ParentGuid}' is not found!"));
                return;
            }
            PluModel plu = new() { IdentityValueUid = itemXml.IdentityValueUid };
            plu = DataContext.GetItemNotNullable<PluModel>(plu.Identity);
            if (plu.IsNew)
            {
                AddResponse1cException(response, itemXml.IdentityValueUid,
                    new($"PLU group for '{itemXml.ParentGuid}' is not found!"));
                return;
            }
            PluFkModel itemFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = plu,
                Parent = parent
            };

            // Find by Identity -> Update exists.
            PluFkModel? itemDb = itemsDb.Find(x =>
                x.Plu.IdentityValueUid.Equals(itemFk.Plu.IdentityValueUid) &&
                x.Parent.IdentityValueUid.Equals(itemFk.Parent.IdentityValueUid));
            if (UpdateItemDb(response, itemFk, itemDb, false, false)) return;

            // Not find -> Add new.
            SaveItemDb(response, itemFk, false);

            // Update db list.
            if (!itemsDb.Select(x => x.IdentityValueUid).Contains(itemFk.IdentityValueUid))
                itemsDb.Add(itemFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, itemXml.IdentityValueUid, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cBoxes(Response1cShortModel response, PluModel itemXml)
    {
        try
        {
            if (Equals(itemXml.BoxTypeGuid, Guid.Empty)) return;

            // Find by Identity -> Save new or update exists.
            BoxModel box = new() { IdentityValueUid = itemXml.BoxTypeGuid };
            box = DataContext.GetItemNotNullable<BoxModel>(box.Identity);
            box.Name = itemXml.BoxTypeName;
            box.Weight = itemXml.BoxTypeWeight;
            if (box.IsNew)
                DataContext.DataAccess.Save(box);
            else
                DataContext.DataAccess.UpdateForce(box);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, itemXml.IdentityValueUid, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPlus(Response1cShortModel response, List<PluModel> itemsDb, PluModel itemXml)
    {
        try
        {
            // Add or update Foreign keys.
            // Must be refactoring!
            itemXml.Nomenclature = DataAccessHelper.Instance.GetItemNewEmpty<NomenclatureModel>();

            // Find by Identity -> Update exists.
            PluModel? itemDb = itemsDb.Find(x => x.IdentityValueUid.Equals(itemXml.IdentityValueUid));
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
    public ContentResult NewResponse1cPlus(ISessionFactory sessionFactory, XElement xml, string format) =>
        NewResponse1cCore<Response1cShortModel>(sessionFactory, response =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<PluModel> itemsDb = DataContext.GetListNotNullable<PluModel>(sqlCrudConfig);
            List<PluFkModel> pluFksDb = DataContext.GetListNotNullable<PluFkModel>(sqlCrudConfig);
            List<BoxModel> boxesDb = DataContext.GetListNotNullable<BoxModel>(sqlCrudConfig);
            List<BundleModel> bundlesDb = DataContext.GetListNotNullable<BundleModel>(sqlCrudConfig);
            List<ClipModel> clipsDb = DataContext.GetListNotNullable<ClipModel>(sqlCrudConfig);
            List<PluModel> itemsXml = GetXmlPluList(xml);
            foreach (PluModel itemXml in itemsXml)
            {
                AddResponse1cBoxes(response, itemXml);
                //AddResponse1cBundles(response, pluFksDb, itemXml);
                //AddResponse1cClips(response, pluFksDb, itemXml);
                switch (itemXml.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cPlus(response, itemsDb, itemXml);
                        AddResponse1cPlusFks(response, pluFksDb, itemXml);
                        //AddResponse1cPlusBoxesFks(response, pluFksDb, itemXml);
                        //AddResponse1cPlusBundlesFks(response, pluFksDb, itemXml);
                        //AddResponse1cPlusNestingFks(response, pluFksDb, itemXml);
                        //AddResponse1cPlusClipsFks(response, pluFksDb, itemXml);
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