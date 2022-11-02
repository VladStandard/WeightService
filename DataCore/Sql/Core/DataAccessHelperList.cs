// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public partial class DataAccessHelper
{
	#region Public and private methods

	public List<DeviceTypeFkModel> GetListDevicesTypesFks(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(0, isShowMarked, isShowOnlyTop);
		List<DeviceTypeFkModel> result = new();
		if (isAddFieldNull)
			result.Add(new() { Device = GetItemNew<DeviceModel>(), DeviceType = GetItemNew<DeviceTypeModel>() });
		List<DeviceTypeFkModel> list = GetListNotNull<DeviceTypeFkModel>(sqlCrudConfig, false);
		result = result.OrderBy(x => x.DeviceType.Name).ToList();
		result = result.OrderBy(x => x.Device.Name).ToList();
		result.AddRange(list);
		return result; 
	}

	public List<DeviceScaleFkModel> GetListDevicesScalesFks(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(0, isShowMarked, isShowOnlyTop);
		List<DeviceScaleFkModel> result = new();
		if (isAddFieldNull)
			result.Add(new() { Device = GetItemNew<DeviceModel>(), Scale = GetItemNew<ScaleModel>() });
		List<DeviceScaleFkModel> list = GetListNotNull<DeviceScaleFkModel>(sqlCrudConfig, false);
		result = result.OrderBy(x => x.Scale.Description).ToList();
		result = result.OrderBy(x => x.Device.Name).ToList();
		result.AddRange(list);
		return result; 
	}

	public List<DeviceTypeFkModel> GetListDevicesTypesFkFree(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		List<DeviceTypeFkModel> deviceTypeFks = GetListDevicesTypesFks(isShowMarked, isShowOnlyTop, isAddFieldNull);
		List<DeviceModel> devices = GetListNotNull<DeviceModel>(new(), isShowMarked, isShowOnlyTop, isAddFieldNull);
		deviceTypeFks = deviceTypeFks.Where(x => !devices.Contains(x.Device)).ToList();
		return deviceTypeFks;
	}

	public List<DeviceTypeFkModel> GetListDevicesTypesFkBusy(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		List<DeviceTypeFkModel> deviceTypeFks = GetListDevicesTypesFks(isShowMarked, isShowOnlyTop, isAddFieldNull);
		List<DeviceModel> devices = GetListNotNull<DeviceModel>(new(), isShowMarked, isShowOnlyTop, isAddFieldNull);
		deviceTypeFks = deviceTypeFks.Where(x => devices.Contains(x.Device)).ToList();
		return deviceTypeFks;
	}

	public List<PluLabelModel> GetListPluLabels(bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(0, isShowMarked, isShowOnlyTop);
		sqlCrudConfig.Orders.Add(new(nameof(PluWeighingModel.ChangeDt), SqlFieldOrderEnum.Desc));
		return GetListNotNull<PluLabelModel>(sqlCrudConfig, false);
	}

    public List<PluScaleModel> GetListPluScales(SqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		List<SqlFieldFilterModel> filters = GetSqlFieldFilterList<ScaleModel>(itemFilter, nameof(PluScaleModel.Scale), itemFilter?.IdentityValueId);
		//if (!isShowNoActive)
		//	filters.Add(new(nameof(PluScaleModel.IsActive), SqlFieldComparerEnum.Equal, true));
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters,
			0, isShowMarked, isShowOnlyTop);
		List<PluScaleModel> result = GetListNotNull<PluScaleModel>(sqlCrudConfig, false);
		result = result.OrderBy(x => x.Plu.Name).ToList();
		return result;
	}

    public List<ScaleScreenShotModel> GetListScalesScreenShots(SqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		List<SqlFieldFilterModel> filters = GetSqlFieldFilterList<ScaleModel>(itemFilter, nameof(ScaleScreenShotModel.Scale), itemFilter?.IdentityValueId);

		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, 0, isShowMarked, isShowOnlyTop);
		List<ScaleScreenShotModel> result = GetListNotNull<ScaleScreenShotModel>(sqlCrudConfig, false);
		result = result.OrderByDescending(x => x.CreateDt).ToList();
		return result;
	}

	public List<PluPackageModel> GetListPluPackages(SqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		List<PluPackageModel> result = new();
        if (isAddFieldNull)
            result.Add(GetItemNew<PluPackageModel>());
		List<SqlFieldFilterModel> filters = GetSqlFieldFilterList<PluPackageModel>(itemFilter, nameof(PluPackageModel.Plu), itemFilter?.IdentityValueUid);

		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters,
			new List<SqlFieldOrderModel> { new (nameof(PluPackageModel.Plu), SqlFieldOrderEnum.Asc), },
			0, isShowMarked, isShowOnlyTop);
		result.AddRange(GetListNotNull<PluPackageModel>(sqlCrudConfig, false));
		result = result.OrderBy(x => x.Package.Name).ToList();
		result = result.OrderBy(x => x.Plu.Number).ToList();
		return result;
	}

	public List<SqlFieldFilterModel> GetSqlFieldFilterList<T>(SqlTableBase? itemFilter, string className, object? value) where T :SqlTableBase, new()
	{
		List<SqlFieldFilterModel> filters = new();
		if (itemFilter is T)
		{
			filters = value switch
			{
				Guid uid => new() { new($"{className}.{nameof(SqlTableBase.IdentityValueUid)}", SqlFieldComparerEnum.Equal, uid) },
				long id => new() { new($"{className}.{nameof(SqlTableBase.IdentityValueId)}", SqlFieldComparerEnum.Equal, id) },
				_ => filters
			};
		}
		return filters;
	}

	public List<PluWeighingModel> GetListPluWeighings(bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(0, isShowMarked, isShowOnlyTop);
		sqlCrudConfig.Orders.Add(new(nameof(PluWeighingModel.ChangeDt), SqlFieldOrderEnum.Desc));
		return GetListNotNull<PluWeighingModel>(sqlCrudConfig, false);
	}

	public List<PrinterResourceModel> GetListPrinterResources(SqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop)
	{
		List<SqlFieldFilterModel> filters = GetSqlFieldFilterList<ScaleModel>(itemFilter, nameof(PrinterResourceModel.Printer), itemFilter?.IdentityValueId);
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters,
			new SqlFieldOrderModel(nameof(SqlTableBase.Description), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		return GetListNotNull<PrinterResourceModel>(sqlCrudConfig, false);
	}

	public List<PrinterTypeModel> GetListPrinterTypes(bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(PrinterTypeModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		return GetListNotNull<PrinterTypeModel>(sqlCrudConfig, false);
	}

	public List<TemplateResourceModel> GetListTemplateResources(bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(TemplateResourceModel.Type), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<TemplateResourceModel> result = GetListNotNull<TemplateResourceModel>(sqlCrudConfig, false);
		result = result.OrderBy(x => x.Name).ToList();
		result = result.OrderBy(x => x.Type).ToList();
		return result;
	}

	#endregion
}
