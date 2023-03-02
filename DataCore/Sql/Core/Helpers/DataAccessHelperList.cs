// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Utils;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PrintersResourcesFks;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.ScalesScreenshots;
using DataCore.Sql.TableScaleModels.TemplatesResources;

namespace DataCore.Sql.Core.Helpers;

public partial class DataAccessHelper
{
    #region Public and private methods

    [Obsolete(@"Use DataContext")]
    public List<DeviceModel> GetListDevices(SqlCrudConfigModel sqlCrudConfig)
    {
        List<DeviceModel> result = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
            result.Add(GetItemNewEmpty<DeviceModel>());
        List<DeviceModel> list = GetListNotNullable<DeviceModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Name).ToList();
        result.AddRange(list);
        return result;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceTypeModel> GetListDevicesTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        List<DeviceTypeModel> result = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
            result.Add(GetItemNewEmpty<DeviceTypeModel>());
        List<DeviceTypeModel> list = GetListNotNullable<DeviceTypeModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Name).ToList();
        result.AddRange(list);
        return result;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceTypeFkModel> GetListDevicesTypesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        List<DeviceTypeFkModel> result = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
            result.Add(new() { Device = GetItemNewEmpty<DeviceModel>(), Type = GetItemNewEmpty<DeviceTypeModel>() });
        List<DeviceTypeFkModel> list = GetListNotNullable<DeviceTypeFkModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Type.Name).ToList();
        result = result.OrderBy(x => x.Device.Name).ToList();
        result.AddRange(list);
        return result;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceScaleFkModel> GetListDevicesScalesFks(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop);
        List<DeviceScaleFkModel> result = new();
        if (isAddFieldNull)
            result.Add(new() { Device = GetItemNewEmpty<DeviceModel>(), Scale = GetItemNewEmpty<ScaleModel>() });
        List<DeviceScaleFkModel> list = GetListNotNullable<DeviceScaleFkModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Scale.Description).ToList();
        result = result.OrderBy(x => x.Device.Name).ToList();
        result.AddRange(list);
        return result;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceTypeModel> GetListDevicesTypes(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceTypeModel> deviceTypes = GetListDevicesTypes(sqlCrudConfig);
        return deviceTypes;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceModel> GetListDevices(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceModel> devices = GetListDevices(sqlCrudConfig);
        return devices;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceTypeFkModel> GetListDevicesTypesFks(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceTypeFkModel> deviceTypesFks = GetListDevicesTypesFks(sqlCrudConfig);
        return deviceTypesFks;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceTypeFkModel> GetListDevicesTypesFkFree(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceTypeFkModel> deviceTypeFks = GetListDevicesTypesFks(sqlCrudConfig);
        List<DeviceModel> devices = GetListNotNullable<DeviceModel>(sqlCrudConfig);
        deviceTypeFks = deviceTypeFks.Where(x => !devices.Contains(x.Device)).ToList();
        return deviceTypeFks;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceTypeFkModel> GetListDevicesTypesFkBusy(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceTypeFkModel> deviceTypeFks = GetListDevicesTypesFks(sqlCrudConfig);
        List<DeviceModel> devices = GetListNotNullable<DeviceModel>(sqlCrudConfig);
        deviceTypeFks = deviceTypeFks.Where(x => devices.Contains(x.Device)).ToList();
        return deviceTypeFks;
    }

    [Obsolete(@"Use DataContext")]
    public List<PluLabelModel> GetListPluLabels(bool isShowMarked, bool isShowOnlyTop)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop);
        sqlCrudConfig.Orders.Add(new(nameof(PluWeighingModel.ChangeDt), SqlFieldOrderEnum.Desc));
        return GetListNotNullable<PluLabelModel>(sqlCrudConfig);
    }

    [Obsolete(@"Use DataContext")]
    public List<ScaleScreenShotModel> GetListScalesScreenShots(SqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(ScaleScreenShotModel.Scale), itemFilter?.IdentityValueId),
            isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<ScaleScreenShotModel> result = GetListNotNullable<ScaleScreenShotModel>(sqlCrudConfig);
        result = result.OrderByDescending(x => x.CreateDt).ToList();
        return result;
    }

    [Obsolete(@"Use DataContext")]
    public List<PluBundleFkModel> GetListPluBundles(SqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        List<PluBundleFkModel> result = new();
        if (isAddFieldNull)
            result.Add(GetItemNewEmpty<PluBundleFkModel>());
        List<SqlFieldFilterModel> filters = SqlCrudConfigModel.GetFiltersIdentity(nameof(PluBundleFkModel.Plu), itemFilter?.IdentityValueUid);

        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters,
            new SqlFieldOrderModel(nameof(PluBundleFkModel.Plu), SqlFieldOrderEnum.Asc),
            isShowMarked, isShowOnlyTop);
        result.AddRange(GetListNotNullable<PluBundleFkModel>(sqlCrudConfig));
        result = result.OrderBy(x => x.Bundle.Name).ToList();
        result = result.OrderBy(x => x.Plu.Number).ToList();
        return result;
    }

    [Obsolete(@"Use DataContext")]
    public List<PrinterResourceFkModel> GetListPrinterResources(SqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop)
    {
        List<SqlFieldFilterModel> filters = SqlCrudConfigModel.GetFiltersIdentity(nameof(PrinterResourceFkModel.Printer), itemFilter?.IdentityValueId);
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters,
            new SqlFieldOrderModel(nameof(SqlTableBase.Description), SqlFieldOrderEnum.Asc),
            isShowMarked, isShowOnlyTop);
        return GetListNotNullable<PrinterResourceFkModel>(sqlCrudConfig);
    }

    [Obsolete(@"Use DataContext")]
    public List<PrinterTypeModel> GetListPrinterTypes(bool isShowMarked, bool isShowOnlyTop)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            new SqlFieldOrderModel(nameof(PrinterTypeModel.Name), SqlFieldOrderEnum.Asc), isShowMarked, isShowOnlyTop);
        return GetListNotNullable<PrinterTypeModel>(sqlCrudConfig);
    }

    [Obsolete(@"Use DataContext")]
    public List<TemplateResourceDeprecatedModel> GetListTemplateResources(bool isShowMarked, bool isShowOnlyTop)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            new SqlFieldOrderModel(nameof(TemplateResourceDeprecatedModel.Type), SqlFieldOrderEnum.Asc), isShowMarked, isShowOnlyTop);
        List<TemplateResourceDeprecatedModel> result = GetListNotNullable<TemplateResourceDeprecatedModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Name).ToList();
        result = result.OrderBy(x => x.Type).ToList();
        return result;
    }

    #endregion
}