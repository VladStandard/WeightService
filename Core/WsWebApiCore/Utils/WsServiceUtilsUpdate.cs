namespace WsWebApiCore.Utils;

/// <summary>
/// Утилиты обновления данных веб-сервиса.
/// </summary>
public static class WsServiceUtilsUpdate
{
    #region Public and private methods

    /// <summary>
    /// Обновить запись 1C в БД.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    public static void UpdateItemDb<T>(WsResponse1CShortModel response, T itemXml, T? itemDb) where T : WsSqlTable1CBase
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        WsServiceUtils.SqlCore.Update(itemDb);
        response.Successes.Add(new(itemXml.Uid1C));
    }

    /// <summary>
    /// Save the record to the database.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="item"></param>
    /// <param name="isCounter"></param>
    /// <param name="uid1C"></param>
    public static void SaveItemDb<T>(WsResponse1CShortModel response, T item, bool isCounter, Guid uid1C) where T : WsSqlTableBase
    {
        WsServiceUtils.SqlCore.Save(item, item.Identity);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить бренд в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    public static void UpdateBrandDb(WsResponse1CShortModel response, Guid uid1C, WsSqlBrandModel itemXml, WsSqlBrandModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        WsServiceUtils.SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    public static void UpdatePluFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluFkModel itemXml, WsSqlPluFkModel? itemDb, 
        bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        WsServiceUtils.SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь клипсы и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    public static void UpdatePluClipFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluClipFkModel itemXml, 
        WsSqlPluClipFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        WsServiceUtils.SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }
    
    
    /// <summary>
    /// Обновить связь вложенности и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    public static void UpdatePluNestingFk(WsSqlPluNestingFkModel itemDb, WsSqlPluNestingFkModel itemXml)
    {
        if (itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        WsServiceUtils.SqlCore.Update(itemDb);
        //if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Заполнить таблицу связей разрешённых для загрузки ПЛУ из 1С.
    /// </summary>
    public static void FillPlus1CFksDb()
    {
        // Проверить наличие всех связей разрешённых для загрузки ПЛУ из 1С.
        if (WsServiceUtilsCheck.CheckExistsAllPlus1CFksDb()) return;
        // Получить список ПЛУ.
        foreach (WsSqlPluModel plu in WsServiceUtils.ContextManager.PluRepository.GetEnumerable())
        {
            // Обновить данные записи в таблице связей обмена ПЛУ 1С.
            UpdatePlu1CFkDbCore(plu);
        }
    }
    
    /// <summary>
    /// Обновить данные записи в таблице связей обмена ПЛУ 1С.
    /// </summary>
    /// <param name="plu"></param>
    private static void UpdatePlu1CFkDbCore(WsSqlPluModel plu)
    {
        WsSqlPlu1CFkModel? plu1CFkCache =
            WsServiceUtils.ContextCache.Plus1CFks.Find(item => item.Plu.IdentityValueUid.Equals(plu.IdentityValueUid));
        // В кэше не найдено - сохранить.
        if (plu1CFkCache is null)
        {
            WsServiceUtils.SqlCore.Save(new WsSqlPlu1CFkModel(plu));
            // Загрузить кэш.
            WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.Plus1CFks);
        }
        // В кэше найдено - обновить.
        else
        {
            plu1CFkCache.Plu = plu;
            WsSqlPlu1CFkValidator validator = new(true);
            ValidationResult validation = validator.Validate(plu1CFkCache);
            if (!validation.IsValid)
                throw new($"Exception at UpdatePlu1CFkDbCore. Check PLU {plu1CFkCache}!");
            WsServiceUtils.SqlCore.Update(plu1CFkCache);
        }
    }

    public static void UpdateBoxDb(WsSqlPluModel pluXml, WsSqlBoxModel boxDb)
    {
        if (boxDb.IsNew) return;
        boxDb.UpdateProperties(pluXml);
        WsServiceUtils.SqlCore.Update(boxDb);
    }

    public static void UpdateBundleDb(WsSqlPluModel pluXml, WsSqlBundleModel bundleDb)
    {
        if (bundleDb.IsNew) return;
        bundleDb.UpdateProperties(pluXml);
        WsServiceUtils.SqlCore.Update(bundleDb);
    }

    public static void UpdateClipDb(WsSqlPluModel pluXml, WsSqlClipModel clipDb)
    {
        if (clipDb.IsNew) return;
        clipDb.UpdateProperties(pluXml);
        WsServiceUtils.SqlCore.Update(clipDb);
    }

    #endregion
}