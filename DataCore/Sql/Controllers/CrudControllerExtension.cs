// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Protocols;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Controllers;

public static class CrudControllerExtension
{
    #region Public and private methods

    public static AppEntity? GetOrCreateNewApp(this CrudController crud, string? appName)
    {
        AppEntity? app = null;
        if (!string.IsNullOrEmpty(appName) && appName is { })
        {
            app = crud.GetEntity<AppEntity>(
                new(new() { new(DbField.Name, DbComparer.Equal, appName),
                    new(DbField.IsMarked, DbComparer.Equal, false),
                }));
            if (app.EqualsDefault())
            {
                app = new()
                {
                    Name = appName,
                    CreateDt = DateTime.Now,
                    ChangeDt = DateTime.Now,
                    IsMarked = false,
                };
                crud.SaveEntity(app);
            }
        }
        return app;
    }

    public static AppEntity? GetApp(this CrudController crud, string? appName)
    {
        AppEntity? app = null;
        if (!string.IsNullOrEmpty(appName) && appName is { })
        {
            app = crud.GetEntity<AppEntity>(
                new(new() { new(DbField.Name, DbComparer.Equal, appName),
                    new(DbField.IsMarked, DbComparer.Equal, false),
                }));
        }
        return app;
    }

	public static HostEntity? GetOrCreateNewHost(this CrudController crud, string? hostName)
	{
		HostEntity? host = null;
		if (!string.IsNullOrEmpty(hostName) && hostName is { })
		{
			host = crud.GetEntity<HostEntity>(
				new(new() { new(DbField.HostName, DbComparer.Equal, hostName),
					new(DbField.IsMarked, DbComparer.Equal, false),
				}));
			if (host.EqualsDefault())
			{
				host = new()
				{
					Name = hostName,
					HostName = hostName,
					CreateDt = DateTime.Now,
					ChangeDt = DateTime.Now,
					IsMarked = false,
					Ip = NetUtils.GetLocalIpAddress(),
					AccessDt = DateTime.Now,
				};
				crud.SaveEntity(host);
			}
			else
			{
				host.AccessDt = DateTime.Now;
				crud.UpdateEntity(host);
			}
		}
		return host;
	}

	public static HostEntity? GetHost(this CrudController crud, string? hostName)
	{
		HostEntity? host = null;
		if (!string.IsNullOrEmpty(hostName) && hostName is { })
		{
			host = crud.GetEntity<HostEntity>(
				new(new() { new(DbField.HostName, DbComparer.Equal, hostName),
					new(DbField.IsMarked, DbComparer.Equal, false),
				}));
			if (!host.EqualsDefault())
			{
				host.AccessDt = DateTime.Now;
				crud.UpdateEntity(host);
			}
		}
		return host;
	}

	public static List<HostEntity> GetHostsFree(this CrudController crud, long? id, bool? isMarked,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		object[] entities = crud.GetEntitiesNativeObject(SqlQueries.DbScales.Tables.Hosts.GetFreeHosts, filePath, lineNumber, memberName);
		List<HostEntity> items = new();
		foreach (object? item in entities)
		{
			if (item is object[] { Length: 10 } obj)
			{
				if (long.TryParse(Convert.ToString(obj[0]), out long idOut))
				{
					HostEntity host = new()
					{
						IdentityId = idOut,
						CreateDt = Convert.ToDateTime(obj[1]),
						ChangeDt = Convert.ToDateTime(obj[2]),
						AccessDt = Convert.ToDateTime(obj[3]),
						Name = Convert.ToString(obj[4]),
						Ip = Convert.ToString(obj[5]),
						MacAddress = new(Convert.ToString(obj[6])),
						IsMarked = Convert.ToBoolean(obj[7]),
					};
					if ((id == null || Equals(host.IdentityId, id)) && (isMarked == null || Equals(host.IsMarked, isMarked)))
						items.Add(host);
				}
			}
		}
		return items;
	}

	public static List<HostEntity> GetHostsBusy(this CrudController crud, long? id, bool? isMarked,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		object[] entities = crud.GetEntitiesNativeObject(SqlQueries.DbScales.Tables.Hosts.GetBusyHosts, filePath, lineNumber, memberName);
		List<HostEntity> items = new();
		foreach (object? item in entities)
		{
			if (item is object[] { Length: 12 } obj)
			{
				if (long.TryParse(Convert.ToString(obj[0]), out long idOut))
				{
					HostEntity host = new()
					{
						IdentityId = idOut,
						CreateDt = Convert.ToDateTime(obj[1]),
						ChangeDt = Convert.ToDateTime(obj[2]),
						AccessDt = Convert.ToDateTime(obj[3]),
						Name = Convert.ToString(obj[4]),
						Ip = Convert.ToString(obj[7]),
						MacAddress = new(Convert.ToString(obj[8])),
						IsMarked = Convert.ToBoolean(obj[9]),
					};
					if ((id == null || Equals(host.IdentityId, id)) && (isMarked == null || Equals(host.IsMarked, isMarked)))
						items.Add(host);
				}
			}
		}
		return items;
	}

	public static T GetEntityNotNull<T>(this CrudController crud, FieldEntity filter, FieldOrderEntity? order = null, 
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
		where T : BaseEntity, new()
	{
		return GetEntityNotNull<T>(crud, new FilterListEntity(new() { filter }), order, filePath, lineNumber, memberName);
	}

	public static T GetEntityNotNull<T>(this CrudController crud, FilterListEntity? filterList, FieldOrderEntity? order = null, 
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
		where T : BaseEntity, new()
	{
		T? item = crud.GetEntity<T>(filterList, order, filePath, lineNumber, memberName);
		if (item is { })
			return item;
		return new();
	}

	public static List<T> GetEntitiesNotNull<T>(this CrudController crud, bool isShowMarkedItems, bool isSelectTopRows, FieldOrderEntity? order = null, 
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
		where T : BaseEntity, new()
	{
		T[]? items = crud.GetEntities<T>(isShowMarkedItems ? null
			: new FilterListEntity(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
			order, isSelectTopRows ? crud.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0,
			filePath, lineNumber, memberName);
		if (items is not null && items.Length > 0)
			return items.ToList();
		return new();
	}

	#endregion
}
