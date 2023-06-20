// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Utils;

/// <summary>
/// Утилиты получения данных веб-сервиса.
/// </summary>
public static class WsServiceGetUtils
{
    #region Public and private fields, properties, constructor

    private static WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;

    #endregion

    #region Public and private methods

    /// <summary>
    /// Get AcceptVersion from string value.
    /// </summary>
    /// <returns></returns>
    public static WsSqlEnumAcceptVersion GetAcceptVersion(string value) =>
        value.ToUpper() switch
        {
            "V2" => WsSqlEnumAcceptVersion.V2,
            "V3" => WsSqlEnumAcceptVersion.V3,
            _ => WsSqlEnumAcceptVersion.V1
        };
    
    /// <summary>
    /// Получить бренд.
    /// </summary>
    /// <param name="contextType"></param>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <returns></returns>
    public static WsSqlBrandModel GetItemBrand(WsSqlEnumContextType contextType, WsResponse1CShortModel response,
        Guid uid1C, Guid uid1CException, string refName)
    {
        WsSqlBrandModel result = contextType switch
        {
            WsSqlEnumContextType.Cache => ContextCache.Brands.Find(item => item.Uid1C.Equals(uid1C))
                                          ?? ContextManager.ContextBrands.GetNewItem(),
            _ => ContextManager.ContextBrands.GetItemByUid1C(uid1C),
        };
        if (!Equals(uid1C, Guid.Empty))
        {
            if (result.IsNew)
            {
                WsServiceResponseUtils.AddResponseException(response, uid1CException,
                    new($"{refName} {WsLocaleCore.WebService.With} '{uid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
            }
        }
        return result;
    }

    /// <summary>
    /// Получить пакет.
    /// </summary>
    /// <param name="contextType"></param>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <returns></returns>
    public static WsSqlBundleModel GetItemBundle(WsSqlEnumContextType contextType, WsResponse1CShortModel response,
        Guid uid1C, Guid uid1CException, string refName)
    {
        WsSqlBundleModel result = contextType switch
        {
            WsSqlEnumContextType.Cache => ContextCache.Bundles.Find(item => item.Uid1C.Equals(uid1C))
                                          ?? ContextManager.ContextBundles.GetNewItem(),
            _ => ContextManager.ContextBundles.GetItemByUid1C(uid1C),
        };
        if (result.IsNew)
        {
            WsServiceResponseUtils.AddResponseException(response, uid1CException,
                new($"{refName} {WsLocaleCore.WebService.With} '{uid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
        }
        return result;
    }

    /// <summary>
    /// Получить клипсу.
    /// </summary>
    /// <param name="contextType"></param>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <returns></returns>
    public static WsSqlClipModel GetItemClip(WsSqlEnumContextType contextType, WsResponse1CShortModel response,
        Guid uid1C, Guid uid1CException, string refName)
    {
        WsSqlClipModel result = contextType switch
        {
            WsSqlEnumContextType.Cache => ContextCache.Clips.Find(item => item.Uid1C.Equals(uid1C))
                                      ?? ContextManager.ContextClips.GetNewItem(),
            _ => ContextManager.ContextClips.GetItemByUid1C(uid1C),
        };
        if (result.IsNew)
        {
            WsServiceResponseUtils.AddResponseException(response, uid1CException,
                new($"{refName} {WsLocaleCore.WebService.With} '{uid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
        }
        return result;
    }

    /// <summary>
    /// Получить коробку.
    /// </summary>
    /// <param name="contextType"></param>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <returns></returns>
    public static WsSqlBoxModel GetBox(WsSqlEnumContextType contextType, WsResponse1CShortModel response,
        Guid uid1C, Guid uid1CException, string refName)
    {
        WsSqlBoxModel result = contextType switch
        {
            WsSqlEnumContextType.Cache => ContextCache.Boxes.Find(item => item.Uid1C.Equals(uid1C))
                                      ?? ContextManager.ContextBoxes.GetNewItem(),
            _ => ContextManager.ContextBoxes.GetItemByUid1C(uid1C),
        };
        if (result.IsNew)
        {
            WsServiceResponseUtils.AddResponseException(response, uid1CException,
                new($"{refName} {WsLocaleCore.WebService.With} '{uid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
        }
        return result;
    }

    /// <summary>
    /// Получить ПЛУ.
    /// </summary>
    /// <param name="contextType"></param>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <returns></returns>
    public static WsSqlPluModel GetItemPlu(WsSqlEnumContextType contextType, WsResponse1CShortModel response,
        Guid uid1C, Guid uid1CException, string refName)
    {
        WsSqlPluModel result = contextType switch
        {
            WsSqlEnumContextType.Cache => ContextCache.Plus.Find(item => item.Uid1C.Equals(uid1C))
                                      ?? ContextManager.ContextPlus.GetNewItem(),
            _ => ContextManager.ContextPlus.GetItemByUid1C(uid1C),
        };
        if (result.IsNew)
        {
            WsServiceResponseUtils.AddResponseException(response, uid1CException,
                new($"{refName} {WsLocaleCore.WebService.With} '{uid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
        }
        return result;
    }

    /// <summary>
    /// Получить связь ПЛУ.
    /// </summary>
    /// <param name="contextType"></param>
    /// <param name="response"></param>
    /// <param name="pluUid1C"></param>
    /// <param name="categoryUid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <param name="parentUid1C"></param>
    /// <returns></returns>
    public static WsSqlPluFkModel GetItemPluFk(WsSqlEnumContextType contextType, WsResponse1CShortModel response,
        Guid pluUid1C, Guid parentUid1C, Guid? categoryUid1C, Guid uid1CException, string refName)
    {
        WsSqlPluFkModel result = contextType switch
        {
            WsSqlEnumContextType.Cache => ContextCache.PlusFks.Find(
                item => item.Plu.Uid1C.Equals(pluUid1C) &&
                item.Parent.Uid1C.Equals(parentUid1C) &&
                categoryUid1C is not null ? categoryUid1C.Equals(item.Category?.Uid1C) : item.Category is null)
                ?? ContextManager.ContextPlusFk.GetNewItem(),
            /*
 ContextCache..Find(item =>
                Equals(item.Plu.Uid1C, ) &&
                Equals(item.Parent.Uid1C, pluFk.Parent.Uid1C) &&
                Equals(item.Category?.Uid1C, pluFk.Category?.Uid1C))
             */
            _ => throw new ArgumentException(),
        };
        if (result.IsNew)
        {
            WsServiceResponseUtils.AddResponseException(response, uid1CException,
                new($"{refName} {WsLocaleCore.WebService.With} '{pluUid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
        }
        return result;
    }

    /// <summary>
    /// Получить характеристику ПЛУ.
    /// </summary>
    /// <param name="contextType"></param>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <returns></returns>
    public static WsSqlPluCharacteristicModel GetItemPluCharacteristic(WsSqlEnumContextType contextType, WsResponse1CShortModel response,
        Guid uid1C, Guid uid1CException, string refName)
    {
        WsSqlPluCharacteristicModel result = contextType switch
        {
            WsSqlEnumContextType.Cache => ContextCache.PlusCharacteristics.Find(item => item.Uid1C.Equals(uid1C))
                                      ?? ContextManager.ContextPluCharacteristics.GetNewItem(),
            _ => ContextManager.ContextPluCharacteristics.GetItemByUid1C(uid1C),
        };
        if (result.IsNew)
        {
            WsServiceResponseUtils.AddResponseException(response, uid1CException,
                new($"{refName} {WsLocaleCore.WebService.With} '{uid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
        }
        return result;
    }

    /// <summary>
    /// Получить связь бренда ПЛУ.
    /// </summary>
    /// <param name="contextType"></param>
    /// <param name="response"></param>
    /// <param name="pluUid1C"></param>
    /// <param name="brandUid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <returns></returns>
    public static WsSqlPluBrandFkModel GetItemPluBrandFk(WsSqlEnumContextType contextType, WsResponse1CShortModel response,
        Guid pluUid1C, Guid brandUid1C, Guid uid1CException, string refName)
    {
        WsSqlPluBrandFkModel result = contextType switch
        {
            WsSqlEnumContextType.Cache => ContextCache.PlusBrandsFks.Find(item =>
                item.Plu.Uid1C.Equals(pluUid1C) && item.Brand.Uid1C.Equals(brandUid1C))
                ?? ContextManager.ContextPluBrandsFk.GetNewItem(),
            _ => throw new ArgumentException(),
        };
        if (result.IsNew)
        {
            WsServiceResponseUtils.AddResponseException(response, uid1CException,
                new($"{refName} {WsLocaleCore.WebService.With} '{pluUid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
        }
        return result;
    }

    /// <summary>
    /// Получить связь клипсы ПЛУ.
    /// </summary>
    /// <param name="contextType"></param>
    /// <param name="response"></param>
    /// <param name="pluUid1C"></param>
    /// <param name="clipUid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <returns></returns>
    public static WsSqlPluClipFkModel GetItemPluClipFk(WsSqlEnumContextType contextType, WsResponse1CShortModel response,
        Guid pluUid1C, Guid clipUid1C, Guid uid1CException, string refName)
    {
        WsSqlPluClipFkModel result = contextType switch
        {
            WsSqlEnumContextType.Cache => ContextCache.PlusClipsFks.Find(item =>
                item.Plu.Uid1C.Equals(pluUid1C) && item.Clip.Uid1C.Equals(clipUid1C))
                ?? ContextManager.ContextPlusClipsFk.GetNewItem(),
            _ => throw new ArgumentException(),
        };
        if (result.IsNew)
        {
            WsServiceResponseUtils.AddResponseException(response, uid1CException,
                new($"{refName} {WsLocaleCore.WebService.With} '{pluUid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
        }
        return result;
    }

    /// <summary>
    /// Получить связь пакета ПЛУ.
    /// </summary>
    /// <param name="contextType"></param>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <returns></returns>
    public static WsSqlPluBundleFkModel GetItemPluBundleFk(WsSqlEnumContextType contextType, WsResponse1CShortModel response,
        Guid uid1C, Guid uid1CException, string refName)
    {
        WsSqlPluBundleFkModel result = contextType switch
        {
            WsSqlEnumContextType.Cache => ContextCache.PlusBundlesFks.Find(item => item.Plu.Uid1C.Equals(uid1C))
                                      ?? ContextManager.ContextPluBundlesFk.GetNewItem(),
            _ => throw new ArgumentException(),
        };
        if (result.IsNew)
        {
            WsServiceResponseUtils.AddResponseException(response, uid1CException,
                new($"{refName} {WsLocaleCore.WebService.With} '{uid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
        }
        return result;
    }

    /// <summary>
    /// Получить список связей обмена ПЛУ 1С по GUID_1C.
    /// </summary>
    /// <param name="uid1C"></param>
    public static List<WsSqlPlu1CFkModel> GetPlus1CFksByGuid1C(Guid uid1C)
    {
        List<WsSqlPlu1CFkModel> plus1CFks = new();
        ContextCache.Load(WsSqlEnumTableName.Plus1CFks);
        // Получить список ПЛУ по UID_1C.
        List<WsSqlPluModel> plusDb = ContextManager.ContextPlus.GetListByUid1C(uid1C);
        foreach (WsSqlPluModel plu in plusDb)
        {
            WsSqlPlu1CFkModel? plu1CFkCache =
                ContextCache.Plus1CFks.Find(item => item.Plu.IdentityValueUid.Equals(plu.IdentityValueUid));
            if (plu1CFkCache is not null)
                plus1CFks.Add(plu1CFkCache);
        }
        return plus1CFks;
    }

    #endregion
}
