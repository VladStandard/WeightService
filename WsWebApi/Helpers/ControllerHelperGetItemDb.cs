// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Clips;
using DataCore.Sql.TableScaleModels.PlusCharacteristics;

// ReSharper disable InconsistentNaming

namespace WsWebApi.Helpers;

public partial class ControllerHelper
{
    #region Public and private methods

    /// <summary>
    /// Get PLU from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="isCheckGroup"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    private bool GetPluDb(Response1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, bool isCheckGroup, out PluModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                { new() { Name = nameof(SqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false);
            itemDb = DataContext.DataAccess.GetItemNullable<PluModel>(sqlCrudConfig);
            if (!isCheckGroup)
            {
                if (itemDb is null || itemDb.IsNew)
                {
                    AddResponse1cException(response, uid1cException,
                        new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
                    return false;
                }
                return true;
            }
            // isCheckGroup.
            if (itemDb is null || itemDb.IsNew || !itemDb.IsGroup)
            {
                AddResponse1cException(response, uid1cException,
                    new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get bundle from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    private bool GetBundleDb(Response1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out BundleModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new() { Name = nameof(SqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false);
            itemDb = DataContext.DataAccess.GetItemNullable<BundleModel>(sqlCrudConfig);
            if (itemDb is null || itemDb.IsNew)
            {
                AddResponse1cException(response, uid1cException,
                    new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get brand from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    private bool GetBrandDb(Response1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out BrandModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new() { Name = nameof(SqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false);
            itemDb = DataContext.DataAccess.GetItemNullable<BrandModel>(sqlCrudConfig);
            if (itemDb is null || itemDb.IsNew)
            {
                AddResponse1cException(response, uid1cException,
                    new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get clip from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    private bool GetClipDb(Response1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out ClipModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new() { Name = nameof(SqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false);
            itemDb = DataContext.DataAccess.GetItemNullable<ClipModel>(sqlCrudConfig);
            if (itemDb is null || itemDb.IsNew)
            {
                AddResponse1cException(response, uid1cException,
                    new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get box from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    private bool GetBoxDb(Response1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out BoxModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel> 
                { new() { Name = nameof(SqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false);
            itemDb = DataContext.DataAccess.GetItemNullable<BoxModel>(sqlCrudConfig);
            if (itemDb is null || itemDb.IsNew)
            {
                AddResponse1cException(response, uid1cException, 
                    new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get PLU from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    private bool GetPluDb(Response1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out PluModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new() { Name = nameof(SqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false);
            itemDb = DataContext.DataAccess.GetItemNullable<PluModel>(sqlCrudConfig);
            if (itemDb is null || itemDb.IsNew)
            {
                AddResponse1cException(response, uid1cException, 
                    new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get PLU characteristic from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    private bool GetPluCharacteristicDb(Response1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out PluCharacteristicModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new() { Name = nameof(SqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false);
            itemDb = DataContext.DataAccess.GetItemNullable<PluCharacteristicModel>(sqlCrudConfig);
            if (itemDb is null || itemDb.IsNew)
            {
                AddResponse1cException(response, uid1cException,
                    new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
                return false;
            }
            return true;
        }
        return false;
    }

    #endregion
}