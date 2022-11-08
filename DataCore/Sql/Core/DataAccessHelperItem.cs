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
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(nameof(SqlTableBase.Name), userName, false, false);
		return GetItemNullable<AccessModel>(sqlCrudConfig);
	}

	public ProductSeriesModel? GetItemProductSeries(ScaleModel scale)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
			new List<SqlFieldFilterModel>
			{
				new(nameof(ProductSeriesModel.IsClose), SqlFieldComparerEnum.Equal, false),
				new($"{nameof(ProductSeriesModel.Scale)}.{nameof(ScaleModel.IdentityValueId)}",                     SqlFieldComparerEnum.Equal, scale.IdentityValueId),
			}, false, false);
		return GetItemNullable<ProductSeriesModel>(sqlCrudConfig);
	}

	public TemplateModel? GetItemTemplate(PluScaleModel pluScale)
	{
		if (!pluScale.IdentityIsNotNew || !pluScale.Plu.IdentityIsNotNew) return null;

		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(nameof(SqlTableBase.IdentityValueId), pluScale.Plu.Template.IdentityValueId, false, false);
		return GetItemNullable<TemplateModel>(sqlCrudConfig);
	}

	public AppModel GetItemAppOrCreateNew(string appName)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(nameof(SqlTableBase.Name), appName, false, false);
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
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(nameof(SqlTableBase.Name), appName, false, false);
		return GetItemNullable<AppModel>(sqlCrudConfig);
	}

	public DeviceModel GetItemDeviceOrCreateNew(string name)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(nameof(SqlTableBase.Name), name, false, false);
		DeviceModel device = GetItemNotNull<DeviceModel>(sqlCrudConfig);
		if (device.IdentityIsNew)
		{
			device = new()
			{
				Name = name,
				PrettyName = name,
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

	private ScaleModel? GetItemScale(DeviceModel device)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
			SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceModel), device.IdentityValueUid), false, false);
		return GetItemNotNull<DeviceScaleFkModel>(sqlCrudConfig).Scale;
	}

	public ScaleModel GetItemScaleNotNull(DeviceModel device) =>
		GetItemScale(device) ?? new();

	private DeviceModel? GetItemDevice(string name)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(nameof(SqlTableBase.Name), name, false, false);
		return GetItemNullable<DeviceModel>(sqlCrudConfig);
	}

	public DeviceModel? GetItemDevice(ScaleModel scale)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
			SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceScaleFkModel.Scale), scale.IdentityValueId), false, false);
		return GetItemNullable<DeviceScaleFkModel>(sqlCrudConfig)?.Device;
	}

	public DeviceModel GetItemDeviceNotNull(string name) => GetItemDevice(name) ?? new();

	public DeviceModel GetItemDeviceNotNull(ScaleModel scale) => GetItemDevice(scale) ?? new();

	public DeviceTypeFkModel? GetItemDeviceTypeFk(DeviceModel device)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
			SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceTypeFkModel.Device), device.IdentityValueUid), false, false);
		return GetItemNullable<DeviceTypeFkModel>(sqlCrudConfig);
	}

	public DeviceTypeFkModel GetItemDeviceTypeFkNotNull(DeviceModel device) =>
		GetItemDeviceTypeFk(device) ?? new();

	public DeviceScaleFkModel? GetItemDeviceScaleFk(DeviceModel device)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
			SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceTypeFkModel.Device), device.IdentityValueUid), false, false);
		return GetItemNullable<DeviceScaleFkModel>(sqlCrudConfig);
	}

	public DeviceScaleFkModel? GetItemDeviceScaleFk(ScaleModel scale)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
			SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceScaleFkModel.Scale), scale.IdentityValueId), false, false);
		return GetItemNullable<DeviceScaleFkModel>(sqlCrudConfig);
	}

	public DeviceScaleFkModel GetItemDeviceScaleFkNotNull(DeviceModel device) =>
		GetItemDeviceScaleFk(device) ?? new();

	public LogTypeModel? GetItemLogType(LogTypeEnum logType)
	{
		SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
			{ new(nameof(LogTypeModel.Number), SqlFieldComparerEnum.Equal, (byte)logType) },
			true, true, false, false);
		return GetItemNullable<LogTypeModel>(sqlCrudConfig);
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
