// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NHibernate.Hql.Ast.ANTLR.Tree;

namespace WsWebApiCore.Utils;

/// <summary>
/// Утилиты обновления данных веб-сервиса.
/// </summary>
public static class WsServiceUpdateUtils
{
    #region Public and private fields, properties, constructor

    private static WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    private static WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;

    #endregion

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
        SqlCore.Update(itemDb);
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
        SqlCore.Save(item, item.Identity);
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
        SqlCore.Update(itemDb);
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
    public static void UpdatePluFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluFkModel itemXml, WsSqlPluFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
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
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить номенклатурную группу в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    public static void UpdatePluGroupDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluGroupModel itemXml, WsSqlPluGroupModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь номенклатурной группы и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    public static void UpdatePluGroupFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluGroupFkModel itemXml, WsSqlPluGroupFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь бренда и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    public static void UpdatePluBrandFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluBrandFkModel itemXml, WsSqlPluBrandFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь пакета и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    public static void UpdatePluBundleFkDb(WsSqlPluBundleFkModel itemXml, WsSqlPluBundleFkModel? itemDb)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        //if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь номенклатурной характеристики и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    public static void UpdatePluCharacteristicFk(WsResponse1CShortModel response, Guid uid1C, WsSqlPluCharacteristicsFkModel itemXml,
        WsSqlPluCharacteristicsFkModel? itemDb)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        //if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь вложенности и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    public static void UpdatePluNestingFk(WsSqlPluNestingFkModel itemXml, WsSqlPluNestingFkModel? itemDb)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        //if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Заполнить таблицу связей разрешённых для загрузки ПЛУ из 1С.
    /// </summary>
    public static void FillPlus1CFksDb()
    {
        // Проверить наличие всех связей разрешённых для загрузки ПЛУ из 1С.
        if (WsServiceCheckUtils.CheckExistsAllPlus1CFksDb()) return;
        // Получить список ПЛУ.
        foreach (WsSqlPluModel plu in ContextManager.ContextPlus.GetList())
        {
            // Обновить данные записи в таблице связей обмена ПЛУ 1С.
            UpdatePlu1CFkDbCore(plu);
        }
    }

    /// <summary>
    /// Обновить таблицу связей ПЛУ для обмена.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="recordXml"></param>
    internal static List<WsSqlPlu1CFkModel> UpdatePlus1CFksDb<T>(WsResponse1CShortModel response,
        WsXmlContentRecord<T> recordXml) where T : WsSqlTable1CBase, new()
    {
        List<WsSqlPlu1CFkModel> plus1CFksDb = new();
        switch (recordXml)
        {
            // ПЛУ.
            case WsXmlContentRecord<WsSqlPluModel> pluXml:
                plus1CFksDb = WsServiceGetUtils.GetPlus1CFksByGuid1C(pluXml.Item.Uid1C);
                break;
            // Характеристика ПЛУ.
            case WsXmlContentRecord<WsSqlPluCharacteristicModel> pluCharacteristicXml:
                plus1CFksDb = WsServiceGetUtils.GetPlus1CFksByGuid1C(pluCharacteristicXml.Item.NomenclatureGuid);
                break;
        }
        // Обновить таблицу связей ПЛУ для обмена.
        plus1CFksDb.ForEach(item => UpdatePlu1CFkDbCore(response, recordXml, item));
        return plus1CFksDb;
    }

    /// <summary>
    /// Обновить данные записи в таблице связей обмена ПЛУ 1С.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="recordXml"></param>
    /// <param name="plu1CFk"></param>
    private static void UpdatePlu1CFkDbCore<T>(WsResponse1CShortModel response, WsXmlContentRecord<T> recordXml,
        WsSqlPlu1CFkModel plu1CFk) where T : WsSqlTable1CBase, new()
    {
        WsSqlPlu1CFkModel? plu1CFkCache =
            ContextCache.Plus1CFks.Find(item => item.Plu.IdentityValueUid.Equals(plu1CFk.Plu.IdentityValueUid));

        // В кэше не найдено - сохранить.
        if (plu1CFkCache is null)
        {
            plu1CFk.UpdateProperties(recordXml.Content);
            SqlCore.Save(plu1CFk);
            // Загрузить кэш.
            ContextCache.Load(WsSqlEnumTableName.Plus1CFks);
        }
        // В кэше найдено - обновить.
        else
        {
            plu1CFk.RequestDataString = recordXml.Content;
            plu1CFkCache.UpdateProperties(plu1CFk);
            WsSqlPlu1CFkValidator validator = new();
            ValidationResult validation = validator.Validate(plu1CFkCache);
            if (!validation.IsValid)
            {
                if (recordXml is WsXmlContentRecord<WsSqlPluModel> pluXml)
                    WsServiceResponseUtils.AddResponseExceptionString(response, pluXml.Item.Uid1C,
                        string.Join(',', validation.Errors.Select(item => item.ErrorMessage).ToList()));
                else if (recordXml is WsXmlContentRecord<WsSqlPluCharacteristicModel> pluCharacteristicXml)
                    WsServiceResponseUtils.AddResponseExceptionString(response, pluCharacteristicXml.Item.NomenclatureGuid,
                        string.Join(',', validation.Errors.Select(item => item.ErrorMessage).ToList()));
            }
            else
            {
                SqlCore.Update(plu1CFkCache);
            }
        }
    }

    /// <summary>
    /// Обновить данные записи в таблице связей обмена ПЛУ 1С.
    /// </summary>
    /// <param name="plu"></param>
    private static void UpdatePlu1CFkDbCore(WsSqlPluModel plu)
    {
        WsSqlPlu1CFkModel? plu1CFkCache =
            ContextCache.Plus1CFks.Find(item => item.Plu.IdentityValueUid.Equals(plu.IdentityValueUid));
        // В кэше не найдено - сохранить.
        if (plu1CFkCache is null)
        {
            SqlCore.Save(new WsSqlPlu1CFkModel(plu));
            // Загрузить кэш.
            ContextCache.Load(WsSqlEnumTableName.Plus1CFks);
        }
        // В кэше найдено - обновить.
        else
        {
            plu1CFkCache.Plu = plu;
            WsSqlPlu1CFkValidator validator = new();
            ValidationResult validation = validator.Validate(plu1CFkCache);
            if (!validation.IsValid)
                throw new($"Exception at UpdatePlu1CFkDbCore. Check PLU {plu1CFkCache}!");
            SqlCore.Update(plu1CFkCache);
        }
    }

    #endregion
}
