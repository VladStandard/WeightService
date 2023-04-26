// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsWebApiCore.Helpers;

public sealed class WsPlusHelper : WsContentBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsPlusHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsPlusHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    internal WsPlusHelper(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    private bool UpdateBoxDb(WsResponse1cShortModel response, PluModel pluXml, BoxModel? boxDb, bool isCounter)
    {
        if (boxDb is null || boxDb.IsNew) return false;
        boxDb.UpdateProperties(pluXml);
        SqlCrudResultModel dbUpdate = AccessManager.AccessItem.UpdateForce(boxDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
            {
                response.Successes.Add(new(pluXml.Uid1c));
                response.SuccessesPlus?.Add(new(pluXml.Uid1c, $"{WsWebConstants.PluNumber}='{pluXml.Number}'"));
            }
        }
        else
            AddResponse1cException(response, pluXml.Uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    internal bool UpdateBundleDb(WsResponse1cShortModel response, PluModel pluXml, BundleModel? bundleDb, bool isCounter)
    {
        if (bundleDb is null || bundleDb.IsNew) return false;
        bundleDb.UpdateProperties(pluXml);
        SqlCrudResultModel dbUpdate = AccessManager.AccessItem.UpdateForce(bundleDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
            {
                response.Successes.Add(new(pluXml.Uid1c));
                response.SuccessesPlus?.Add(new(pluXml.Uid1c, $"{WsWebConstants.PluNumber}='{pluXml.Number}'"));
            }
        }
        else
            AddResponse1cException(response, pluXml.Uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    internal bool UpdateClipDb(WsResponse1cShortModel response, PluModel pluXml, ClipModel? clipDb, bool isCounter)
    {
        if (clipDb is null || clipDb.IsNew) return false;
        clipDb.UpdateProperties(pluXml);
        SqlCrudResultModel dbUpdate = AccessManager.AccessItem.UpdateForce(clipDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
            {
                response.Successes.Add(new(pluXml.Uid1c));
                response.SuccessesPlus?.Add(new(pluXml.Uid1c, $"{WsWebConstants.PluNumber}='{pluXml.Number}'"));
            }
        }
        else
            AddResponse1cException(response, pluXml.Uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    /// <summary>
    /// Update the PLU record in the database.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    /// <param name="pluDb"></param>
    /// <param name="isCounter"></param>
    internal bool UpdatePluDb(WsResponse1cShortModel response, PluModel pluXml, PluModel? pluDb, bool isCounter)
    {
        if (pluDb is null || pluDb.IsNew) return false;
        pluDb.Identity = pluXml.Identity;
        pluDb.UpdateProperties(pluXml);
        // Native update -> Be careful, good luck.
        SqlCrudResultModel dbUpdate = AccessManager.AccessItem.ExecQueryNative(
            WsWebSqlQueries.UpdatePlu, new List<SqlParameter>
            {
                new("uid", pluXml.IdentityValueUid),
                new("code", pluDb.Code),
                new("number", pluDb.Number),
            });
        if (dbUpdate.IsOk)
        {
            if (isCounter)
            {
                response.Successes.Add(new(pluXml.Uid1c));
                response.SuccessesPlus?.Add(new(pluXml.Uid1c, $"{WsWebConstants.PluNumber}='{pluXml.Number}'"));
            }
        }
        else
        {
            AddResponse1cException(response, pluXml.IdentityValueUid, dbUpdate.Exception);
        }
        return dbUpdate.IsOk;
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Fill PLU list from XML.
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    internal List<PluModel> GetXmlPluList(XElement xml) =>
        WsContentUtils.GetNodesListCore<PluModel>(xml, LocaleCore.WebService.XmlItemNomenclature, (xmlNode, itemXml) =>
        {
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsGroup));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ParentGroupGuid");
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.FullName));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.CategoryGuid));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BrandGuid));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.MeasurementType));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.GroupGuid));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.AttachmentsCount));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BoxTypeGuid));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BoxTypeName));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BoxTypeWeight));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.PackageTypeGuid));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.PackageTypeName));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.PackageTypeWeight));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.ClipTypeGuid));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.ClipTypeName));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.ClipTypeWeight));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "PluNumber");
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Description));
            //SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsCheckWeight));
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ShelfLife");
            WsContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Gtin));
        });

    /// <summary>
    /// Добавить ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="plusDb"></param>
    /// <param name="pluXml"></param>
    private void AddResponse1cPlu(WsResponse1cShortModel response, List<PluModel> plusDb, PluModel pluXml)
    {
        try
        {
            // Check Uid1C.
            if (Equals(pluXml.IdentityValueUid, Guid.Empty))
            {
                AddResponse1cException(response, pluXml.Uid1c,
                    $"{LocaleCore.WebService.IsEmpty} {LocaleCore.WebService.FieldGuid}!", "");
                return;
            }

            // Find by Uid1C -> Update exists.
            PluModel? pluDb = plusDb.Find(item => Equals(item.Uid1c, pluXml.IdentityValueUid));
            if (UpdateItem1cDb(response, pluXml, pluDb, true, pluXml.Number.ToString())) return;

            // Find by Code -> Update exists.
            pluDb = plusDb.Find(item => Equals(item.Code, pluXml.Code));
            if (UpdateItem1cDb(response, pluXml, pluDb, true, pluXml.Number.ToString())) return;

            // Find by Number -> Update exists.
            pluDb = plusDb.Find(item => Equals(item.Number, pluXml.Number) && !Equals(item.Number, (short)0));
            if (UpdateItem1cDb(response, pluXml, pluDb, true, pluXml.Number.ToString())) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluXml, true);

            // Update db list.
            if (pluDb is not null && isSave && !plusDb.Select(x => x.IdentityValueUid).Contains(pluDb.IdentityValueUid))
                plusDb.Add(pluDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
        }
    }

    /// <summary>
    /// Добавить связь ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluFksDb"></param>
    /// <param name="pluXml"></param>
    private void AddResponse1cPluFks(WsResponse1cShortModel response, List<PluFkModel> pluFksDb, PluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.ParentGuid, Guid.Empty)) return;
            // Проверить наличие ПЛУ в БД.
            if (!CheckExistsPluDb(response, pluXml.Uid1c, pluXml.Uid1c, LocaleCore.WebService.FieldNomenclature, false, out PluModel? pluDb)) return;
            if (!CheckExistsPluDb(response, pluXml.ParentGuid, pluXml.Uid1c, LocaleCore.WebService.FieldGroup, true, out PluModel? parentDb)) return;
            if (!CheckExistsPluDb(response, pluXml.CategoryGuid, pluXml.Uid1c, LocaleCore.WebService.FieldGroup1Level, true, out PluModel? categoryDb)) return;
            if (pluDb is null || parentDb is null) return;

            PluFkModel pluFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Parent = parentDb,
                Category = categoryDb
            };

            // Find by Identity -> Update exists.
            PluFkModel? pluFkDb = pluFksDb.Find(item =>
                Equals(item.Plu.Uid1c, pluFk.Plu.Uid1c) &&
                Equals(item.Parent.Uid1c, pluFk.Parent.Uid1c) &&
                Equals(item.Category?.Uid1c, pluFk.Category?.Uid1c));
            if (UpdatePluFkDb(response, pluXml.Uid1c, pluFk, pluFkDb, false)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluFk, false, pluXml.Uid1c);

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
    /// Добавить коробку.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="boxesDb"></param>
    /// <param name="pluXml"></param>
    private void AddResponse1cPluBox(WsResponse1cShortModel response, List<BoxModel> boxesDb, PluModel pluXml)
    {
        try
        {
            // Check Uid1C.
            if (Equals(pluXml.BoxTypeGuid, Guid.Empty))
            {
                // BoxTypeGuid="00000000-0000-0000-0000-000000000000" BoxTypeName!="" BoxTypeWeight!="".
                if (pluXml.PackageTypeWeight > 0)
                {
                    AddResponse1cException(response, pluXml.Uid1c,
                    $"{LocaleCore.WebService.IsEmpty} {nameof(pluXml.BoxTypeGuid)}!", "");
                    return;
                }
                // BoxTypeGuid="00000000-0000-0000-0000-000000000000" BoxTypeName="" BoxTypeWeight="".
                pluXml.BoxTypeName = LocaleCore.WebService.BoxZero;
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
            bool isSave = SaveItemDb(response, boxDb, false, pluXml.Uid1c);

            // Update db list.
            if (isSave && !boxesDb.Select(x => x.IdentityValueUid).Contains(boxDb.IdentityValueUid))
                boxesDb.Add(boxDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
        }
    }

    /// <summary>
    /// Добавить пакет.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="bundlesDb"></param>
    /// <param name="pluXml"></param>
    private void AddResponse1cPluBundle(WsResponse1cShortModel response, List<BundleModel> bundlesDb, PluModel pluXml)
    {
        try
        {
            // Check Uid1C.
            if (Equals(pluXml.PackageTypeGuid, Guid.Empty))
            {
                // PackageTypeGuid="00000000-0000-0000-0000-000000000000" PackageTypeName!="" PackageTypeWeight!="".
                if (pluXml.PackageTypeWeight > 0)
                {
                    AddResponse1cException(response, pluXml.Uid1c,
                    $"{LocaleCore.WebService.IsEmpty} {nameof(pluXml.PackageTypeGuid)}!", "");
                    return;
                }
                // PackageTypeGuid="00000000-0000-0000-0000-000000000000" PackageTypeName="" PackageTypeWeight="".
                pluXml.PackageTypeName = LocaleCore.WebService.PackageZero;
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
            bool isSave = SaveItemDb(response, bundleDb, false, pluXml.Uid1c);

            // Update db list.
            if (isSave && !bundlesDb.Select(x => x.IdentityValueUid).Contains(bundleDb.IdentityValueUid))
                bundlesDb.Add(bundleDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
        }
    }

    /// <summary>
    /// Добавить связь бренда.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluBrandsFksDb"></param>
    /// <param name="pluXml"></param>
    private void AddResponse1cPluBrandFk(WsResponse1cShortModel response, List<PluBrandFkModel> pluBrandsFksDb, PluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.BrandGuid, Guid.Empty)) return;
            // Проверить наличие ПЛУ в БД.
            if (!CheckExistsPluDb(response, pluXml.Uid1c, pluXml.Uid1c, LocaleCore.WebService.FieldNomenclature, false, out PluModel? pluDb)) return;
            // Проверить наличие бренда в БД.
            if (!CheckExistsBrandDb(response, pluXml.BrandGuid, pluXml.Uid1c, LocaleCore.WebService.FieldBrand, out BrandModel? brandDb)) return;
            if (pluDb is null || brandDb is null) return;

            PluBrandFkModel pluBrandFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Brand = brandDb
            };

            // Find by Identity -> Update exists | UQ_PLUS_CLIP_PLU_FK.
            PluBrandFkModel? pluBrandFkDb = pluBrandsFksDb.Find(item => Equals(item.Plu.Uid1c, pluBrandFk.Plu.Uid1c));
            if (UpdatePluBrandFkDb(response, pluXml.Uid1c, pluBrandFk, pluBrandFkDb, false)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluBrandFk, false, pluXml.Uid1c);

            // Update db list.
            if (isSave && !pluBrandsFksDb.Select(x => x.IdentityValueUid).Contains(pluBrandFk.IdentityValueUid))
                pluBrandsFksDb.Add(pluBrandFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
        }
    }

    /// <summary>
    /// Добавить клипсу.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="clipsDb"></param>
    /// <param name="pluXml"></param>
    private void AddResponse1cPluClip(WsResponse1cShortModel response, List<ClipModel> clipsDb, PluModel pluXml)
    {
        try
        {
            // Check Uid1C.
            if (Equals(pluXml.ClipTypeGuid, Guid.Empty))
            {
                // ClipTypeGuid="00000000-0000-0000-0000-000000000000" ClipTypeName!="" ClipTypeWeight!="".
                if (pluXml.ClipTypeWeight > 0)
                {
                    AddResponse1cException(response, pluXml.Uid1c,
                    $"{LocaleCore.WebService.IsEmpty} {nameof(pluXml.ClipTypeGuid)}!", "");
                    return;
                }
                // ClipTypeGuid="00000000-0000-0000-0000-000000000000" ClipTypeName="" ClipTypeWeight="".
                pluXml.ClipTypeName = LocaleCore.WebService.ClipZero;
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
            bool isSave = SaveItemDb(response, clipDb, false, pluXml.Uid1c);

            // Update db list.
            if (isSave && !clipsDb.Select(x => x.IdentityValueUid).Contains(clipDb.IdentityValueUid))
                clipsDb.Add(clipDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
        }
    }

    /// <summary>
    /// Добавить связь клипсы ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluClipsFksDb"></param>
    /// <param name="pluXml"></param>
    private void AddResponse1cPluClipFk(WsResponse1cShortModel response, List<PluClipFkModel> pluClipsFksDb, PluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.ClipTypeGuid, Guid.Empty)) return;
            // Проверить наличие ПЛУ в БД.
            if (!CheckExistsPluDb(response, pluXml.Uid1c, pluXml.Uid1c, LocaleCore.WebService.FieldNomenclature, false, out PluModel? pluDb)) return;
            // Проверить наличие клипсы в БД.
            if (!CheckExistsClipDb(response, pluXml.ClipTypeGuid, pluXml.Uid1c, LocaleCore.WebService.FieldClip, out ClipModel? clipDb)) return;
            if (pluDb is null || clipDb is null) return;

            PluClipFkModel pluClipFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Clip = clipDb
            };

            // Find by Identity -> Update exists | UQ_PLUS_CLIP_PLU_FK.
            PluClipFkModel? pluClipFkDb = pluClipsFksDb.Find(item => Equals(item.Plu.Uid1c, pluClipFk.Plu.Uid1c));
            if (UpdatePluClipFkDb(response, pluXml.Uid1c, pluClipFk, pluClipFkDb, false)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluClipFk, false, pluXml.Uid1c);

            // Update db list.
            if (isSave && !pluClipsFksDb.Select(x => x.IdentityValueUid).Contains(pluClipFk.IdentityValueUid))
                pluClipsFksDb.Add(pluClipFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
        }
    }

    /// <summary>
    /// Добавить связь пакета ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluBundlesFksDb"></param>
    /// <param name="pluXml"></param>
    /// <returns></returns>
    private PluBundleFkModel AddResponse1cPluBundleFk(WsResponse1cShortModel response, List<PluBundleFkModel> pluBundlesFksDb,
        PluModel pluXml)
    {
        PluBundleFkModel pluBundleFk = new();
        try
        {
            // Проверить наличие ПЛУ в БД.
            if (!CheckExistsPluDb(response, pluXml.Uid1c, pluXml.Uid1c, LocaleCore.WebService.FieldNomenclature, false, out PluModel? pluDb)) return pluBundleFk;
            // Проверить наличие пакета в БД.
            if (!CheckExistsBundleDb(response, pluXml.PackageTypeGuid, pluXml.Uid1c, LocaleCore.WebService.FieldBundle, out BundleModel? bundleDb)) return pluBundleFk;
            if (pluDb is null || bundleDb is null) return pluBundleFk;

            pluBundleFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Bundle = bundleDb,
            };

            // Find by Identity -> Update exists | UQ_BUNDLES_FK.
            PluBundleFkModel? pluBundleFkDb = pluBundlesFksDb.Find(item => Equals(item.Plu.Uid1c, pluBundleFk.Plu.Uid1c));
            if (pluBundleFkDb is not null)
                if (UpdatePluBundleFkDb(response, pluXml.Uid1c, pluBundleFk, pluBundleFkDb, false)) return pluBundleFkDb;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluBundleFk, false, pluXml.Uid1c);

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

    /// <summary>
    /// Добавить связь вложенности ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluBundleFk"></param>
    /// <param name="pluNestingFksDb"></param>
    /// <param name="pluXml"></param>
    private void AddResponse1cPluNestingFk(WsResponse1cShortModel response, PluBundleFkModel pluBundleFk,
        List<PluNestingFkModel> pluNestingFksDb, PluModel pluXml)
    {
        try
        {
            if (pluBundleFk.IsNotExists)
            {
                List<PluBundleFkModel> pluBundleFks = ContextManager.ContextList.GetListNotNullablePlusBundlesFks(SqlCrudConfig);
                if (pluBundleFks.Any())
                {
                    pluBundleFk = pluBundleFks.Find(item => Equals(item.Plu.Number, pluXml.Number)) ?? new();
                }
            }
            if (pluBundleFk.IsNotExists) return;

            if (!GetBoxDb(response, pluXml.BoxTypeGuid, pluXml.Uid1c, "Box", out BoxModel? boxDb)) return;
            if (boxDb is null) return;

            PluNestingFkModel pluNestingFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                PluBundle = pluBundleFk,
                Box = boxDb,
                BundleCount = pluXml.AttachmentsCount,
                IsDefault = true,
            };

            // Find by Identity -> Update exists | UQ_PLUS_NESTING_FK.
            PluNestingFkModel? pluNestingFkDb = pluNestingFksDb.FirstOrDefault(item =>
                    Equals(item.Box.Uid1c, pluNestingFk.Box.Uid1c) &&
                    Equals(item.PluBundle.Plu.Uid1c, pluNestingFk.PluBundle.Plu.Uid1c) &&
                    Equals(item.PluBundle.Bundle.Uid1c, pluNestingFk.PluBundle.Bundle.Uid1c) &&
                    Equals(item.BundleCount, pluXml.AttachmentsCount));
            if (UpdatePluNestingFk(response, pluXml.Uid1c, pluNestingFk, pluNestingFkDb, false)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluNestingFk, false, pluXml.Uid1c);

            // Update db list.
            if (isSave && !pluNestingFksDb.Select(x => x.IdentityValueUid).Contains(pluNestingFk.IdentityValueUid))
                pluNestingFksDb.Add(pluNestingFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluXml.Uid1c, ex);
        }
    }

    private string[] GetPluPropertiesArray() => new[]
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

    /// <summary>
    /// Отправить номенклатуру и получить ответ.
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponse1cPlus(XElement xml, string format, bool isDebug, ISessionFactory sessionFactory) =>
        NewResponse1cCore<WsResponse1cShortModel>(response =>
        {
            // Подготовка.
            List<PluModel> plusDb = ContextManager.ContextList.GetListNotNullablePlus(SqlCrudConfig);
            List<PluFkModel> pluFksDb = ContextManager.ContextList.GetListNotNullablePlusFks(SqlCrudConfig);
            List<BoxModel> boxesDb = ContextManager.ContextList.GetListNotNullableBoxes(SqlCrudConfig);
            List<BundleModel> bundlesDb = ContextManager.ContextList.GetListNotNullableBundles(SqlCrudConfig);
            List<PluBundleFkModel> pluBundlesFksDb = ContextManager.ContextList.GetListNotNullablePlusBundlesFks(SqlCrudConfig);
            List<PluBrandFkModel> pluBrandsFksDb = ContextManager.ContextList.GetListNotNullablePlusBrandsFks(SqlCrudConfig);
            List<ClipModel> clipsDb = ContextManager.ContextList.GetListNotNullableClips(SqlCrudConfig);
            List<PluClipFkModel> pluClipsFksDb = ContextManager.ContextList.GetListNotNullablePlusClipsFks(SqlCrudConfig);
            List<PluNestingFkModel> pluNestingFksDb = ContextManager.ContextList.GetListNotNullablePlusNestingFks(
                new(WsSqlQueriesScales.Tables.PluNestingFks.GetList(false), false));
            List<PluModel> plusXml = GetXmlPluList(xml);
            // Цикл по всем XML-номенклатурам.
            foreach (PluModel pluXml in plusXml)
            {
                // Проверить номер ПЛУ для группы.
                if (pluXml.ParseResult.IsStatusSuccess)
                    CheckPluNumberForNonGroup(pluXml);
                // Проверить номер ПЛУ по списку разрешённых для загрузки.
                if (pluXml.ParseResult.IsStatusSuccess)
                    CheckPluNumberForAcl(pluXml, pluXml);
                // Проверить валидацию ПЛУ.
                if (pluXml.ParseResult.IsStatusSuccess)
                    CheckPluValidation(pluXml);
                // Проверить дубликат ПЛУ для не группы.
                if (pluXml.ParseResult.IsStatusSuccess)
                    CheckPluDublicateForNonGroup(pluXml, plusDb);
                // Добавить ПЛУ.
                if (pluXml.ParseResult.IsStatusSuccess)
                    AddResponse1cPlu(response, plusDb, pluXml);
                // Добавить связь ПЛУ.
                if (pluXml.ParseResult.IsStatusSuccess)
                    AddResponse1cPluFks(response, pluFksDb, pluXml);
                // Добавить коробку.
                if (pluXml.ParseResult.IsStatusSuccess)
                    AddResponse1cPluBox(response, boxesDb, pluXml);
                // Добавить пакет.
                if (pluXml.ParseResult.IsStatusSuccess)
                    AddResponse1cPluBundle(response, bundlesDb, pluXml);
                // Добавить связь бренда.
                if (pluXml.ParseResult.IsStatusSuccess)
                    AddResponse1cPluBrandFk(response, pluBrandsFksDb, pluXml);
                // Добавить клипсу.
                if (pluXml.ParseResult.IsStatusSuccess)
                    AddResponse1cPluClip(response, clipsDb, pluXml);
                // Добавить связь клипсы ПЛУ.
                if (pluXml.ParseResult.IsStatusSuccess)
                    AddResponse1cPluClipFk(response, pluClipsFksDb, pluXml);

                if (pluXml.ParseResult.IsStatusSuccess)
                {
                    // Добавить связь пакета ПЛУ.
                    PluBundleFkModel pluBundleFk = AddResponse1cPluBundleFk(response, pluBundlesFksDb, pluXml);
                    // Добавить связь вложенности ПЛУ.
                    if (pluXml.ParseResult.IsStatusSuccess)
                        AddResponse1cPluNestingFk(response, pluBundleFk, pluNestingFksDb, pluXml);
                }
                // Исключение.
                if (pluXml.ParseResult.IsStatusError)
                    AddResponse1cException(response, pluXml.Uid1c,
                        pluXml.ParseResult.Exception, pluXml.ParseResult.InnerException);
            }
        }, format, isDebug, sessionFactory);

    /// <summary>
    /// Проверить валидацию ПЛУ.
    /// </summary>
    /// <param name="itemXml"></param>
    private void CheckPluValidation(PluModel itemXml)
    {
        PluValidator pluValidator = new();
        ValidationResult validation = pluValidator.Validate(itemXml);
        if (!validation.IsValid)
        {
            string[] pluProperties = GetPluPropertiesArray();
            foreach (ValidationFailure error in validation.Errors)
            {
                if (pluProperties.Contains(error.PropertyName) &&
                    !itemXml.ParseResult.Exception.Contains(error.PropertyName))
                    WsContentUtils.SetItemParseResultException(itemXml, error.PropertyName);
            }
        }
    }

    /// <summary>
    /// Проверить номер ПЛУ для группы.
    /// </summary>
    /// <param name="pluXml"></param>
    private void CheckPluNumberForNonGroup(PluModel pluXml)
    {
        if (pluXml.IsGroup) return;
        if (Equals(pluXml.Number, (short)0))
        {
            pluXml.ParseResult.Status = ParseStatus.Error;
            pluXml.ParseResult.Exception =
                $"{LocaleCore.WebService.FieldPluNumber} '{pluXml.Number}' " +
                $"{LocaleCore.WebService.ForDbRecord} {LocaleCore.WebService.With} {LocaleCore.WebService.FieldCode} '{pluXml.Code}'";
        }
    }

    /// <summary>
    /// Проверить дубликат ПЛУ для не группы.
    /// </summary>
    /// <param name="pluXml"></param>
    /// <param name="plusDb"></param>
    private void CheckPluDublicateForNonGroup(PluModel pluXml, List<PluModel> plusDb)
    {
        if (pluXml.IsGroup) return;
        if (plusDb.Select(x => x.Number).Contains(pluXml.Number))
        {
            PluModel? pluDb = plusDb.Find(x => Equals(x.Number, pluXml.Number) && !Equals(x.Code, pluXml.Code));
            if (pluDb is not null)
            {
                pluXml.ParseResult.Status = ParseStatus.Error;
                pluXml.ParseResult.Exception =
                    $"{LocaleCore.WebService.Dublicate} {LocaleCore.WebService.FieldPluNumber} '{pluXml.Number}' " +
                    $"{LocaleCore.WebService.With} {LocaleCore.WebService.FieldCode} '{pluXml.Code}' {LocaleCore.WebService.ForDbRecord} {LocaleCore.WebService.With} Code '{pluDb.Code}'";
            }
        }
    }

    #endregion
}