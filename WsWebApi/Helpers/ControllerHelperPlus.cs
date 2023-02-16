// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusClipsFks;
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

    /// <summary>
    /// Fill PLU list from XML.
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
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
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.AttachmentsCount));
        });

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPlus(Response1cShortModel response, List<PluModel> plusDb, PluModel pluXml)
    {
        try
        {
            // Check Uid1C.
            if (Equals(pluXml.IdentityValueUid, Guid.Empty))
            {
                AddResponse1cException(response, pluXml.Uid1c, "Empty GUID!", "");
                return;
            }

            // Find by Uid1C -> Update exists.
            PluModel? pluDb = plusDb.Find(item => Equals(item.Uid1c, pluXml.IdentityValueUid));
            if (UpdateItemDb(response, pluXml.Uid1c, pluXml, pluDb, true)) return;

            // Find by Code -> Update exists.
            pluDb = plusDb.Find(item => Equals(item.Code, pluXml.Code));
            if (UpdateItemDb(response, pluXml.Uid1c, pluXml, pluDb, true)) return;

            // Find by Number -> Update exists.
            pluDb = plusDb.Find(item => Equals(item.Number, pluXml.Number));
            if (UpdateItemDb(response, pluXml.Uid1c, pluXml, pluDb, true)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluXml.Uid1c, pluXml, true);

            // Update db list.
            if (pluDb is not null && isSave && !plusDb.Select(x => x.IdentityValueUid).Contains(pluDb.IdentityValueUid))
                plusDb.Add(pluDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
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
                Equals(item.Plu.Uid1c, pluFk.Plu.Uid1c) &&
                Equals(item.Parent.Uid1c, pluFk.Parent.Uid1c) &&
                Equals(item.Category?.Uid1c, pluFk.Category?.Uid1c));
            if (UpdateItemDb(response, pluXml.Uid1c, pluFk, pluFkDb, false)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluXml.Uid1c, pluFk, false);

            // Update db list.
            if (isSave && !pluFksDb.Select(x => x.IdentityValueUid).Contains(pluFk.IdentityValueUid))
                pluFksDb.Add(pluFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
        }
    }

    /// <summary>
    /// Get PLU for PLU from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    /// <param name="uid"></param>
    /// <param name="refName"></param>
    /// <param name="isCheckGroup"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    private bool GetPluFkPluDb(Response1cShortModel response, PluModel pluXml, Guid uid, string refName, bool isCheckGroup, out PluModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                { new(nameof(SqlTableBase1c.Uid1c), SqlFieldComparerEnum.Equal, uid) },
                true, false, false, false);
            itemDb = DataContext.DataAccess.GetItemNullable<PluModel>(sqlCrudConfig);
            if (!isCheckGroup)
            {
                if (itemDb is null || itemDb.IsNew)
                {
                    AddResponse1cException(response, pluXml.Uid1c, new($"{refName} with '{uid}' is not found!"));
                    return false;
                }
                return true;
            }
            // isCheckGroup.
            if (itemDb is null || itemDb.IsNew || !itemDb.IsGroup)
            {
                AddResponse1cException(response, pluXml.Uid1c, new($"{refName} with '{uid}' is not found!"));
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get bundle for PLU from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    /// <param name="uid"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    private bool GetPluBundleFkBundleDb(Response1cShortModel response, PluModel pluXml, Guid uid, string refName, out BundleModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new(nameof(SqlTableBase1c.Uid1c), SqlFieldComparerEnum.Equal, uid) },
                true, false, false, false);
            itemDb = DataContext.DataAccess.GetItemNullable<BundleModel>(sqlCrudConfig);
            if (itemDb is null || itemDb.IsNew)
            {
                AddResponse1cException(response, pluXml.Uid1c, new($"{refName} with '{uid}' is not found!"));
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get clip for PLU from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    /// <param name="uid"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    private bool GetPluClipFkClipDb(Response1cShortModel response, PluModel pluXml, Guid uid, string refName, out ClipModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new(nameof(SqlTableBase1c.Uid1c), SqlFieldComparerEnum.Equal, uid) },
                true, false, false, false);
            itemDb = DataContext.DataAccess.GetItemNullable<ClipModel>(sqlCrudConfig);
            if (itemDb is null || itemDb.IsNew)
            {
                AddResponse1cException(response, pluXml.Uid1c, new($"{refName} with '{uid}' is not found!"));
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get box for PLU from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    /// <param name="uid"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    private bool GetPluNestingFkBoxDb(Response1cShortModel response, PluModel pluXml, Guid uid, string refName, out BoxModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new(nameof(SqlTableBase1c.Uid1c), SqlFieldComparerEnum.Equal, uid) },
                true, false, false, false);
            itemDb = DataContext.DataAccess.GetItemNullable<BoxModel>(sqlCrudConfig);
            if (itemDb is null || itemDb.IsNew)
            {
                AddResponse1cException(response, pluXml.Uid1c, new($"{refName} with '{uid}' is not found!"));
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
                AddResponse1cException(response, pluXml.Uid1c, $"Empty {nameof(pluXml.BoxTypeGuid)}!", "");
                return;
            }

            // Find by Uid1C -> Update exists.
            BoxModel? boxDb = boxesDb.Find(item => Equals(item.Uid1c, pluXml.BoxTypeGuid));
            if (UpdateBoxDb(response, pluXml, boxDb, false)) return;

            // Find by Name -> Update exists.
            boxDb = boxesDb.Find(item => Equals(item.Name, pluXml.BoxTypeName));
            if (UpdateBoxDb(response, pluXml, boxDb, false)) return;

            // Not find -> Add new.
            boxDb = new();
            boxDb.UpdateProperties(pluXml);
            bool isSave = SaveItemDb(response, pluXml.Uid1c, boxDb, false);

            // Update db list.
            if (isSave && !boxesDb.Select(x => x.IdentityValueUid).Contains(boxDb.IdentityValueUid))
                boxesDb.Add(boxDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
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
                AddResponse1cException(response, pluXml.Uid1c, $"Empty {nameof(pluXml.PackageTypeGuid)}!", "");
                return;
            }

            // Find by Uid1C -> Update exists.
            BundleModel? bundleDb = bundlesDb.Find(item => Equals(item.Uid1c, pluXml.PackageTypeGuid));
            if (UpdateBundleDb(response, pluXml, bundleDb, false)) return;

            // Find by Name -> Update exists.
            bundleDb = bundlesDb.Find(item => Equals(item.Name, pluXml.PackageTypeName));
            if (UpdateBundleDb(response, pluXml, bundleDb, false)) return;

            // Not find -> Add new.
            bundleDb = new();
            bundleDb.UpdateProperties(pluXml);
            bool isSave = SaveItemDb(response, pluXml.Uid1c, bundleDb, false);

            // Update db list.
            if (isSave && !bundlesDb.Select(x => x.IdentityValueUid).Contains(bundleDb.IdentityValueUid))
                bundlesDb.Add(bundleDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private PluBundleFkModel AddResponse1cPlusBundlesFks(Response1cShortModel response, List<PluBundleFkModel> pluBundlesFksDb, PluModel pluXml)
    {
        PluBundleFkModel pluBundleFk = new();
        try
        {
            if (Equals(pluXml.PackageTypeGuid, Guid.Empty)) return pluBundleFk;

            if (!GetPluFkPluDb(response, pluXml, pluXml.IdentityValueUid, "PLU", false, out PluModel? plu)) return pluBundleFk;
            if (!GetPluBundleFkBundleDb(response, pluXml, pluXml.PackageTypeGuid, "Bundle", out BundleModel? bundle)) return pluBundleFk;
            if (plu is null || bundle is null) return pluBundleFk;

            pluBundleFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = plu,
                Bundle = bundle
            };

            // Find by Identity -> Update exists | UQ_BUNDLES_FK.
            PluBundleFkModel? pluBundleFkDb = pluBundlesFksDb.Find(item => Equals(item.Plu.Uid1c, pluBundleFk.Plu.Uid1c));
            if (pluBundleFkDb is not null)
                if (UpdateItemDb(response, pluXml.Uid1c, pluBundleFk, pluBundleFkDb, false)) return pluBundleFkDb;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluXml.Uid1c, pluBundleFk, false);

            // Update db list.
            if (isSave && !pluBundlesFksDb.Select(x => x.IdentityValueUid).Contains(pluBundleFk.IdentityValueUid))
                pluBundlesFksDb.Add(pluBundleFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
        }
        return pluBundleFk;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPlusClips(Response1cShortModel response, List<ClipModel> clipsDb, PluModel pluXml)
    {
        try
        {
            // Check Uid1C.
            if (Equals(pluXml.ClipTypeGuid, Guid.Empty) && !string.IsNullOrEmpty(pluXml.ClipTypeName))
            {
                AddResponse1cException(response, pluXml.Uid1c, $"Empty {nameof(pluXml.ClipTypeGuid)}!", "");
                return;
            }

            // Find by Uid1C -> Update exists.
            ClipModel? clipDb = clipsDb.Find(item => Equals(item.Uid1c, pluXml.ClipTypeGuid));
            if (UpdateClipDb(response, pluXml, clipDb, false)) return;

            // Find by Name -> Update exists.
            clipDb = clipsDb.Find(item => Equals(item.Name, pluXml.ClipTypeName));
            if (UpdateClipDb(response, pluXml, clipDb, false)) return;

            // Not find -> Add new.
            clipDb = new();
            clipDb.UpdateProperties(pluXml);
            bool isSave = SaveItemDb(response, pluXml.Uid1c, clipDb, false);

            // Update db list.
            if (isSave && !clipsDb.Select(x => x.IdentityValueUid).Contains(clipDb.IdentityValueUid))
                clipsDb.Add(clipDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPlusClipsFks(Response1cShortModel response, List<PluClipFkModel> pluClipsFksDb, PluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.PackageTypeGuid, Guid.Empty)) return;

            if (!GetPluFkPluDb(response, pluXml, pluXml.IdentityValueUid, "PLU", false, out PluModel? plu)) return;
            if (!GetPluClipFkClipDb(response, pluXml, pluXml.ClipTypeGuid, "Clip", out ClipModel? clip)) return;
            if (plu is null || clip is null) return;

            PluClipFkModel pluClipFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = plu,
                Clip = clip
            };

            // Find by Identity -> Update exists | UQ_PLUS_CLIP_PLU_FK.
            PluClipFkModel? pluClipFkDb = pluClipsFksDb.Find(item => Equals(item.Plu.Uid1c, pluClipFk.Plu.Uid1c));
            if (UpdateItemDb(response, pluXml.Uid1c, pluClipFk, pluClipFkDb, false)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluXml.Uid1c, pluClipFk, false);

            // Update db list.
            if (isSave && !pluClipsFksDb.Select(x => x.IdentityValueUid).Contains(pluClipFk.IdentityValueUid))
                pluClipsFksDb.Add(pluClipFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPlusNestingFks(Response1cShortModel response, PluBundleFkModel pluBundleFk,
        List<PluNestingFkModel> pluNestingFksDb, PluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.PackageTypeGuid, Guid.Empty)) return;

            if (!GetPluNestingFkBoxDb(response, pluXml, pluXml.BoxTypeGuid, "Box", out BoxModel? box)) return;
            if (box is null) return;

            PluNestingFkModel pluNestingFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                PluBundle = pluBundleFk,
                Box = box,
            };

            // Find by Identity -> Update exists | UQ_PLUS_NESTING_FK.
            PluNestingFkModel? pluNestingFkDb = pluNestingFksDb.FirstOrDefault(item => 
                    Equals(item.Box.Uid1c, pluNestingFk.Box.Uid1c) && 
                    Equals(item.PluBundle.Plu.Uid1c, pluNestingFk.PluBundle.Plu.Uid1c) && 
                    Equals(item.PluBundle.Bundle.Uid1c, pluNestingFk.PluBundle.Bundle.Uid1c) && 
                    Equals(item.BundleCount, pluXml.AttachmentsCount));
            if (UpdateItemDb(response, pluXml.Uid1c, pluNestingFk, pluNestingFkDb, false)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluXml.Uid1c, pluNestingFk, false);

            // Update db list.
            if (isSave && !pluNestingFksDb.Select(x => x.IdentityValueUid).Contains(pluNestingFk.IdentityValueUid))
                pluNestingFksDb.Add(pluNestingFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
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
            List<PluBundleFkModel> pluBundlesFksDb = DataContext.GetListNotNullable<PluBundleFkModel>(sqlCrudConfig);
            List<ClipModel> clipsDb = DataContext.GetListNotNullable<ClipModel>(sqlCrudConfig);
            List<PluClipFkModel> pluClipsFksDb = DataContext.GetListNotNullable<PluClipFkModel>(sqlCrudConfig);
            List<PluNestingFkModel> pluNestingFksDb = DataContext.GetListNotNullable<PluNestingFkModel>(
                new(DataCore.Sql.Core.Utils.SqlQueries.DbScales.Tables.PluNestingFks.GetList(false), false));
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
                    AddResponse1cPlusClipsFks(response, pluClipsFksDb, pluXml);
                if (pluXml.ParseResult.Status == ParseStatus.Success)
                {
                    PluBundleFkModel pluBundleFk = AddResponse1cPlusBundlesFks(response, pluBundlesFksDb, pluXml);
                    if (pluXml.ParseResult.Status == ParseStatus.Success)
                        AddResponse1cPlusNestingFks(response, pluBundleFk, pluNestingFksDb, pluXml);
                }
                if (pluXml.ParseResult.Status == ParseStatus.Error)
                    AddResponse1cException(response, pluXml.Uid1c, pluXml.ParseResult.Exception, pluXml.ParseResult.InnerException);
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