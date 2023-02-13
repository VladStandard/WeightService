// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Clips;
using FluentValidation.Results;

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
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ShelfLife");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "PluNumber");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ParentGroupGuid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "BrandGuid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "BoxTypeGuid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "BoxTypeName");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "BoxTypeWeight");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "PackageTypeGuid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "PackageTypeName");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "PackageTypeWeight");
        });

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPlus(Response1cShortModel response, List<PluModel> plusDb, PluModel pluXml)
    {
        try
        {
            // Check Uid1C.
            if (Equals(pluXml.IdentityValueUid, Guid.Empty))
            {
                AddResponse1cException(response, pluXml.IdentityValueUid, "Empty GUID!", "");
                return;
            }

            // Find by Uid1C -> Update exists.
            PluModel? pluDb = plusDb.Find(item => Equals(item.Uid1C, pluXml.IdentityValueUid));
            if (UpdateItemDb(response, pluXml, pluDb, true)) return;

            // Find by Code -> Update exists.
            pluDb = plusDb.Find(item => Equals(item.Code, pluXml.Code));
            if (UpdateItemDb(response, pluXml, pluDb, true)) return;

            // Find by Number -> Update exists.
            pluDb = plusDb.Find(item => Equals(item.Number, pluXml.Number));
            if (UpdateItemDb(response, pluXml, pluDb, true)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluXml, true);

            // Update db list.
            if (pluDb is not null && isSave && !plusDb.Select(x => x.IdentityValueUid).Contains(pluDb.IdentityValueUid))
                plusDb.Add(pluDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.IdentityValueUid, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPlusFks(Response1cShortModel response, List<PluFkModel> pluFksDb, PluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.ParentGuid, Guid.Empty)) return;

            if (!GetPluFkPluDb(response, pluXml, pluXml.IdentityValueUid, "PLU", false, out PluModel? plu)) return;
            if (!GetPluFkPluDb(response, pluXml, pluXml.ParentGuid, "Parent PLU", true, out PluModel? parent)) return;
            if (!GetPluFkPluDb(response, pluXml, pluXml.CategoryGuid, "Category PLU", true, out PluModel? category)) return;
            if (plu is null || parent is null) return;

            PluFkModel pluFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = plu,
                Parent = parent,
                Category = category
            };

            // Find by Identity -> Update exists.
            PluFkModel? pluFkDb = pluFksDb.Find(item =>
                Equals(item.Plu.Uid1C, pluFk.Plu.Uid1C) &&
                Equals(item.Parent.Uid1C, pluFk.Parent.Uid1C) &&
                Equals(item.Category?.Uid1C, pluFk.Category?.Uid1C));
            if (UpdateItemDb(response, pluFk, pluFkDb, false)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluFk, false);

            // Update db list.
            if (isSave && !pluFksDb.Select(x => x.IdentityValueUid).Contains(pluFk.IdentityValueUid))
                pluFksDb.Add(pluFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.IdentityValueUid, ex);
        }
    }

    private bool GetPluFkPluDb(Response1cShortModel response, PluModel pluXml, Guid uid, string refName, bool isCheckGroup, out PluModel? plu)
    {
        plu = null;
        if (!Equals(uid, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                { new(nameof(SqlTableBase1c.Uid1C), SqlFieldComparerEnum.Equal, uid) }, 
                true, false, false, false);
            plu = DataContext.DataAccess.GetItemNullable<PluModel>(sqlCrudConfig);
            if (!isCheckGroup)
            {
                if (plu is null || plu.IsNew)
                {
                    AddResponse1cException(response, pluXml.IdentityValueUid, new($"{refName} with '{uid}' is not found!"));
                    return false;
                }
                return true;
            }
            // isCheckGroup.
            if (plu is null || plu.IsNew || !plu.IsGroup)
            {
                AddResponse1cException(response, pluXml.IdentityValueUid, new($"{refName} with '{uid}' is not found!"));
                return false;
            }
            return true;
        }
        return false;
    }

    private bool GetPluBundleFkBundleDb(Response1cShortModel response, PluModel pluXml, Guid uid, string refName, out BundleModel? bundle)
    {
        bundle = null;
        if (!Equals(uid, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new(nameof(SqlTableBase1c.Uid1C), SqlFieldComparerEnum.Equal, uid) },
                true, false, false, false);
            bundle = DataContext.DataAccess.GetItemNullable<BundleModel>(sqlCrudConfig);
            if (bundle is null || bundle.IsNew)
            {
                AddResponse1cException(response, pluXml.IdentityValueUid, new($"{refName} with '{uid}' is not found!"));
                return false;
            }
            return true;
        }
        return false;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPlusBoxes(Response1cShortModel response, List<BoxModel> boxesDb, PluModel pluXml)
    {
        try
        {
            // Check Uid1C.
            if (Equals(pluXml.BoxTypeGuid, Guid.Empty) && !string.IsNullOrEmpty(pluXml.BoxTypeName))
            {
                AddResponse1cException(response, pluXml.IdentityValueUid, $"Empty {nameof(pluXml.BoxTypeGuid)}!", "");
                return;
            }

            // Find by Uid1C -> Update exists.
            BoxModel? boxDb = boxesDb.Find(item => Equals(item.Uid1C, pluXml.BoxTypeGuid));
            if (UpdateBoxDb(response, pluXml, boxDb, false)) return;

            // Find by Name -> Update exists.
            boxDb = boxesDb.Find(item => Equals(item.Name, pluXml.BoxTypeName));
            if (UpdateBoxDb(response, pluXml, boxDb, false)) return;

            // Not find -> Add new.
            boxDb = new();
            boxDb.UpdateProperties(pluXml);
            bool isSave = SaveItemDb(response, boxDb, false);

            // Update db list.
            if (isSave && !boxesDb.Select(x => x.IdentityValueUid).Contains(boxDb.IdentityValueUid))
                boxesDb.Add(boxDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.IdentityValueUid, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPlusBundles(Response1cShortModel response, List<BundleModel> bundlesDb, PluModel pluXml)
    {
        try
        {
            // Check Uid1C.
            if (Equals(pluXml.PackageTypeGuid, Guid.Empty) && !string.IsNullOrEmpty(pluXml.PackageTypeName))
            {
                AddResponse1cException(response, pluXml.IdentityValueUid, $"Empty {nameof(pluXml.PackageTypeGuid)}!", "");
                return;
            }

            // Find by Uid1C -> Update exists.
            BundleModel? bundleDb = bundlesDb.Find(item => Equals(item.Uid1C, pluXml.PackageTypeGuid));
            if (UpdateBundleDb(response, pluXml, bundleDb, false)) return;

            // Find by Name -> Update exists.
            bundleDb = bundlesDb.Find(item => Equals(item.Name, pluXml.PackageTypeName));
            if (UpdateBundleDb(response, pluXml, bundleDb, false)) return;

            // Not find -> Add new.
            bundleDb = new();
            bundleDb.UpdateProperties(pluXml);
            bool isSave = SaveItemDb(response, bundleDb, false);

            // Update db list.
            if (isSave && !bundlesDb.Select(x => x.IdentityValueUid).Contains(bundleDb.IdentityValueUid))
                bundlesDb.Add(bundleDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.IdentityValueUid, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPlusBundlesFks(Response1cShortModel response, List<PluBundleFkModel> pluBbundlesFksDb, PluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.PackageTypeGuid, Guid.Empty)) return;

            if (!GetPluFkPluDb(response, pluXml, pluXml.IdentityValueUid, "PLU", false, out PluModel? plu)) return;
            if (!GetPluBundleFkBundleDb(response, pluXml, pluXml.PackageTypeGuid, "Bundle", out BundleModel? bundle)) return;
            if (plu is null || bundle is null) return;

            PluBundleFkModel pluBundleFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = plu,
                Bundle = bundle
            };

            // Find by Identity -> Update exists.
            PluBundleFkModel? pluBundleFkDb = pluBbundlesFksDb.Find(item =>
                Equals(item.Plu.Uid1C, pluBundleFk.Plu.Uid1C) &&
                Equals(item.Bundle.Uid1C, pluBundleFk.Bundle.Uid1C));
            if (UpdateItemDb(response, pluBundleFk, pluBundleFkDb, false)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluBundleFk, false);

            // Update db list.
            if (isSave && !pluBbundlesFksDb.Select(x => x.IdentityValueUid).Contains(pluBundleFk.IdentityValueUid))
                pluBbundlesFksDb.Add(pluBundleFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.IdentityValueUid, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPlusClips(Response1cShortModel response, List<ClipModel> clipsDb, PluModel pluXml)
    {
        try
        {
            // Check Uid1C.
            if (Equals(pluXml.ClipTypeGuid, Guid.Empty) && !string.IsNullOrEmpty(pluXml.ClipTypeName))
            {
                AddResponse1cException(response, pluXml.IdentityValueUid, $"Empty {nameof(pluXml.ClipTypeGuid)}!", "");
                return;
            }

            // Find by Uid1C -> Update exists.
            ClipModel? clipDb = clipsDb.Find(item => Equals(item.Uid1C, pluXml.ClipTypeGuid));
            if (UpdateClipDb(response, pluXml, clipDb, false)) return;

            // Find by Name -> Update exists.
            clipDb = clipsDb.Find(item => Equals(item.Name, pluXml.ClipTypeName));
            if (UpdateClipDb(response, pluXml, clipDb, false)) return;

            // Not find -> Add new.
            clipDb = new();
            clipDb.UpdateProperties(pluXml);
            bool isSave = SaveItemDb(response, clipDb, false);

            // Update db list.
            if (isSave && !clipsDb.Select(x => x.IdentityValueUid).Contains(clipDb.IdentityValueUid))
                clipsDb.Add(clipDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.IdentityValueUid, ex);
        }
    }

    private string[] GetPluPropertiesArray() => new string[]
    {
        nameof(PluModel.BoxTypeGuid),
        nameof(PluModel.BoxTypeName),
        nameof(PluModel.BoxTypeWeight),
        nameof(PluModel.BrandGuid),
        nameof(PluModel.CategoryGuid),
        nameof(PluModel.ClipTypeGuid),
        nameof(PluModel.ClipTypeName),
        nameof(PluModel.ClipTypeWeight),
        nameof(PluModel.Code),
        nameof(PluModel.Description),
        nameof(PluModel.FullName),
        nameof(PluModel.GroupGuid),
        nameof(PluModel.IdentityValueUid),
        nameof(PluModel.IsCheckWeight),
        nameof(PluModel.IsGroup),
        nameof(PluModel.IsMarked),
        nameof(PluModel.MeasurementType),
        nameof(PluModel.Name),
        nameof(PluModel.Number),
        nameof(PluModel.PackageTypeGuid),
        nameof(PluModel.PackageTypeName),
        nameof(PluModel.PackageTypeWeight),
        nameof(PluModel.ParentGuid),
        nameof(PluModel.ShelfLifeDays),
    };

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public ContentResult NewResponse1cPlus(ISessionFactory sessionFactory, XElement xml, string format) =>
        NewResponse1cCore<Response1cShortModel>(sessionFactory, response =>
        {
            string[] pluProperties = GetPluPropertiesArray();
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<PluModel> plusDb = DataContext.GetListNotNullable<PluModel>(sqlCrudConfig);
            List<PluFkModel> pluFksDb = DataContext.GetListNotNullable<PluFkModel>(sqlCrudConfig);
            List<BoxModel> boxesDb = DataContext.GetListNotNullable<BoxModel>(sqlCrudConfig);
            List<BundleModel> bundlesDb = DataContext.GetListNotNullable<BundleModel>(sqlCrudConfig);
            List<PluBundleFkModel> pluBbundlesFksDb = DataContext.GetListNotNullable<PluBundleFkModel>(sqlCrudConfig);
            List<ClipModel> clipsDb = DataContext.GetListNotNullable<ClipModel>(sqlCrudConfig);
            List<PluNestingFkModel> pluNestingFksDb = DataContext.GetListNotNullable<PluNestingFkModel>(sqlCrudConfig);
            List<PluModel> plusXml = GetXmlPluList(xml);
            foreach (PluModel pluXml in plusXml)
            {
                CheckPluValidator(pluXml, pluProperties);
                CheckPluDublicate(pluXml, plusDb);
                if (pluXml.ParseResult.Status == ParseStatus.Success)
                    AddResponse1cPlus(response, plusDb, pluXml);
                if (pluXml.ParseResult.Status == ParseStatus.Success)
                    AddResponse1cPlusFks(response, pluFksDb, pluXml);
                if (pluXml.ParseResult.Status == ParseStatus.Success)
                    AddResponse1cPlusBoxes(response, boxesDb, pluXml);
                if (pluXml.ParseResult.Status == ParseStatus.Success)
                    AddResponse1cPlusBundles(response, bundlesDb, pluXml);
                if (pluXml.ParseResult.Status == ParseStatus.Success)
                    AddResponse1cPlusClips(response, clipsDb, pluXml);
                if (pluXml.ParseResult.Status == ParseStatus.Success)
                    AddResponse1cPlusBundlesFks(response, pluBbundlesFksDb, pluXml);
                //if (pluXml.ParseResult.Status == ParseStatus.Success)
                //    AddResponse1cPlusClipsFks(response, pluFksDb, pluXml);
                //if (pluXml.ParseResult.Status == ParseStatus.Success)
                //    AddResponse1cPlusNestingFks(response, pluNestingFksDb, pluXml);
                if (pluXml.ParseResult.Status == ParseStatus.Error)
                {
                    AddResponse1cException(response, pluXml.IdentityValueUid,
                        pluXml.ParseResult.Exception, pluXml.ParseResult.InnerException);
                }
            }
        }, format, false);

    private void CheckPluValidator(PluModel itemXml, string[] pluProperties)
    {
        PluValidator pluValidator = new();
        ValidationResult validation = pluValidator.Validate(itemXml);
        if (!validation.IsValid)
        {
            foreach (ValidationFailure error in validation.Errors)
            {
                if (pluProperties.Contains(error.PropertyName) &&
                    !itemXml.ParseResult.Exception.Contains(error.PropertyName))
                    SetItemParseResultException(itemXml, error.PropertyName);
            }
        }
    }

    private void CheckPluDublicate(PluModel pluXml, List<PluModel> plusDb)
    {
        if (pluXml.IsGroup) return;
        if (plusDb.Select(x => x.Number).Contains(pluXml.Number))
        {
            PluModel? pluDb = plusDb.Find(x => Equals(x.Number, pluXml.Number) && !Equals(x.Code, pluXml.Code));
            if (pluDb is not null)
            {
                pluXml.ParseResult.Status = ParseStatus.Error;
                pluXml.ParseResult.Exception = $"Dublicate PluNumber '{pluXml.Number}' with Code '{pluXml.Code}' for DB record with Code '{pluDb.Code}'";
            }
        }
    }

    #endregion
}