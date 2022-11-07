// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public partial class DataAccessHelper
{
	#region Public and private methods

	public List<DeviceTypeFkModel> GetListDevicesTypesFks(SqlCrudConfigModel sqlCrudConfig)
	{
		List<DeviceTypeFkModel> result = new();
		if (sqlCrudConfig.IsResultAddFieldEmpty)
			result.Add(new() { Device = GetItemNew<DeviceModel>(), DeviceType = GetItemNew<DeviceTypeModel>() });
		List<DeviceTypeFkModel> list = GetListNotNull<DeviceTypeFkModel>(sqlCrudConfig);
		result = result.OrderBy(x => x.DeviceType.Name).ToList();
		result = result.OrderBy(x => x.Device.Name).ToList();
		result.AddRange(list);
		return result;
	}

	public List<DeviceScaleFkModel> GetListDevicesScalesFks(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop);
		List<DeviceScaleFkModel> result = new();
		if (isAddFieldNull)
			result.Add(new() { Device = GetItemNew<DeviceModel>(), Scale = GetItemNew<ScaleModel>() });
		List<DeviceScaleFkModel> list = GetListNotNull<DeviceScaleFkModel>(sqlCrudConfig);
		result = result.OrderBy(x => x.Scale.Description).ToList();
		result = result.OrderBy(x => x.Device.Name).ToList();
		result.AddRange(list);
		return result;
	}

	public List<DeviceTypeFkModel> GetListDevicesTypesFkFree(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
		List<DeviceTypeFkModel> deviceTypeFks = GetListDevicesTypesFks(sqlCrudConfig);
		List<DeviceModel> devices = GetListNotNull<DeviceModel>(sqlCrudConfig);
		deviceTypeFks = deviceTypeFks.Where(x => !devices.Contains(x.Device)).ToList();
		return deviceTypeFks;
	}

	public List<DeviceTypeFkModel> GetListDevicesTypesFkBusy(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
		List<DeviceTypeFkModel> deviceTypeFks = GetListDevicesTypesFks(sqlCrudConfig);
		List<DeviceModel> devices = GetListNotNull<DeviceModel>(sqlCrudConfig);
		deviceTypeFks = deviceTypeFks.Where(x => devices.Contains(x.Device)).ToList();
		return deviceTypeFks;
	}

	public List<PluLabelModel> GetListPluLabels(bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop);
		sqlCrudConfig.Orders.Add(new(nameof(PluWeighingModel.ChangeDt), SqlFieldOrderEnum.Desc));
		return GetListNotNull<PluLabelModel>(sqlCrudConfig);
	}

	public List<PluScaleModel> GetListPluScales(SqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		List<SqlFieldFilterModel> filters = SqlCrudConfigModel.GetFiltersIdentity(nameof(PluScaleModel.Scale), itemFilter?.IdentityValueId);
		//if (!isShowNoActive)
		//	filters.Add(new(nameof(PluScaleModel.IsActive), SqlFieldComparerEnum.Equal, true));
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
		List<PluScaleModel> result = GetListNotNull<PluScaleModel>(sqlCrudConfig);
		result = result.OrderBy(x => x.Plu.Name).ToList();
		return result;
	}

	public List<ScaleScreenShotModel> GetListScalesScreenShots(SqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
			SqlCrudConfigModel.GetFiltersIdentity(nameof(ScaleScreenShotModel.Scale), itemFilter?.IdentityValueId), 
			isShowMarked, isShowOnlyTop, isAddFieldNull);
		List<ScaleScreenShotModel> result = GetListNotNull<ScaleScreenShotModel>(sqlCrudConfig);
		result = result.OrderByDescending(x => x.CreateDt).ToList();
		return result;
	}

	public List<PluPackageModel> GetListPluPackages(SqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		List<PluPackageModel> result = new();
		if (isAddFieldNull)
			result.Add(GetItemNew<PluPackageModel>());
		List<SqlFieldFilterModel> filters = SqlCrudConfigModel.GetFiltersIdentity(nameof(PluPackageModel.Plu), itemFilter?.IdentityValueUid);

		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters,
			new SqlFieldOrderModel(nameof(PluPackageModel.Plu), SqlFieldOrderEnum.Asc),
			isShowMarked, isShowOnlyTop);
		result.AddRange(GetListNotNull<PluPackageModel>(sqlCrudConfig));
		result = result.OrderBy(x => x.Package.Name).ToList();
		result = result.OrderBy(x => x.Plu.Number).ToList();
		return result;
	}

	public List<PluWeighingModel> GetListPluWeighings(SqlCrudConfigModel sqlCrudConfig)
	{
		sqlCrudConfig.Orders.Add(new(nameof(PluWeighingModel.ChangeDt), SqlFieldOrderEnum.Desc));
		return GetListNotNull<PluWeighingModel>(sqlCrudConfig);
	}

	public List<PrinterResourceModel> GetListPrinterResources(SqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop)
	{
		List<SqlFieldFilterModel> filters = SqlCrudConfigModel.GetFiltersIdentity(nameof(PrinterResourceModel.Printer), itemFilter?.IdentityValueId);
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters,
			new SqlFieldOrderModel(nameof(SqlTableBase.Description), SqlFieldOrderEnum.Asc),
			isShowMarked, isShowOnlyTop);
		return GetListNotNull<PrinterResourceModel>(sqlCrudConfig);
	}

	public List<PrinterTypeModel> GetListPrinterTypes(bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(PrinterTypeModel.Name), SqlFieldOrderEnum.Asc), isShowMarked, isShowOnlyTop);
		return GetListNotNull<PrinterTypeModel>(sqlCrudConfig);
	}

	public List<TemplateResourceModel> GetListTemplateResources(bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(TemplateResourceModel.Type), SqlFieldOrderEnum.Asc), isShowMarked, isShowOnlyTop);
		List<TemplateResourceModel> result = GetListNotNull<TemplateResourceModel>(sqlCrudConfig);
		result = result.OrderBy(x => x.Name).ToList();
		result = result.OrderBy(x => x.Type).ToList();
		return result;
	}

	#endregion
}
