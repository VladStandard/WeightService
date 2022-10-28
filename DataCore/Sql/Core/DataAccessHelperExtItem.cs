// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public static partial class DataAccessHelperExt
{
	#region Public and private methods

	public static AccessModel? GetItemAccess(this DataAccessHelper dataAccess, string? userName)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(
			nameof(AccessModel.Name), SqlFieldComparerEnum.Equal, userName), 0, false, false);
		return dataAccess.GetItem<AccessModel>(sqlCrudConfig);
	}

	public static ProductSeriesModel? GetItemProductSeries(this DataAccessHelper dataAccess, ScaleModel scale)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new List<SqlFieldFilterModel>
			{
				new(nameof(ProductSeriesModel.IsClose), SqlFieldComparerEnum.Equal, false),
				new($"{nameof(ProductSeriesModel.Scale)}.{nameof(ScaleModel.IdentityValueId)}", 					SqlFieldComparerEnum.Equal, scale.IdentityValueId),
			},
			0, false, false);
		return dataAccess.GetItem<ProductSeriesModel>(sqlCrudConfig);
	}

	public static TemplateModel? GetItemTemplate(this DataAccessHelper dataAccess, PluScaleModel pluScale)
	{
		if (!pluScale.IdentityIsNotNew || !pluScale.Plu.IdentityIsNotNew) return null;

		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldFilterModel(nameof(SqlTableBase.IdentityValueId), SqlFieldComparerEnum.Equal, 
				pluScale.Plu.Template.IdentityValueId), 
			0, false, false);
		return dataAccess.GetItem<TemplateModel>(sqlCrudConfig);
	}

	public static AppModel? GetOrCreateNewApp(this DataAccessHelper dataAccess, string appName)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldFilterModel(nameof(AppModel.Name), SqlFieldComparerEnum.Equal, appName), 0, false, false);
		AppModel? app = dataAccess.GetItem<AppModel>(sqlCrudConfig);
		if (app is null || app.EqualsDefault())
		{
			app = new()
			{
				Name = appName,
				CreateDt = DateTime.Now,
				ChangeDt = DateTime.Now,
				IsMarked = false,
			};
			dataAccess.Save(app);
		}
		return app;
	}

	public static AppModel? GetItemApp(this DataAccessHelper dataAccess, string appName)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldFilterModel(nameof(AppModel.Name), SqlFieldComparerEnum.Equal, appName), 0, false, false);
		return dataAccess.GetItem<AppModel>(sqlCrudConfig);
	}

	public static DeviceModel? GetItemOrCreateNewDevice(this DataAccessHelper dataAccess, string hostName)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldFilterModel(nameof(DeviceModel.Name), SqlFieldComparerEnum.Equal, hostName), 0, false, false);
		DeviceModel? device = dataAccess.GetItem<DeviceModel>(sqlCrudConfig);
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
			dataAccess.Save(device);
		}
		else
		{
			device.LoginDt = DateTime.Now;
			dataAccess.Update(device);
		}
		return device;
	}

	//public static HostModel? GetItemOrCreateNewHost(this DataAccessHelper dataAccess, string hostName)
	//{
	//	HostModel? host;
	//	if (!string.IsNullOrEmpty(hostName))
	//	{
	//		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(nameof(HostModel.HostName), SqlFieldComparerEnum.Equal, hostName), 0, false, false);
	//		item = dataAccess.GetItem<HostModel>(sqlCrudConfig);
	//		if (item is null || item.EqualsDefault())
	//		{
	//			item = new()
	//			{
	//				Name = hostName,
	//				HostName = hostName,
	//				CreateDt = DateTime.Now,
	//				ChangeDt = DateTime.Now,
	//				IsMarked = false,
	//				Ip = NetUtils.GetLocalIpAddress(),
	//				LoginDt = DateTime.Now,
	//			};
	//			dataAccess.Save(item);
	//		}
	//		else
	//		{
	//			item.LoginDt = DateTime.Now;
	//			dataAccess.Update(item);
	//		}
	//	}
	//	return item;
	//}

	public static ScaleModel? GetItemScale(this DataAccessHelper dataAccess, DeviceModel device)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(
			$"{nameof(DeviceModel)}.{nameof(DeviceModel.IdentityValueUid)}", SqlFieldComparerEnum.Equal, device.IdentityValueUid),
			0, false, false);
		return dataAccess.GetItemNotNull<DeviceScaleFkModel>(sqlCrudConfig).Scale;
	}

	public static ScaleModel GetItemScaleNotNull(this DataAccessHelper dataAccess, DeviceModel device) =>
		dataAccess.GetItemScale(device) ?? new();

	public static DeviceModel? GetItemDevice(this DataAccessHelper dataAccess, string deviceName)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(
			$"{nameof(DeviceModel.Name)}", SqlFieldComparerEnum.Equal, deviceName), 
			0, false, false);
		return dataAccess.GetItem<DeviceModel>(sqlCrudConfig);
	}

	public static DeviceModel GetItemDeviceNotNull(this DataAccessHelper dataAccess, string deviceName) => 
		dataAccess.GetItemDevice(deviceName) ?? new();

	public static DeviceModel? GetItemDevice(this DataAccessHelper dataAccess, ScaleModel scale)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(
			$"{nameof(DeviceScaleFkModel.Scale)}.{nameof(ScaleModel.IdentityValueId)}", SqlFieldComparerEnum.Equal, scale.IdentityValueId),
			0, false, false);
		return dataAccess.GetItem<DeviceScaleFkModel>(sqlCrudConfig)?.Device;
	}

	public static DeviceModel GetItemDeviceNotNull(this DataAccessHelper dataAccess, ScaleModel scale) => 
		dataAccess.GetItemDevice(scale) ?? new();

	public static DeviceTypeFkModel? GetItemDeviceTypeFk(this DataAccessHelper dataAccess, DeviceModel device)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(
				$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.IdentityValueUid)}", SqlFieldComparerEnum.Equal,  device.IdentityValueUid),
			0, false, false);
		return dataAccess.GetItem<DeviceTypeFkModel>(sqlCrudConfig);
	}

	public static DeviceTypeFkModel GetItemDeviceTypeFkNotNull(this DataAccessHelper dataAccess, DeviceModel device) => 
		dataAccess.GetItemDeviceTypeFk(device) ?? new();

	public static DeviceScaleFkModel? GetItemDeviceScaleFk(this DataAccessHelper dataAccess, DeviceModel device)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(
				nameof(DeviceScaleFkModel.Device.Name), SqlFieldComparerEnum.Equal, device.Name),
			0, false, false);
		return dataAccess.GetItem<DeviceScaleFkModel>(sqlCrudConfig);
	}

	public static DeviceScaleFkModel GetItemDeviceScaleFkNotNull(this DataAccessHelper dataAccess, DeviceModel device) =>
		dataAccess.GetItemDeviceScaleFk(device) ?? new();

	public static LogTypeModel? GetItemLogType(this DataAccessHelper dataAccess, LogTypeEnum logType)
	{
        SqlCrudConfigModel sqlCrudConfig = new(
            new SqlFieldFilterModel(nameof(LogTypeModel.Number), SqlFieldComparerEnum.Equal, (byte)logType));
        return dataAccess.GetItem<LogTypeModel>(sqlCrudConfig);
	}

	public static string GetAccessRightsDescription(this DataAccessHelper dataAccess, AccessRightsEnum? accessRights)
	{
		return accessRights switch
		{
			AccessRightsEnum.Read => LocaleCore.Strings.AccessRightsRead,
			AccessRightsEnum.Write => LocaleCore.Strings.AccessRightsWrite,
			AccessRightsEnum.Admin => LocaleCore.Strings.AccessRightsAdmin,
			_ => LocaleCore.Strings.AccessRightsNone,
		};
	}

	public static string GetAccessRightsDescription(this DataAccessHelper dataAccess, byte accessRights) =>
		GetAccessRightsDescription(dataAccess, (AccessRightsEnum)accessRights);

	#endregion
}
