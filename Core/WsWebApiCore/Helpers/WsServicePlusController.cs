// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Helpers;

/// <summary>
/// Веб-контроллер номенклатур.
/// </summary>
public sealed class WsServicePlusController : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    public WsServicePlusController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    private void UpdateBoxDb(WsSqlPluModel pluXml, WsSqlBoxModel? boxDb)
    {
        if (boxDb is null || boxDb.IsNew) return;
        boxDb.UpdateProperties(pluXml);
        SqlCore.Update(boxDb);
    }

    private void UpdateBundleDb(WsSqlPluModel pluXml, WsSqlBundleModel? bundleDb)
    {
        if (bundleDb is null || bundleDb.IsNew) return;
        bundleDb.UpdateProperties(pluXml);
        SqlCore.Update(bundleDb);
    }

    private void UpdateClipDb(WsSqlPluModel pluXml, WsSqlClipModel? clipDb)
    {
        if (clipDb is null || clipDb.IsNew) return;
        clipDb.UpdateProperties(pluXml);
        SqlCore.Update(clipDb);
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Заполнить список ПЛУ из XML.
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    private List<WsXmlContentRecord<WsSqlPluModel>> GetXmlPluList(XElement xml) =>
        WsServiceContentUtils.GetNodesListCore<WsSqlPluModel>(xml, WsLocaleCore.WebService.XmlItemNomenclature, (xmlNode, itemXml) =>
        {
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsGroup));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ParentGroupGuid");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.FullName));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.CategoryGuid));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BrandGuid));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.MeasurementType));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.GroupGuid));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.AttachmentsCount));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BoxTypeGuid));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BoxTypeName));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BoxTypeWeight));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.PackageTypeGuid));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.PackageTypeName));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.PackageTypeWeight));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.ClipTypeGuid));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.ClipTypeName));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.ClipTypeWeight));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "PluNumber");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Description));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ShelfLife");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Gtin));
        });

    /// <summary>
    /// Сохранить ПЛУ в БД.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void SavePlu(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        try
        {
            // Проверка на пустой Uid1C.
            if (Equals(pluXml.Uid1C, Guid.Empty))
            {
                WsServiceResponseUtils.AddResponseExceptionString(response, pluXml.Uid1C,
                    $"{WsLocaleCore.WebService.IsEmpty} {WsLocaleCore.WebService.FieldGuid}!");
                return;
            }
            // Поиск по Uid1C и Number.
            WsSqlPluModel? pluDb = ContextCache.Plus.Find(item =>
                Equals(item.Uid1C, pluXml.Uid1C) && Equals(item.Number, pluXml.Number));
            if (pluDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUpdateUtils.UpdateItemDb(response, pluXml, pluDb);
                return;
            }
            // Поиск по Number.
            pluDb = ContextCache.Plus.Find(item =>
                !Equals(item.Uid1C, pluXml.Uid1C) && Equals(item.Number, pluXml.Number));
            if (pluDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUpdateUtils.UpdateItemDb(response, pluXml, pluDb);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUpdateUtils.SaveItemDb(response, pluXml, true, pluXml.Uid1C);
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.Plus);
        }
        catch (Exception ex)
        {
            WsServiceResponseUtils.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить связь ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void SavePluFks(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.ParentGuid, Guid.Empty)) return;
            // Получить ПЛУ.
            WsSqlPluModel pluDb = WsServiceGetUtils.GetItemPlu(WsSqlEnumContextType.Cache, response, 
                pluXml.Uid1C, pluXml.Uid1C, WsLocaleCore.WebService.FieldNomenclature);
            if (pluDb.IsNotExists) return;
            // Получить группу ПЛУ из БД.
            WsSqlPluModel parentDb = WsServiceGetUtils.GetItemPlu(WsSqlEnumContextType.Cache, response, 
                pluXml.ParentGuid, pluXml.Uid1C, WsLocaleCore.WebService.FieldGroup);
            if (parentDb.IsNotExists) return;
            // Получить категорию ПЛУ из БД.
            WsSqlPluModel categoryDb = WsServiceGetUtils.GetItemPlu(WsSqlEnumContextType.Cache, response, 
                pluXml.CategoryGuid, pluXml.Uid1C, WsLocaleCore.WebService.FieldGroup1Level);
            // Связь ПЛУ.
            WsSqlPluFkModel pluFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Parent = parentDb,
                Category = categoryDb.IsExists ? categoryDb : null
            };
            // Поиск по Identity.
            WsSqlPluFkModel pluFkDb = WsServiceGetUtils.GetItemPluFk(WsSqlEnumContextType.Cache, response, pluFk.Plu.Uid1C,
                pluFk.Parent.Uid1C, pluFk.Category?.Uid1C, pluXml.Uid1C, "Связь ПЛУ");
            if (pluFkDb.IsExists)
            {
                // Обновить найденную запись.
                WsServiceUpdateUtils.UpdatePluFkDb(response, pluXml.Uid1C, pluFk, pluFkDb, false);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUpdateUtils.SaveItemDb(response, pluFk, false, pluXml.Uid1C);
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.PluFks);
        }
        catch (Exception ex)
        {
            WsServiceResponseUtils.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить коробку.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void SaveBox(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        try
        {
            // Поиск по Uid1C.
            WsSqlBoxModel boxDb = WsServiceGetUtils.GetBox(WsSqlEnumContextType.Cache, response,
                pluXml.BoxTypeGuid, pluXml.Uid1C, "Коробка");
            if (boxDb.IsExists)
            {
                // Обновить найденную запись.
                UpdateBoxDb(pluXml, boxDb);
                return;
            }
            // Не найдено -> Добавить новую запись.
            boxDb.UpdateProperties(pluXml);
            WsServiceUpdateUtils.SaveItemDb(response, boxDb, false, pluXml.Uid1C);
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.Boxes);
        }
        catch (Exception ex)
        {
            WsServiceResponseUtils.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить пакет.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void SaveBundle(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        try
        {
            // Поиск по Uid1C.
            WsSqlBundleModel bundleDb = WsServiceGetUtils.GetItemBundle(WsSqlEnumContextType.Cache, response,
                pluXml.PackageTypeGuid, pluXml.Uid1C, WsLocaleCore.WebService.FieldBundle);
            if (bundleDb.IsExists)
            {
                // Обновить найденную запись.
                UpdateBundleDb(pluXml, bundleDb);
                return;
            }
            // Не найдено -> Добавить новую запись.
            bundleDb.UpdateProperties(pluXml);
            WsServiceUpdateUtils.SaveItemDb(response, bundleDb, false, pluXml.Uid1C);
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.Bundles);
        }
        catch (Exception ex)
        {
            WsServiceResponseUtils.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить клипсу.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void SaveClip(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        try
        {
            // Поиск по Uid1C.
            WsSqlClipModel clipDb = WsServiceGetUtils.GetItemClip(WsSqlEnumContextType.Cache, response,
                pluXml.ClipTypeGuid, pluXml.Uid1C, "Клипса");
            if (clipDb.IsExists)
            {
                // Обновить найденную запись.
                UpdateClipDb(pluXml, clipDb);
                return;
            }
            // Не найдено -> Добавить новую запись.
            clipDb.UpdateProperties(pluXml);
            WsServiceUpdateUtils.SaveItemDb(response, clipDb, false, pluXml.Uid1C);
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.Clips);
        }
        catch (Exception ex)
        {
            WsServiceResponseUtils.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить связь бренда.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void SavePluBrandFk(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.BrandGuid, Guid.Empty)) return;
            // Получить ПЛУ.
            WsSqlPluModel pluDb = WsServiceGetUtils.GetItemPlu(WsSqlEnumContextType.Cache, response,
                pluXml.Uid1C, pluXml.Uid1C, WsLocaleCore.WebService.FieldNomenclature);
            if (pluDb.IsNotExists) return;
            // Получить бренд.
            WsSqlBrandModel brandDb = WsServiceGetUtils.GetItemBrand(WsSqlEnumContextType.Cache, response,
                pluXml.BrandGuid, pluXml.Uid1C, WsLocaleCore.WebService.FieldBrand);
            if (brandDb.IsNotExists) return;
            // Связь бренда и ПЛУ.
            WsSqlPluBrandFkModel pluBrandFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Brand = brandDb
            };
            // Поиск по Identity | UQ_PLUS_CLIP_PLU_FK.
            WsSqlPluBrandFkModel pluBrandFkDb = WsServiceGetUtils.GetItemPluBrandFk(WsSqlEnumContextType.Cache, response,
                pluBrandFk.Plu.Uid1C, pluBrandFk.Brand.Uid1C, pluXml.Uid1C, "Связь бренда ПЛУ");
            if (pluBrandFkDb.IsExists)
            {
                // Обновить найденную запись.
                WsServiceUpdateUtils.UpdatePluBrandFkDb(response, pluXml.Uid1C, pluBrandFk, pluBrandFkDb, false);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUpdateUtils.SaveItemDb(response, pluBrandFk, false, pluXml.Uid1C);
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.PluBrandsFks);
        }
        catch (Exception ex)
        {
            WsServiceResponseUtils.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить связь клипсы ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void SavePluClipFk(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.ClipTypeGuid, Guid.Empty)) return;
            // Получить ПЛУ.
            WsSqlPluModel pluDb = WsServiceGetUtils.GetItemPlu(WsSqlEnumContextType.Cache, response,
                pluXml.Uid1C, pluXml.Uid1C, WsLocaleCore.WebService.FieldNomenclature);
            if (pluDb.IsNotExists) return;
            // Получить клипсу.
            WsSqlClipModel clipDb = WsServiceGetUtils.GetItemClip(WsSqlEnumContextType.Cache, response, 
                pluXml.ClipTypeGuid, pluXml.Uid1C, WsLocaleCore.WebService.FieldClip);
            if (clipDb.IsNotExists) return;
            // Связь клипсы и ПЛУ.
            WsSqlPluClipFkModel pluClipFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Clip = clipDb
            };
            // Поиск по Identity | UQ_PLUS_CLIP_PLU_FK.
            WsSqlPluClipFkModel pluClipFkDb = WsServiceGetUtils.GetItemPluClipFk(WsSqlEnumContextType.Cache, response,
                pluClipFk.Plu.Uid1C, pluClipFk.Clip.Uid1C, pluXml.Uid1C, "Связь клипсы ПЛУ");
            if (pluClipFkDb.IsExists)
            {
                // Обновить найденную запись.
                WsServiceUpdateUtils.UpdatePluClipFkDb(response, pluXml.Uid1C, pluClipFk, pluClipFkDb, false);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUpdateUtils.SaveItemDb(response, pluClipFk, false, pluXml.Uid1C);
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.PluClipsFks);
        }
        catch (Exception ex)
        {
            WsServiceResponseUtils.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить связь пакета и ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    /// <returns></returns>
    private WsSqlPluBundleFkModel AddResponsePluBundleFk(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        WsSqlPluBundleFkModel pluBundleFk = new();
        try
        {
            // Получить ПЛУ.
            WsSqlPluModel pluDb = WsServiceGetUtils.GetItemPlu(WsSqlEnumContextType.Cache, response, 
                pluXml.Uid1C, pluXml.Uid1C, WsLocaleCore.WebService.FieldNomenclature);
            if (pluDb.IsNotExists) return pluBundleFk;
            // Получить пакет.
            WsSqlBundleModel bundleDb = WsServiceGetUtils.GetItemBundle(WsSqlEnumContextType.Cache, response, 
                pluXml.PackageTypeGuid, pluXml.Uid1C, WsLocaleCore.WebService.FieldBundle);
            if (bundleDb.IsNotExists) return pluBundleFk;
            // Связь пакета и ПЛУ.
            pluBundleFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Bundle = bundleDb,
            };
            // Поиск по Identity | UQ_BUNDLES_FK.
            WsSqlPluBundleFkModel? pluBundleFkDb = ContextCache.PlusBundlesFks.Find(item => Equals(item.Plu.Uid1C, pluBundleFk.Plu.Uid1C));
            if (pluBundleFkDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUpdateUtils.UpdatePluBundleFkDb(response, pluXml.Uid1C, pluBundleFk, pluBundleFkDb, false);
                return pluBundleFkDb;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUpdateUtils.SaveItemDb(response, pluBundleFk, false, pluXml.Uid1C);
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.PluBundlesFks);
        }
        catch (Exception ex)
        {
            WsServiceResponseUtils.AddResponseException(response, pluXml.Uid1C, ex);
        }
        return pluBundleFk;
    }

    /// <summary>
    /// Сохранить связь вложенности и ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="pluXml"></param>
    private void SavePluNestingFk(WsResponse1CShortModel response, Guid uid1C, WsSqlPluModel pluXml)
    {
        try
        {
            // Получить связь пакета и ПЛУ.
            WsSqlPluBundleFkModel pluBundleFk = WsServiceGetUtils.GetItemPluBundleFk(WsSqlEnumContextType.Cache, response,
                uid1C, pluXml.Uid1C, "Связь пакета и ПЛУ");
            if (pluBundleFk.IsNotExists) return;
            // Получить коробку.
            WsSqlBoxModel boxDb = WsServiceGetUtils.GetBox(WsSqlEnumContextType.Cache, response, 
                pluXml.BoxTypeGuid, pluXml.Uid1C, "Коробка");
            if (boxDb.IsNotExists) return;
            // Связь вложенности ПЛУ.
            WsSqlPluNestingFkModel pluNestingFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                PluBundle = pluBundleFk,
                Box = boxDb,
                BundleCount = pluXml.AttachmentsCount,
                IsDefault = true,
            };
            // Поиск по Identity | UQ_PLUS_NESTING_FK.
            WsSqlViewPluNestingModel? pluNestingFkDb = ContextCache.ViewPlusNesting.Find(item =>
                Equals(item.BoxUid1C, pluNestingFk.Box.Uid1C) &&
                Equals(item.PluUid1C, pluNestingFk.PluBundle.Plu.Uid1C) &&
                Equals(item.BundleUid1C, pluNestingFk.PluBundle.Bundle.Uid1C) &&
                Equals(item.BundleCount, pluXml.AttachmentsCount));
            if (pluNestingFkDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUpdateUtils.UpdatePluNestingFk(response, pluXml.Uid1C, pluNestingFk, pluNestingFkDb, false);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUpdateUtils.SaveItemDb(response, pluNestingFk, false, pluXml.Uid1C);
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.ViewPlusNesting);
        }
        catch (Exception ex)
        {
            WsServiceResponseUtils.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    private string[] GetPluPropertiesArray() => new[]
    {
        nameof(WsSqlPluModel.BoxTypeGuid),
        nameof(WsSqlPluModel.BoxTypeName),
        nameof(WsSqlPluModel.BoxTypeWeight),
        nameof(WsSqlPluModel.BrandGuid),
        nameof(WsSqlPluModel.CategoryGuid),
        nameof(WsSqlPluModel.ClipTypeGuid),
        nameof(WsSqlPluModel.ClipTypeName),
        nameof(WsSqlPluModel.ClipTypeWeight),
        nameof(WsSqlPluModel.Code),
        nameof(WsSqlPluModel.Description),
        nameof(WsSqlPluModel.FullName),
        nameof(WsSqlPluModel.GroupGuid),
        nameof(WsSqlPluModel.IdentityValueUid),
        nameof(WsSqlPluModel.IsCheckWeight),
        nameof(WsSqlPluModel.IsGroup),
        nameof(WsSqlPluModel.IsMarked),
        nameof(WsSqlPluModel.MeasurementType),
        nameof(WsSqlPluModel.Name),
        nameof(WsSqlPluModel.Number),
        nameof(WsSqlPluModel.PackageTypeGuid),
        nameof(WsSqlPluModel.PackageTypeName),
        nameof(WsSqlPluModel.PackageTypeWeight),
        nameof(WsSqlPluModel.ParentGuid),
        nameof(WsSqlPluModel.ShelfLifeDays),
    };

    /// <summary>
    /// Загрузить номенклатуру и получить ответ.
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponsePlus(XElement xml, string format, bool isDebug, ISessionFactory sessionFactory) =>
        WsServiceResponseUtils.NewResponse1CCore<WsResponse1CShortModel>(response =>
        {
            // Заполнить таблицу связей разрешённых для загрузки ПЛУ из 1С.
            WsServiceUpdateUtils.FillPlus1CFksDb();
            // Загрузить кэш.
            ContextCache.Load();
            // Заполнить список ПЛУ из XML.
            List<WsXmlContentRecord<WsSqlPluModel>> plusXml = GetXmlPluList(xml);
            WsSqlPluValidator pluValidator = new(false, false);
            // Цикл по всем XML-номенклатурам.
            foreach (WsXmlContentRecord<WsSqlPluModel> record in plusXml)
            {
                WsSqlPluModel itemXml = record.Item;
                // Обновить таблицу связей ПЛУ для обмена.
                List<WsSqlPlu1CFkModel> plus1CFksDb = WsServiceUpdateUtils.UpdatePlus1CFksDb(response, record);
                // Проверить корректность группы и номера ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                    WsServiceCheckUtils.CheckCorrectPluNumberForNonGroup(itemXml);
                // Проверить разрешение обмена для ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                    WsServiceCheckUtils.CheckEnabledPlu(itemXml, plus1CFksDb);
                // Проверить валидацию ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                    CheckPluValidation(itemXml, pluValidator);
                // Проверить дубликат номера ПЛУ для не групп.
                if (itemXml.ParseResult.IsStatusSuccess)
                    CheckPluDublicateForNonGroup(response, itemXml);
                // Сохранить ПЛУ в БД.
                if (itemXml.ParseResult.IsStatusSuccess)
                    SavePlu(response, itemXml);
                // Сохранить связь ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                    SavePluFks(response, itemXml);
                // Сохранить коробку.
                if (itemXml.ParseResult.IsStatusSuccess)
                    SaveBox(response, itemXml);
                // Сохранить пакет.
                if (itemXml.ParseResult.IsStatusSuccess)
                    SaveBundle(response, itemXml);
                // Сохранить связь бренда.
                if (itemXml.ParseResult.IsStatusSuccess)
                    SavePluBrandFk(response, itemXml);
                // Сохранить клипсу.
                if (itemXml.ParseResult.IsStatusSuccess)
                    SaveClip(response, itemXml);
                // Сохранить связь клипсы ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                    SavePluClipFk(response, itemXml);
                // Сохранить связь пакета и ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                {
                    WsSqlPluBundleFkModel pluBundleFk = AddResponsePluBundleFk(response, itemXml);
                    // Сохранить связь вложенности и ПЛУ.
                    if (itemXml.ParseResult.IsStatusSuccess)
                    {
                        // TODO: FIX HERE
                        //if (itemXml.ParseResult.IsStatusSuccess)
                        //{
                        //    itemXml.ParseResult.Status = WsEnumParseStatus.Error;
                        //    itemXml.ParseResult.Exception =
                        //        WsLocaleCore.WebService.FieldPluNumberTemplate(itemXml.Number) + WsLocaleCore.WebService.Underdevelopment(90);
                        //}
                        SavePluNestingFk(response, pluBundleFk.Plu.Uid1C, itemXml);
                    }
                }
                // Исключение.
                if (itemXml.ParseResult.IsStatusError)
                    WsServiceResponseUtils.AddResponseExceptionString(response, itemXml.Uid1C,
                        itemXml.ParseResult.Exception, itemXml.ParseResult.InnerException);
            }
        }, format, isDebug, sessionFactory);

    /// <summary>
    /// Проверить валидацию ПЛУ.
    /// </summary>
    /// <param name="itemXml"></param>
    /// <param name="pluValidator"></param>
    private void CheckPluValidation(WsSqlPluModel itemXml, WsSqlPluValidator pluValidator)
    {
        ValidationResult validation = pluValidator.Validate(itemXml);
        if (!validation.IsValid)
        {
            string[] pluProperties = GetPluPropertiesArray();
            foreach (ValidationFailure error in validation.Errors)
            {
                if (pluProperties.Contains(error.PropertyName) &&
                    !itemXml.ParseResult.Exception.Contains(error.PropertyName))
                    WsServiceContentUtils.SetItemParseResultException(itemXml, error.PropertyName);
            }
        }
    }

    /// <summary>
    /// Проверить дубликат номера ПЛУ для не групп.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void CheckPluDublicateForNonGroup(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        // Пропуск групп с нулевым номером.
        if (Equals(pluXml.Number, (short)0)) return;
        List<WsSqlPluModel> plusNumberDb = ContextCache.Plus.FindAll(item => Equals(item.Number, pluXml.Number));
        if (plusNumberDb.Count > 1)
        {
            WsServiceResponseUtils.AddResponseExceptionString(response, pluXml.Uid1C,
                WsLocaleCore.WebService.FieldPluNumberTemplate(pluXml.Number) + WsLocaleCore.WebService.Dublicate +
                $" {WsLocaleCore.WebService.FieldCodes} '{string.Join(',', plusNumberDb.Select(item => item.Code).ToList())}'");
            pluXml.ParseResult.Status = WsEnumParseStatus.Error;
        }
    }

    #endregion
}