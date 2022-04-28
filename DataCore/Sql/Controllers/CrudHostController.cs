// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using DataCore.Protocols;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Controllers
{
    public class CrudHostController
    {
        #region Public and private fields and properties

        public DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;

        #endregion

        #region Constructor and destructor

        public CrudHostController()
        {
            //
        }

        #endregion

        #region Public and private methods

        public HostEntity? GetOrCreateNew(string? hostName)
        {
            HostEntity? host = null;
            if (!string.IsNullOrEmpty(hostName) && hostName is string strName)
            {
                host = DataAccess.Crud.GetEntity<HostEntity>(
                    new FieldListEntity(new Dictionary<DbField, object?> {
                        { DbField.HostName, hostName },
                        { DbField.IsMarked, false },
                    }));
                if (host == null || host.EqualsDefault())
                {
                    host = new HostEntity()
                    {
                        Name = strName,
                        HostName = strName,
                        CreateDt = DateTime.Now,
                        ChangeDt = DateTime.Now,
                        IsMarked = false,
                        Ip = NetUtils.GetLocalIpAddress(),
                        AccessDt = DateTime.Now,
                    };
                    DataAccess.Crud.SaveEntity(host);
                }
                else
                {
                    host.AccessDt = DateTime.Now;
                    DataAccess.Crud.UpdateEntity(host);
                }
            }
            return host;
        }

        public List<HostEntity> GetFree(long? id, bool? isMarked,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            object[]? entities = DataAccess.Crud.GetEntitiesNativeObject(SqlQueries.DbScales.Tables.Hosts.GetFreeHosts, filePath, lineNumber, memberName);
            List<HostEntity>? items = new();
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
                            MacAddress = new MacAddressEntity(Convert.ToString(obj[6])),
                            IdRRef = Guid.Parse(Convert.ToString(obj[7])),
                            IsMarked = Convert.ToBoolean(obj[8]),
                            SettingsFile = Convert.ToString(obj[9]),
                        };
                        if ((id == null || Equals(host.IdentityId, id)) && (isMarked == null || Equals(host.IsMarked, isMarked)))
                            items.Add(host);
                    }
                }
            }
            return items;
        }

        public List<HostEntity> GetBusy(long? id, bool? isMarked,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            object[]? entities = DataAccess.Crud.GetEntitiesNativeObject(SqlQueries.DbScales.Tables.Hosts.GetBusyHosts, filePath, lineNumber, memberName);
            List<HostEntity>? items = new();
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
                            MacAddress = new MacAddressEntity(Convert.ToString(obj[8])),
                            IdRRef = Guid.Parse(Convert.ToString(obj[9])),
                            IsMarked = Convert.ToBoolean(obj[10]),
                            SettingsFile = Convert.ToString(obj[11]),
                        };
                        if ((id == null || Equals(host.IdentityId, id)) && (isMarked == null || Equals(host.IsMarked, isMarked)))
                            items.Add(host);
                    }
                }
            }
            return items;
        }

        #endregion
    }
}
