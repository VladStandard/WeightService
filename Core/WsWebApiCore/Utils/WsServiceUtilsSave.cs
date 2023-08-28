namespace WsWebApiCore.Utils;

/// <summary>
/// Утилиты сохранения данных веб-сервиса.
/// </summary>
public static class WsServiceUtilsSave
{
    #region Public and private methods

    /// <summary>
    /// Сохранить бренд в БД.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="brandXml"></param>
    public static void SaveBrand(WsResponse1CShortModel response, WsSqlBrandModel brandXml)
    {
        try
        {
            // Поиск по Uid1C.
            WsSqlBrandModel? brandDb = WsServiceUtils.ContextCache.Brands.Find(item => Equals(item.Uid1C, brandXml.IdentityValueUid));
            if (brandDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdateBrandDb(response, brandXml.Uid1C, brandXml, brandDb, true);
                return;
            }
            // Поиск по Code.
            brandDb = WsServiceUtils.ContextCache.Brands.Find(item => Equals(item.Code, brandXml.Code));
            if (brandDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdateBrandDb(response, brandXml.Uid1C, brandXml, brandDb, true);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUtilsUpdate.SaveItemDb(response, brandXml, true, brandXml.Uid1C);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.Brands);
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, brandXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить ПЛУ в БД.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    public static void SavePlu(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        try
        {
            // Проверка на пустой Uid1C.
            if (Equals(pluXml.Uid1C, Guid.Empty))
            {
                WsServiceUtilsResponse.AddResponseExceptionString(response, pluXml.Uid1C,
                    $"{WsLocaleCore.WebService.IsEmpty} {WsLocaleCore.WebService.FieldGuid}!");
                return;
            }
            // Поиск по Uid1C и Number.
            WsSqlPluModel? pluDb = WsServiceUtils.ContextCache.Plus.Find(item =>
                Equals(item.Uid1C, pluXml.Uid1C) && Equals(item.Number, pluXml.Number));
            if (pluDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdateItemDb(response, pluXml, pluDb);
                return;
            }
            // Поиск по Number.
            pluDb = WsServiceUtils.ContextCache.Plus.Find(item =>
                !Equals(item.Uid1C, pluXml.Uid1C) && Equals(item.Number, pluXml.Number));
            if (pluDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdateItemDb(response, pluXml, pluDb);
                // Обновить кэш.
                WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.Plus);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUtilsUpdate.SaveItemDb(response, pluXml, true, pluXml.Uid1C);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.Plus);
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить связь ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    public static void SavePluFks(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.ParentGuid, Guid.Empty)) return;
            // Получить ПЛУ.
            WsSqlPluModel pluDb = WsServiceUtilsGet.GetItemPlu(WsSqlEnumContextType.Cache, response,
                pluXml.Uid1C, pluXml.Uid1C, WsLocaleCore.WebService.FieldNomenclature);
            if (pluDb.IsNotExists) return;
            // Получить группу ПЛУ из БД.
            WsSqlPluModel parentDb = WsServiceUtilsGet.GetItemPlu(WsSqlEnumContextType.Cache, response,
                pluXml.ParentGuid, pluXml.Uid1C, WsLocaleCore.WebService.FieldGroup);
            if (parentDb.IsNotExists) return;
            // Получить категорию ПЛУ из БД.
            WsSqlPluModel categoryDb = WsServiceUtilsGet.GetItemPlu(WsSqlEnumContextType.Cache, response,
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
            WsSqlPluFkModel pluFkDb = WsServiceUtilsGet.GetItemPluFk(WsSqlEnumContextType.Cache, response, pluFk.Plu.Uid1C,
                pluFk.Parent.Uid1C, pluFk.Category?.Uid1C, pluXml.Uid1C, "Связь ПЛУ");
            if (pluFkDb.IsExists)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdatePluFkDb(response, pluXml.Uid1C, pluFk, pluFkDb, false);
                // Обновить кэш.
                WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PluFks);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUtilsUpdate.SaveItemDb(response, pluFk, false, pluXml.Uid1C);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PluFks);
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить коробку.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    public static void SaveBox(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        try
        {
            // Поиск по Uid1C.
            WsSqlBoxModel boxDb = WsServiceUtilsGet.GetBox(WsSqlEnumContextType.Cache, response,
                pluXml.BoxTypeGuid, pluXml.Uid1C, "Коробка");
            if (boxDb.IsExists)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdateBoxDb(pluXml, boxDb);
                // Обновить кэш.
                WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.Boxes);
                return;
            }
            // Не найдено -> Добавить новую запись.
            boxDb.UpdateProperties(pluXml);
            WsServiceUtilsUpdate.SaveItemDb(response, boxDb, false, pluXml.Uid1C);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.Boxes);
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить пакет.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    public static void SaveBundle(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        try
        {
            // Поиск по Uid1C.
            WsSqlBundleModel bundleDb = WsServiceUtilsGet.GetItemBundle(WsSqlEnumContextType.Cache, response,
                pluXml.PackageTypeGuid, pluXml.Uid1C, WsLocaleCore.WebService.FieldBundle);
            if (bundleDb.IsExists)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdateBundleDb(pluXml, bundleDb);
                // Обновить кэш.
                WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.Bundles);
                return;
            }
            // Не найдено -> Добавить новую запись.
            bundleDb.UpdateProperties(pluXml);
            WsServiceUtilsUpdate.SaveItemDb(response, bundleDb, false, pluXml.Uid1C);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.Bundles);
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить клипсу.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    public static void SaveClip(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        try
        {
            // Поиск по Uid1C.
            WsSqlClipModel clipDb = WsServiceUtilsGet.GetItemClip(WsSqlEnumContextType.Cache, response,
                pluXml.ClipTypeGuid, pluXml.Uid1C, "Клипса");
            if (clipDb.IsExists)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdateClipDb(pluXml, clipDb);
                // Обновить кэш.
                WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.Clips);
                return;
            }
            // Не найдено -> Добавить новую запись.
            clipDb.UpdateProperties(pluXml);
            WsServiceUtilsUpdate.SaveItemDb(response, clipDb, false, pluXml.Uid1C);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.Clips);
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить связь бренда.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    public static void SavePluBrandFk(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.BrandGuid, Guid.Empty)) return;
            // Получить ПЛУ.
            WsSqlPluModel pluDb = WsServiceUtilsGet.GetItemPlu(WsSqlEnumContextType.Cache, response,
                pluXml.Uid1C, pluXml.Uid1C, WsLocaleCore.WebService.FieldNomenclature);
            if (pluDb.IsNotExists) return;
            // Получить бренд.
            WsSqlBrandModel brandDb = WsServiceUtilsGet.GetItemBrand(WsSqlEnumContextType.Cache, response,
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
            WsSqlPluBrandFkModel pluBrandFkDb = WsServiceUtilsGet.GetItemPluBrandFk(WsSqlEnumContextType.Cache, response,
                pluBrandFk.Plu.Uid1C, pluBrandFk.Brand.Uid1C, pluXml.Uid1C, "Связь бренда ПЛУ");
            if (pluBrandFkDb.IsExists)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdatePluBrandFkDb(response, pluXml.Uid1C, pluBrandFk, pluBrandFkDb, false);
                // Обновить кэш.
                WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PluBrandsFks);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUtilsUpdate.SaveItemDb(response, pluBrandFk, false, pluXml.Uid1C);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PluBrandsFks);
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить связь клипсы ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    public static void SavePluClipFk(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.ClipTypeGuid, Guid.Empty)) return;
            // Получить ПЛУ.
            WsSqlPluModel pluDb = WsServiceUtilsGet.GetItemPlu(WsSqlEnumContextType.Cache, response,
                pluXml.Uid1C, pluXml.Uid1C, WsLocaleCore.WebService.FieldNomenclature);
            if (pluDb.IsNotExists) return;
            // Получить клипсу.
            WsSqlClipModel clipDb = WsServiceUtilsGet.GetItemClip(WsSqlEnumContextType.Cache, response,
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
            WsSqlPluClipFkModel pluClipFkDb = WsServiceUtilsGet.GetItemPluClipFk(WsSqlEnumContextType.Cache, response,
                pluClipFk.Plu.Uid1C, pluClipFk.Clip.Uid1C, pluXml.Uid1C, "Связь клипсы ПЛУ");
            if (pluClipFkDb.IsExists)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdatePluClipFkDb(response, pluXml.Uid1C, pluClipFk, pluClipFkDb, false);
                // Обновить кэш.
                WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PluClipsFks);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUtilsUpdate.SaveItemDb(response, pluClipFk, false, pluXml.Uid1C);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PluClipsFks);
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить связь пакета и ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    /// <returns></returns>
    public static WsSqlPluBundleFkModel SavePluBundleFk(WsResponse1CShortModel response, WsSqlPluModel pluXml)
    {
        WsSqlPluBundleFkModel pluBundleFk = WsServiceUtils.ContextManager.PluBundleFkRepository.GetNewItem();
        try
        {
            // Получить ПЛУ.
            WsSqlPluModel pluDb = WsServiceUtilsGet.GetItemPlu(WsSqlEnumContextType.Cache, response,
                pluXml.Uid1C, pluXml.Uid1C, WsLocaleCore.WebService.FieldNomenclature);
            if (pluDb.IsNotExists) return pluBundleFk;
            // Получить пакет.
            WsSqlBundleModel bundleDb = WsServiceUtilsGet.GetItemBundle(WsSqlEnumContextType.Cache, response,
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
            WsSqlPluBundleFkModel? pluBundleFkDb = WsServiceUtils.ContextCache.PlusBundlesFks.Find(item => Equals(item.Plu.Uid1C, pluBundleFk.Plu.Uid1C));
            if (pluBundleFkDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdatePluBundleFkDb(pluBundleFk, pluBundleFkDb);
                // Обновить кэш.
                WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PluBundlesFks);
                return pluBundleFkDb;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUtilsUpdate.SaveItemDb(response, pluBundleFk, false, pluXml.Uid1C);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PluBundlesFks);
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, pluXml.Uid1C, ex);
        }
        return pluBundleFk;
    }

    /// <summary>
    /// Сохранить вложенность ПЛУ по-умолчанию.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluBundleFk"></param>
    /// <param name="pluXml"></param>
    public static WsSqlPluNestingFkModel SavePluNestingFkDefault(WsResponse1CShortModel response, WsSqlPluBundleFkModel pluBundleFk, 
        WsSqlPluModel pluXml)
    {
        try
        {
            // Получить коробку.
            WsSqlBoxModel boxDb = WsServiceUtilsGet.GetBox(WsSqlEnumContextType.Cache, response,
                pluXml.BoxTypeGuid, pluXml.Uid1C, "Коробка");
            if (boxDb.IsNotExists) return WsServiceUtils.ContextManager.PluNestingFkRepository.GetNewItem();
            // Связь вложенности ПЛУ.
            WsSqlPluNestingFkModel pluNestingFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                PluBundle = pluBundleFk,
                Box = boxDb,
                BundleCount = pluXml.AttachmentsCount,
                IsDefault = true,
                IsMarked = pluXml.IsMarked,
            };
            // Поиск представления.
            WsSqlPluNestingFkModel? pluNestingFkDb = WsServiceUtils.ContextCache.PlusNestingFks.Find(item =>
                Equals(item.Box.Uid1C, pluNestingFk.Box.Uid1C) &&
                Equals(item.PluBundle.Plu.Uid1C, pluNestingFk.PluBundle.Plu.Uid1C) &&
                Equals(item.PluBundle.Bundle.Uid1C, pluNestingFk.PluBundle.Bundle.Uid1C) &&
                Equals(item.BundleCount, pluXml.AttachmentsCount));
            if (pluNestingFkDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdatePluNestingFk(pluNestingFkDb, pluNestingFk);
                // Обновить кэш.
                WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PlusNestingFks);
                WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.ViewPlusNesting);
                return pluNestingFkDb;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUtilsUpdate.SaveItemDb(response, pluNestingFk, false, pluXml.Uid1C);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PlusNestingFks);
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.ViewPlusNesting);
            return pluNestingFk;
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, pluXml.Uid1C, ex);
        }
        return WsServiceUtils.ContextManager.PluNestingFkRepository.GetNewItem();
    }

    /// <summary>
    /// Сохранить остальные вложенности и ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluNestingFkDefault"></param>
    /// <param name="pluXml"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void SavePluNestingFkOther(WsResponse1CShortModel response, WsSqlPluNestingFkModel pluNestingFkDefault, WsSqlPluModel pluXml)
    {
        // Получить список вложенностей ПЛУ.
        List<WsSqlPluNestingFkModel> pluNestingFks = WsServiceUtilsGet.GetListPluNestingFks(
            WsSqlEnumContextType.Cache, response, pluXml.Uid1C, pluXml.Uid1C, "Список вложенностей ПЛУ");
        // Отфильтровать список вложенностей ПЛУ.
        List<WsSqlPluNestingFkModel> pluNestingFksOther = 
            pluNestingFks.Where(item => !item.IdentityValueUid.Equals(pluNestingFkDefault.IdentityValueUid)).ToList();
        // Деактивировать все прочие вложенности.
        foreach (WsSqlPluNestingFkModel pluNestingFk in pluNestingFksOther)
        {
            pluNestingFk.IsDefault = false;
            // Сохранить связь вложенности и ПЛУ.
            if (pluXml.ParseResult.IsStatusSuccess) SavePluNestingFk(response, pluNestingFk);
        }
    }

    /// <summary>
    /// Сохранить связь вложенности и ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluNestingFk"></param>
    public static void SavePluNestingFk(WsResponse1CShortModel response, WsSqlPluNestingFkModel pluNestingFk)
    {
        try
        {
            if (pluNestingFk.IsNotNew)
                // Обновить найденную запись.
                WsServiceUtils.SqlCore.Update(pluNestingFk);
            else
                // Добавить запись.
                WsServiceUtils.SqlCore.Save(pluNestingFk);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PlusNestingFks);
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.ViewPlusNesting);
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, pluNestingFk.PluBundle.Plu.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить характеристику ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="characteristicXml"></param>
    public static void SavePluCharacteristics(WsResponse1CShortModel response, WsSqlPluCharacteristicModel characteristicXml)
    {
        try
        {
            // Получить характеристику ПЛУ.
            WsSqlPluCharacteristicModel characteristicDb = WsServiceUtilsGet.GetItemPluCharacteristic(
                WsSqlEnumContextType.Cache, response, characteristicXml.Uid1C, 
                characteristicXml.Uid1C, WsLocaleCore.WebService.FieldNomenclatureCharacteristic);
            if (characteristicDb.IsExists)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdatePluCharacteristic(response, characteristicXml.IdentityValueUid, characteristicXml, characteristicDb, true);
                // Обновить кэш.
                WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PluCharacteristics);
                return;
            };
            // Не найдено -> Добавить новую запись.
            WsServiceUtilsUpdate.SaveItemDb(response, characteristicXml, true, characteristicXml.Uid1C);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PluCharacteristics);
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, characteristicXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить связь характеристики ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="characteristicXml"></param>
    public static void SavePluCharacteristicsFks(WsResponse1CShortModel response, WsSqlPluCharacteristicModel characteristicXml)
    {
        try
        {
            if (Equals(characteristicXml.NomenclatureGuid, Guid.Empty)) return;
            // Получить ПЛУ.
            WsSqlPluModel pluDb = WsServiceUtilsGet.GetItemPlu(WsSqlEnumContextType.Cache, response,
                characteristicXml.NomenclatureGuid, characteristicXml.Uid1C, WsLocaleCore.WebService.FieldNomenclature);
            if (pluDb.IsNotExists) return;
            // Получить характеристику ПЛУ.
            WsSqlPluCharacteristicModel characteristicDb = WsServiceUtilsGet.GetItemPluCharacteristic(
                WsSqlEnumContextType.Cache, response, characteristicXml.Uid1C, 
                characteristicXml.Uid1C, WsLocaleCore.WebService.FieldNomenclatureCharacteristic);
            if (characteristicDb.IsNotExists) return;
            // Связь характеристики и ПЛУ.
            WsSqlPluCharacteristicsFkModel pluCharacteristicsFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Characteristic = characteristicDb,
            };
            // Поиск по Identity.
            WsSqlPluCharacteristicsFkModel pluCharacteristicFkDb = 
                WsServiceUtilsGet.GetItemPluCharacteristicFk(WsSqlEnumContextType.Cache, response, 
                    pluCharacteristicsFk.Characteristic.Uid1C, pluCharacteristicsFk.Plu.Uid1C, 
                    "Связь характеристики ПЛУ");
            if (pluCharacteristicFkDb.IsExists)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdatePluCharacteristicFk(response, characteristicXml.Uid1C,
                    pluCharacteristicsFk, pluCharacteristicFkDb, false);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUtilsUpdate.SaveItemDb(response, pluCharacteristicsFk, false, characteristicXml.Uid1C);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PluCharacteristicsFks);
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, characteristicXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить группу в таблицу БД.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluGroupXml"></param>
    public static void SavePluGroups(WsResponse1CShortModel response, WsSqlPluGroupModel pluGroupXml)
    {
        try
        {
            // Поиск по Uid1C.
            WsSqlPluGroupModel? pluGroupDb = WsServiceUtils.ContextCache.PlusGroups.Find(item => Equals(item.Uid1C, pluGroupXml.IdentityValueUid));
            if (pluGroupDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdatePluGroupDb(response, pluGroupXml.Uid1C, pluGroupXml, pluGroupDb, true);
                return;
            }
            // Поиск по Code.
            pluGroupDb = WsServiceUtils.ContextCache.PlusGroups.Find(item => Equals(item.Code, pluGroupXml.Code));
            if (pluGroupDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdatePluGroupDb(response, pluGroupXml.Uid1C, pluGroupXml, pluGroupDb, true);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUtilsUpdate.SaveItemDb(response, pluGroupXml, true, pluGroupXml.Uid1C);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PluGroups);
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, pluGroupXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Сохранить связь группы в таблицу БД.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluGroupXml"></param>
    public static void SavePluGroupsFks(WsResponse1CShortModel response, WsSqlPluGroupModel pluGroupXml)
    {
        try
        {
            if (Equals(pluGroupXml.ParentGuid, Guid.Empty)) return;
            // Родитель.
            WsSqlPluGroupModel parent = new() { IdentityValueUid = pluGroupXml.ParentGuid };
            parent = WsServiceUtils.SqlCore.GetItemByIdentity<WsSqlPluGroupModel>(parent.Identity);
            if (parent.IsNew)
            {
                WsServiceUtilsResponse.AddResponseException(response, pluGroupXml.Uid1C, new($"Parent PLU group for '{pluGroupXml.ParentGuid}' {WsLocaleCore.WebService.IsNotFound}!"));
                return;
            }
            // Группа.
            WsSqlPluGroupModel pluGroup = new() { IdentityValueUid = pluGroupXml.IdentityValueUid };
            pluGroup = WsServiceUtils.SqlCore.GetItemByIdentity<WsSqlPluGroupModel>(pluGroup.Identity);
            if (pluGroup.IsNew)
            {
                WsServiceUtilsResponse.AddResponseException(response, pluGroupXml.Uid1C, new($"PLU group for '{pluGroupXml.ParentGuid}' {WsLocaleCore.WebService.IsNotFound}!"));
                return;
            }
            // Связь группы.
            WsSqlPluGroupFkModel itemGroupFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                PluGroup = pluGroup,
                Parent = parent
            };
            // Поиск по UID.
            WsSqlPluGroupFkModel? itemDb = WsServiceUtils.ContextCache.PlusGroupsFks.Find(x =>
                x.PluGroup.IdentityValueUid.Equals(itemGroupFk.PluGroup.IdentityValueUid) &&
                x.Parent.IdentityValueUid.Equals(itemGroupFk.Parent.IdentityValueUid));
            if (itemDb is not null)
            {
                // Обновить найденную запись.
                WsServiceUtilsUpdate.UpdatePluGroupFkDb(response, pluGroupXml.Uid1C, itemGroupFk, itemDb, false);
                return;
            }
            // Не найдено -> Добавить новую запись.
            WsServiceUtilsUpdate.SaveItemDb(response, itemGroupFk, false, pluGroupXml.Uid1C);
            // Обновить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.PluGroupsFks);
        }
        catch (Exception ex)
        {
            WsServiceUtilsResponse.AddResponseException(response, pluGroupXml.Uid1C, ex);
        }
    }

    #endregion
}