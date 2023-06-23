// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Utils;

/// <summary>
/// Утилиты получения данных веб-сервиса.
/// </summary>
public static class WsServiceCheckUtils
{
    #region Public and private fields, properties, constructor

    private static WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;

    #endregion

    #region Public and private methods

    /// <summary>
    /// Проверить некорректность группы и номера ПЛУ.
    /// </summary>
    /// <param name="pluXml">Номенклатура</param>
    public static bool CheckUnCorrectPluNumberForNonGroup(WsSqlPluModel pluXml) =>
        // Проверить корректность группы и номера ПЛУ.
        !CheckCorrectPluNumberForNonGroup(pluXml);

    /// <summary>
    /// Проверить корректность группы и номера ПЛУ.
    /// </summary>
    /// <param name="pluXml">Номенклатура</param>
    public static bool CheckCorrectPluNumberForNonGroup(WsSqlPluModel pluXml)
    {
        if (pluXml is { IsGroup: true, Number: 0 }) return true;
        if (pluXml is { IsGroup: false, Number: > 0 }) return true;
        pluXml.ParseResult.Status = WsEnumParseStatus.Error;
        pluXml.ParseResult.Exception =
            WsLocaleCore.WebService.FieldPluNumberTemplate(pluXml.Number) + WsLocaleCore.WebService.FieldNomenclatureIsZeroNumber;
        return false;
    }

    /// <summary>
    /// Проверить наличие всех связей разрешённых для загрузки ПЛУ из 1С.
    /// </summary>
    public static bool CheckExistsAllPlus1CFksDb()
    {
        // Загрузить кэш.
        ContextCache.Load(WsSqlEnumTableName.Plus1CFks);
        // Получить список ПЛУ.
        List<WsSqlPluModel> plusDb = ContextManager.ContextPlus.GetList();
        if (plusDb.Count > ContextCache.Plus1CFks.Count) return false;

        foreach (WsSqlPluModel plu in plusDb)
        {
            WsSqlPlu1CFkModel? plu1CFkCache =
                ContextCache.Plus1CFks.Find(item => item.Plu.IdentityValueUid.Equals(plu.IdentityValueUid));
            if (plu1CFkCache is null)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить разрешение обмена для ПЛУ.
    /// </summary>
    /// <param name="itemXml"></param>
    /// <param name="plus1CFks"></param>
    public static void CheckEnabledPlu(WsSqlTable1CBase itemXml, List<WsSqlPlu1CFkModel> plus1CFks) =>
        plus1CFks.ForEach(item => CheckEnabledPluForItem(itemXml, item));

    /// <summary>
    /// Проверить номер ПЛУ в списке доступа к выгрузке.
    /// </summary>
    /// <param name="itemXml"></param>
    /// <param name="plu1CFkDb"></param>
    private static void CheckEnabledPluForItem(WsSqlTable1CBase itemXml, WsSqlPlu1CFkModel plu1CFkDb)
    {
        // Пропуск групп с нулевым номером.
        if (WsServiceCheckUtils.CheckUnCorrectPluNumberForNonGroup(plu1CFkDb.Plu)) return;
        // ПЛУ не найдена.
        if (plu1CFkDb.IsNotExists)
        {
            itemXml.ParseResult.Status = WsEnumParseStatus.Error;
            itemXml.ParseResult.Exception =
                $"{WsLocaleCore.WebService.FieldNomenclatureIsNotFound} '{plu1CFkDb.Plu.Number}' {WsLocaleCore.WebService.WithFieldCode} '{plu1CFkDb.Plu.Code}'";
        }
        // Загрузка номенклатуры.
        if (itemXml is WsSqlPluModel pluXml)
        {
            // UID_1C не совпадает.
            if (!Equals(pluXml.Uid1C, plu1CFkDb.Plu.Uid1C))
            {
                itemXml.ParseResult.Status = WsEnumParseStatus.Error;
                itemXml.ParseResult.Exception =
                    $"{WsLocaleCore.WebService.FieldNomenclatureIsErrorUid1C} '{plu1CFkDb.Plu.Number}' {WsLocaleCore.WebService.WithFieldCode} '{plu1CFkDb.Plu.Code}'";
            }
            // Загрузка ПЛУ выключена по номеру.
            if (plu1CFkDb.IsEnabled && !plu1CFkDb.Plu.Number.Equals(pluXml.Number))
            {
                itemXml.ParseResult.Status = WsEnumParseStatus.Error;
                itemXml.ParseResult.Exception =
                    WsLocaleCore.WebService.FieldPluNumberTemplate(pluXml.Number) + 
                    WsLocaleCore.WebService.FieldNomenclatureIsDiffForLoadByNumber(plu1CFkDb.Plu.Number, pluXml.Number);
            }
        }
        // Загрузка характеристики номенклатуры.
        else if (itemXml is WsSqlPluCharacteristicModel pluCharacteristicXml)
        {
            if (!Equals(pluCharacteristicXml.NomenclatureGuid, plu1CFkDb.Plu.Uid1C))
            {
                itemXml.ParseResult.Status = WsEnumParseStatus.Error;
                itemXml.ParseResult.Exception =
                    $"{WsLocaleCore.WebService.FieldNomenclatureIsErrorUid1C} '{plu1CFkDb.Plu.Number}' {WsLocaleCore.WebService.WithFieldCode} '{plu1CFkDb.Plu.Code}'";
            }
        }
        // Загрузка ПЛУ выключена по UID_1C.
        if (!plu1CFkDb.IsEnabled)
        {
            itemXml.ParseResult.Status = WsEnumParseStatus.Error;
            itemXml.ParseResult.Exception =
                WsLocaleCore.WebService.FieldPluNumberTemplate(plu1CFkDb.Plu.Number) + WsLocaleCore.WebService.FieldNomenclatureIsDenyForLoadByUid1C;
        }
    }

    #endregion
}