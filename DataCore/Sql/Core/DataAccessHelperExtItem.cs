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
		AccessModel? item = null;
		if (!string.IsNullOrEmpty(userName))
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(nameof(AccessModel.User), SqlFieldComparerEnum.Equal, userName), 0, false, false);
			item = dataAccess.GetItem<AccessModel>(sqlCrudConfig);
		}
		return item;
	}

	public static AppModel? GetOrCreateNewApp(this DataAccessHelper dataAccess, string? appName)
	{
		AppModel? app = null;
		if (!string.IsNullOrEmpty(appName) && appName is not null)
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
				new SqlFieldFilterModel(nameof(AppModel.Name), SqlFieldComparerEnum.Equal, appName), 0, false, false);
			app = dataAccess.GetItem<AppModel>(sqlCrudConfig);
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
		}
		return app;
	}

	public static AppModel? GetItemApp(this DataAccessHelper dataAccess, string? appName)
	{
		AppModel? item = null;
		if (!string.IsNullOrEmpty(appName))
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(nameof(AppModel.Name), SqlFieldComparerEnum.Equal, appName), 0, false, false);
			item = dataAccess.GetItem<AppModel>(sqlCrudConfig);
		}
		return item;
	}

	public static HostModel? GetItemOrCreateNewHost(this DataAccessHelper dataAccess, string hostName)
	{
		HostModel? item = null;
		if (!string.IsNullOrEmpty(hostName))
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(nameof(HostModel.HostName), SqlFieldComparerEnum.Equal, hostName), 0, false, false);
			item = dataAccess.GetItem<HostModel>(sqlCrudConfig);
			if (item is null || item.EqualsDefault())
			{
				item = new()
				{
					Name = hostName,
					HostName = hostName,
					CreateDt = DateTime.Now,
					ChangeDt = DateTime.Now,
					IsMarked = false,
					Ip = NetUtils.GetLocalIpAddress(),
					AccessDt = DateTime.Now,
				};
				dataAccess.Save(item);
			}
			else
			{
				item.AccessDt = DateTime.Now;
				dataAccess.Update(item);
			}
		}
		return item;
	}

	public static HostModel? GetItemHost(this DataAccessHelper dataAccess, string? hostName)
	{
		HostModel? item = null;
		if (!string.IsNullOrEmpty(hostName))
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldFilterModel(nameof(HostModel.HostName), SqlFieldComparerEnum.Equal, hostName), 0, false, false);
			item = dataAccess.GetItem<HostModel>(sqlCrudConfig);
			if (item is not null && !item.EqualsDefault())
			{
				item.AccessDt = DateTime.Now;
				dataAccess.Update(item);
			}
		}
		return item;
	}

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
