// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public partial class DataAccessHelper
{
	#region Public and private methods

	public AccessModel? GetItemAccess(string? userName)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(
			nameof(AccessModel.Name), SqlFieldComparerEnum.Equal, userName), 0, false, false);
		return GetItem<AccessModel>(sqlCrudConfig);
	}

	public ProductSeriesModel? GetItemProductSeries(ScaleModel scale)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new List<SqlFieldFilterModel>
			{
				new(nameof(ProductSeriesModel.IsClose), SqlFieldComparerEnum.Equal, false),
				new($"{nameof(ProductSeriesModel.Scale)}.{nameof(ScaleModel.IdentityValueId)}", 					SqlFieldComparerEnum.Equal, scale.IdentityValueId),
			},
			0, false, false);
		return GetItem<ProductSeriesModel>(sqlCrudConfig);
	}

	public TemplateModel? GetItemTemplate(PluScaleModel pluScale)
	{
		if (!pluScale.IdentityIsNotNew || !pluScale.Plu.IdentityIsNotNew) return null;

		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldFilterModel(nameof(SqlTableBase.IdentityValueId), SqlFieldComparerEnum.Equal, 
				pluScale.Plu.Template.IdentityValueId), 
			0, false, false);
		return GetItem<TemplateModel>(sqlCrudConfig);
	}

	public AppModel? GetOrCreateNewApp(string appName)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldFilterModel(nameof(AppModel.Name), SqlFieldComparerEnum.Equal, appName), 0, false, false);
		AppModel? app = GetItem<AppModel>(sqlCrudConfig);
		if (app is null || app.EqualsDefault())
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
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldFilterModel(nameof(AppModel.Name), SqlFieldComparerEnum.Equal, appName), 0, false, false);
		return GetItem<AppModel>(sqlCrudConfig);
	}

	public DeviceModel? GetItemOrCreateNewDevice(string hostName)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldFilterModel(nameof(DeviceModel.Name), SqlFieldComparerEnum.Equal, hostName), 0, false, false);
		DeviceModel? device = GetItem<DeviceModel>(sqlCrudConfig);
		if (device is null || device.EqualsDefault())
		{
			device = new()
			{
				Name = hostName,
				PrettyName = hostName,
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
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(
			$"{nameof(DeviceModel)}.{nameof(DeviceModel.IdentityValueUid)}", SqlFieldComparerEnum.Equal, device.IdentityValueUid),
			0, false, false);
		return GetItemNotNull<DeviceScaleFkModel>(sqlCrudConfig).Scale;
	}

	public ScaleModel GetItemScaleNotNull(DeviceModel device) =>
		GetItemScale(device) ?? new();

	public DeviceModel? GetItemDevice(string deviceName)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(
			$"{nameof(DeviceModel.Name)}", SqlFieldComparerEnum.Equal, deviceName), 
			0, false, false);
		return GetItem<DeviceModel>(sqlCrudConfig);
	}

	public DeviceModel GetItemDeviceNotNull(string deviceName) => 
		GetItemDevice(deviceName) ?? new();

	public DeviceModel? GetItemDevice(ScaleModel scale)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(
			$"{nameof(DeviceScaleFkModel.Scale)}.{nameof(ScaleModel.IdentityValueId)}", SqlFieldComparerEnum.Equal, scale.IdentityValueId),
			0, false, false);
		return GetItem<DeviceScaleFkModel>(sqlCrudConfig)?.Device;
	}

	public DeviceModel GetItemDeviceNotNull(ScaleModel scale) => 
		GetItemDevice(scale) ?? new();

	public DeviceTypeFkModel? GetItemDeviceTypeFk(DeviceModel device)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(
				$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.IdentityValueUid)}", SqlFieldComparerEnum.Equal,  device.IdentityValueUid),
			0, false, false);
		return GetItem<DeviceTypeFkModel>(sqlCrudConfig);
	}

	public DeviceTypeFkModel GetItemDeviceTypeFkNotNull(DeviceModel device) => 
		GetItemDeviceTypeFk(device) ?? new();

	public DeviceScaleFkModel? GetItemDeviceScaleFk(DeviceModel device)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(
				nameof(DeviceScaleFkModel.Device.Name), SqlFieldComparerEnum.Equal, device.Name),
			0, false, false);
		return GetItem<DeviceScaleFkModel>(sqlCrudConfig);
	}

	public DeviceScaleFkModel GetItemDeviceScaleFkNotNull(DeviceModel device) =>
		GetItemDeviceScaleFk(device) ?? new();

	public LogTypeModel? GetItemLogType(LogTypeEnum logType)
	{
        SqlCrudConfigModel sqlCrudConfig = new(
            new SqlFieldFilterModel(nameof(LogTypeModel.Number), SqlFieldComparerEnum.Equal, (byte)logType));
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
