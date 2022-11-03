// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public partial class DataAccessHelper
{
	#region Public and public methods

	public AccessModel? GetItemAccess(string? userName)
	{
		List<SqlFieldFilterModel> filters = GetFilters(nameof(SqlTableBase.Name), userName);
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters, false, false);
		return GetItem<AccessModel>(sqlCrudConfig);
	}

	public ProductSeriesModel? GetItemProductSeries(ScaleModel scale)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
			new List<SqlFieldFilterModel>
			{
				new(nameof(ProductSeriesModel.IsClose), SqlFieldComparerEnum.Equal, false),
				new($"{nameof(ProductSeriesModel.Scale)}.{nameof(ScaleModel.IdentityValueId)}",                     SqlFieldComparerEnum.Equal, scale.IdentityValueId),
			}, false, false);
		return GetItem<ProductSeriesModel>(sqlCrudConfig);
	}

	public TemplateModel? GetItemTemplate(PluScaleModel pluScale)
	{
		if (!pluScale.IdentityIsNotNew || !pluScale.Plu.IdentityIsNotNew) return null;

		List<SqlFieldFilterModel> filters = GetFilters(
			nameof(SqlTableBase.IdentityValueId), pluScale.Plu.Template.IdentityValueId);
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters, false, false);
		return GetItem<TemplateModel>(sqlCrudConfig);
	}

	public AppModel GetItemAppOrCreateNew(string appName)
	{
		List<SqlFieldFilterModel> filters = GetFilters(nameof(SqlTableBase.Name), appName);
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters, false, false);
		AppModel app = GetItemNotNull<AppModel>(sqlCrudConfig);
		if (app.IdentityIsNew)
		{
			app = new()
			{
				Name = appName,
				CreateDt = DateTime.Now,
				ChangeDt = DateTime.Now,
				IsMarked = false,
			};
			Save(app);
		}
		return app;
	}

	public AppModel? GetItemApp(string appName)
	{
		List<SqlFieldFilterModel> filters = GetFilters(nameof(SqlTableBase.Name), appName);
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters, false, false);
		return GetItem<AppModel>(sqlCrudConfig);
	}

	public DeviceModel GetItemDeviceOrCreateNew(string deviceName)
	{
		List<SqlFieldFilterModel> filters = GetFilters(nameof(SqlTableBase.Name), deviceName);
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters, false, false);
		DeviceModel device = GetItemNotNull<DeviceModel>(sqlCrudConfig);
		if (device.IdentityIsNew)
		{
			device = new()
			{
				Name = deviceName,
				PrettyName = deviceName,
				CreateDt = DateTime.Now,
				ChangeDt = DateTime.Now,
				IsMarked = false,
				Ipv4 = NetUtils.GetLocalIpAddress(),
				LoginDt = DateTime.Now,
			};
			Save(device);
		}
		else
		{
			device.LoginDt = DateTime.Now;
			Update(device);
		}
		return device;
	}

	public ScaleModel? GetItemScale(DeviceModel device)
	{
		List<SqlFieldFilterModel> filters = GetFilters(
			$"{nameof(DeviceModel)}.{nameof(DeviceModel.IdentityValueUid)}", device.IdentityValueUid);
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters, false, false);
		return GetItemNotNull<DeviceScaleFkModel>(sqlCrudConfig).Scale;
	}

	public ScaleModel GetItemScaleNotNull(DeviceModel device) =>
		GetItemScale(device) ?? new();

	public DeviceModel? GetItemDevice(string deviceName)
	{
		List<SqlFieldFilterModel> filters = GetFilters(nameof(SqlTableBase.Name), deviceName);
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters, false, false);
		return GetItem<DeviceModel>(sqlCrudConfig);
	}

	public DeviceModel GetItemDeviceNotNull(string deviceName) => GetItemDevice(deviceName) ?? new();

	public DeviceModel? GetItemDevice(ScaleModel scale)
	{
		List<SqlFieldFilterModel> filters = GetFilters(
			$"{nameof(DeviceScaleFkModel.Scale)}.{nameof(ScaleModel.IdentityValueId)}", scale.IdentityValueId);
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters, false, false);
		return GetItem<DeviceScaleFkModel>(sqlCrudConfig)?.Device;
	}

	public DeviceModel GetItemDeviceNotNull(ScaleModel scale) => GetItemDevice(scale) ?? new();

	public DeviceTypeFkModel? GetItemDeviceTypeFk(DeviceModel device)
	{
		List<SqlFieldFilterModel> filters = GetFilters(
			$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.IdentityValueUid)}", device.IdentityValueUid);
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters, false, false);
		return GetItem<DeviceTypeFkModel>(sqlCrudConfig);
	}

	public DeviceTypeFkModel GetItemDeviceTypeFkNotNull(DeviceModel device) =>
		GetItemDeviceTypeFk(device) ?? new();

	public DeviceScaleFkModel? GetItemDeviceScaleFk(DeviceModel device)
	{
		List<SqlFieldFilterModel> filters = GetFilters(
			$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.IdentityValueUid)}", device.IdentityValueUid);
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters, false, false);
		return GetItem<DeviceScaleFkModel>(sqlCrudConfig);
	}

	public DeviceScaleFkModel? GetItemDeviceScaleFk(ScaleModel scale)
	{
		List<SqlFieldFilterModel> filters = GetFilters(
			$"{nameof(DeviceScaleFkModel.Scale)}.{nameof(ScaleModel.IdentityValueId)}", scale.IdentityValueId);
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(filters, false, false);
		return GetItem<DeviceScaleFkModel>(sqlCrudConfig);
	}

	public DeviceScaleFkModel GetItemDeviceScaleFkNotNull(DeviceModel device) =>
		GetItemDeviceScaleFk(device) ?? new();

	public LogTypeModel? GetItemLogType(LogTypeEnum logType)
	{
		SqlCrudConfigModel sqlCrudConfig = new(new() { new(nameof(LogTypeModel.Number), SqlFieldComparerEnum.Equal, (byte)logType) },
			new(), true, true, false, false, 0);
		return GetItem<LogTypeModel>(sqlCrudConfig);
	}

	public string GetAccessRightsDescription(AccessRightsEnum? accessRights)
	{
		return accessRights switch
		{
			AccessRightsEnum.Read => LocaleCore.Strings.AccessRightsRead,
			AccessRightsEnum.Write => LocaleCore.Strings.AccessRightsWrite,
			AccessRightsEnum.Admin => LocaleCore.Strings.AccessRightsAdmin,
			_ => LocaleCore.Strings.AccessRightsNone,
		};
	}

	public string GetAccessRightsDescription(byte accessRights) =>
		GetAccessRightsDescription((AccessRightsEnum)accessRights);

	#endregion
}
