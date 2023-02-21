// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Utils;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusGroupsFks;
using DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusGroups;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.Templates;

namespace DataCore.Sql.Core.Helpers;

public partial class DataAccessHelper
{
    #region Public and public methods

    public AccessModel? GetItemAccessNullable(string? userName)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(nameof(SqlTableBase.Name), userName, false, false);
        return GetItemNullable<AccessModel>(sqlCrudConfig);
    }

    public ProductSeriesModel? GetItemProductSeriesNullable(ScaleModel scale)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            new List<SqlFieldFilterModel>
            {
                new(nameof(ProductSeriesModel.IsClose), SqlFieldComparerEnum.Equal, false),
                new($"{nameof(ProductSeriesModel.Scale)}.{nameof(ScaleModel.IdentityValueId)}",  SqlFieldComparerEnum.Equal, scale.IdentityValueId)
            }, false, false);
        return GetItemNullable<ProductSeriesModel>(sqlCrudConfig);
    }

    public ProductSeriesModel GetItemProductSeriesNotNullable(ScaleModel scale) =>
        GetItemProductSeriesNullable(scale) ?? GetItemNewEmpty<ProductSeriesModel>();

    private PluModel? GetItemPluNullable(PluScaleModel pluScale)
    {
        if (!pluScale.IsNotNew || !pluScale.Plu.IsNotNew) return null;
        
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            nameof(SqlTableBase.IdentityValueUid), pluScale.Plu.IdentityValueUid, false, false);
        return GetItemNullable<PluModel>(sqlCrudConfig);
    }

    public PluModel GetItemPluNotNullable(PluScaleModel pluScale) =>
        GetItemPluNullable(pluScale) ?? new();

    public PluTemplateFkModel? GetItemPluTemplateFkNullable(PluModel plu)
    {
	    if (plu.IsNew) return null;
	    SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
		    $"{nameof(PluTemplateFkModel.Plu)}.{nameof(SqlTableBase.IdentityValueUid)}", plu.IdentityValueUid,
		    false, false);
	    return GetItemNullable<PluTemplateFkModel>(sqlCrudConfig);
    }

    public PluTemplateFkModel GetItemPluTemplateFkNotNullable(PluModel plu) =>
	    GetItemPluTemplateFkNullable(plu) ?? new();

    public PluBundleFkModel? GetItemPluBundleFkNullable(PluModel plu, TableScaleModels.Bundles.BundleModel bundle)
    {
	    if (plu.IsNew) return null;
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(PluBundleFkModel.Plu)}.{nameof(SqlTableBase.IdentityValueUid)}", plu.IdentityValueUid, false, false);
        SqlCrudConfigModel sqlCrudConfigBundle = SqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(PluBundleFkModel.Bundle)}.{nameof(SqlTableBase.IdentityValueUid)}", bundle.IdentityValueUid, false, false);
        sqlCrudConfig.Filters.Add(sqlCrudConfigBundle.Filters.First());
        return GetItemNullable<PluBundleFkModel>(sqlCrudConfig);
    }

    public PluBundleFkModel GetItemPluBundleFkNotNullable(PluModel plu, TableScaleModels.Bundles.BundleModel bundle) =>
        GetItemPluBundleFkNullable(plu, bundle) ?? new();

    private TemplateModel? GetItemTemplateNullable(PluScaleModel pluScale)
    {
        if (pluScale.IsNew || pluScale.Plu.IsNew) return null;
        PluModel plu = GetItemPluNotNullable(pluScale);
        return GetItemPluTemplateFkNullable(plu)?.Template;
    }

    public TemplateModel GetItemTemplateNotNullable(PluScaleModel pluScale) =>
        GetItemTemplateNullable(pluScale) ?? new();

    private AppModel GetItemAppOrCreateNew(string appName)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(nameof(SqlTableBase.Name), appName, false, false);
        AppModel app = GetItemNotNullable<AppModel>(sqlCrudConfig);
        if (app.IsNew)
        {
            app = new()
            {
                Name = appName,
                CreateDt = DateTime.Now,
                ChangeDt = DateTime.Now
            };
        }
        else
        {
            app.ChangeDt = DateTime.Now;
        }
        SaveOrUpdate(app);
        return app;
    }

    public AppModel? GetItemAppNullable(string appName)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(nameof(SqlTableBase.Name), appName, false, false);
        return GetItemNullable<AppModel>(sqlCrudConfig);
    }

    private DeviceModel GetItemDeviceOrCreateNew(string name)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            nameof(SqlTableBase.Name), name, true, false);
        DeviceModel device = GetItemNotNullable<DeviceModel>(sqlCrudConfig);
        if (device.IsNew)
        {
            device = new()
            {
                Name = name,
                PrettyName = name,
                CreateDt = DateTime.Now,
                ChangeDt = DateTime.Now,
                LoginDt = DateTime.Now,
                LogoutDt = DateTime.Now,
                Ipv4 = NetUtils.GetLocalIpAddress()
            };
        }
        else
        {
            device.ChangeDt = DateTime.Now;
            device.LoginDt = DateTime.Now;
         
        }
        SaveOrUpdate(device);
        return device;
    }

    private ScaleModel? GetItemScaleNullable(DeviceModel device)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(SqlCrudConfigModel.GetFiltersIdentity(
            $"{nameof(DeviceScaleFkModel.Device)}", device.IdentityValueUid), false, false);
        return GetItemNotNullable<DeviceScaleFkModel>(sqlCrudConfig).Scale;
    }

    public ScaleModel GetItemScaleNotNullable(DeviceModel device) => 
        GetItemScaleNullable(device) ?? new();

    private DeviceModel? GetItemDeviceNullable(string name)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(nameof(SqlTableBase.Name), name, false, false);
        return GetItemNullable<DeviceModel>(sqlCrudConfig);
    }

    public DeviceModel? GetItemDeviceNullable(ScaleModel scale)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceScaleFkModel.Scale), scale.IdentityValueId), false, false);
        return GetItemNullable<DeviceScaleFkModel>(sqlCrudConfig)?.Device;
    }

    public DeviceModel GetItemDeviceNotNullable(string name) => GetItemDeviceNullable(name) ?? new();

    public DeviceModel GetItemDeviceNotNullable(ScaleModel scale) => GetItemDeviceNullable(scale) ?? new();

    public DeviceTypeModel? GetItemDeviceTypeNullable(string typeName)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFilters(nameof(DeviceTypeModel.Name), typeName), false, false);
        return GetItemNullable<DeviceTypeModel>(sqlCrudConfig);
    }

    public DeviceTypeModel GetItemDeviceTypeNotNullable(string typeName) =>
        GetItemDeviceTypeNullable(typeName) ?? new();

    public DeviceTypeFkModel? GetItemDeviceTypeFkNullable(DeviceModel device)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceTypeFkModel.Device), device.IdentityValueUid), false, false);
        return GetItemNullable<DeviceTypeFkModel>(sqlCrudConfig);
    }

    public DeviceTypeFkModel GetItemDeviceTypeFkNotNullable(DeviceModel device) =>
        GetItemDeviceTypeFkNullable(device) ?? new();

    public DeviceScaleFkModel? GetItemDeviceScaleFkNullable(DeviceModel device)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceScaleFkModel.Device), device.IdentityValueUid), false, false);
        return GetItemNullable<DeviceScaleFkModel>(sqlCrudConfig);
    }

    public DeviceScaleFkModel GetItemDeviceScaleFkNotNullable(DeviceModel device) =>
        GetItemDeviceScaleFkNullable(device) ?? new();

    public DeviceScaleFkModel? GetItemDeviceScaleFkNullable(ScaleModel scale)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceScaleFkModel.Scale), scale.IdentityValueId), false, false);
        return GetItemNullable<DeviceScaleFkModel>(sqlCrudConfig);
    }

    public DeviceScaleFkModel GetItemDeviceScaleFkNotNullable(ScaleModel scale) =>
        GetItemDeviceScaleFkNullable(scale) ?? new();

    public LogTypeModel? GetItemLogTypeNullable(LogType logType)
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
            { new(nameof(LogTypeModel.Number), SqlFieldComparerEnum.Equal, (byte)logType) },
            true, true, false, false);
        return GetItemNullable<LogTypeModel>(sqlCrudConfig);
    }

    public LogTypeModel GetItemLogTypeNotNullable(LogType logType) => 
        GetItemLogTypeNullable(logType) ?? new();

    public List<LogTypeModel> GetListLogTypesNotNullable()
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(),
            false, false, false, true);
        sqlCrudConfig.AddOrders(new(nameof(LogTypeModel.Number), SqlFieldOrderEnum.Asc));
        return GetListNotNullable<LogTypeModel>(sqlCrudConfig);
    }

    public string GetAccessRightsDescription(AccessRightsEnum? accessRights)
    {
        return accessRights switch
        {
            AccessRightsEnum.Read => LocaleCore.Strings.AccessRightsRead,
            AccessRightsEnum.Write => LocaleCore.Strings.AccessRightsWrite,
            AccessRightsEnum.Admin => LocaleCore.Strings.AccessRightsAdmin,
            _ => LocaleCore.Strings.AccessRightsNone
        };
    }

    public string GetAccessRightsDescription(byte accessRights) =>
        GetAccessRightsDescription((AccessRightsEnum)accessRights);

    public ScaleModel GetScaleNotNullable(long id)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            nameof(SqlTableBase.IdentityValueId), id, false, false);
        return DataAccessHelper.Instance.GetItemNotNullable<ScaleModel>(sqlCrudConfig);
    }

    public ProductionFacilityModel GetProductionFacilityNotNullable(string name)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            nameof(ProductionFacilityModel.Name), name, false, false);
        return DataAccessHelper.Instance.GetItemNotNullable<ProductionFacilityModel>(sqlCrudConfig);
    }

    public PluGroupModel? GetItemNomenclatureGroupParentNullable(PluGroupModel nomenclatureGroup)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(SqlCrudConfigModel.GetFilters(
            $"{nameof(PluGroupFkModel.PluGroup)}.{nameof(SqlTableBase.IdentityValueUid)}", nomenclatureGroup.IdentityValueUid),
            false, false);
        PluGroupModel? result = GetItemNullable<PluGroupFkModel>(sqlCrudConfig)?.Parent;
        return result;
    }

    public PluGroupModel GetItemNomenclatureGroupParentNotNullable(PluGroupModel nomenclatureGroup) => 
        GetItemNomenclatureGroupParentNullable(nomenclatureGroup) ?? new();

    #endregion
}
