using BlazorCore.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorCore.DAL.TableModels
{
    public class HostsCrud : BaseCrud<HostsEntity>
    {
        #region Constructor and destructor

        public HostsCrud(DataAccessEntity dataAccess) : base(dataAccess)
        {
            //
        }

        #endregion

        #region Public and private methods

        public string GetQueryFreeHosts()
        {
            return @"
-- Get free hosts
select [h].[Id]
      ,[h].[CreateDate]
      ,[h].[ModifiedDate]
      ,[h].[Name]
      ,[h].[IP]
      ,[h].[MAC]
      ,[h].[IdRRef]
      ,[h].[Marked]
      ,[h].[SettingsFile]
from [db_scales].[Hosts] [h]
where [h].[Id] not in (select [HostId] from [db_scales].[Scales] [s] where [s].[HostId] is not null and [s].[Marked] = 0) and [h].[Marked] = 0
order by [h].[Name]
            ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');
        }

        public List<HostsEntity> GetFreeHosts(int? id)
        {
            var entities = DataAccess.HostsCrud.GetEntitiesNativeObject(GetQueryFreeHosts());
            var items = new List<HostsEntity>();
            foreach (var entity in entities)
            {
                if (entity is object[] { Length: 9 } ent)
                {
                    items.Add(new HostsEntity
                    {
                        Id = Convert.ToInt32(ent[0]),
                        CreateDate = Convert.ToDateTime(ent[1]),
                        ModifiedDate = Convert.ToDateTime(ent[2]),
                        Name = Convert.ToString(ent[3]),
                        Ip = Convert.ToString(ent[4]),
                        Mac = Convert.ToString(ent[5]),
                        IdRRef = Guid.Parse(Convert.ToString(ent[6])),
                        Marked = Convert.ToBoolean(ent[7]),
                        SettingsFile = Convert.ToString(ent[8]),
                    });
                }
            }

            if (id > 0 && items.Select(x => x).Where(x => Equals(x.Id, id)).ToList().Count == 0)
            {
                items.Add(GetEntity(new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), id } }), null));
            }
            return items;
        }

        public string GetQueryBusyHosts()
        {
            return @"
-- Get busy hosts
select [h].[Id]
      ,[h].[CreateDate]
      ,[h].[ModifiedDate]
      ,[h].[Name]
      ,[s].[Description]
      ,[h].[IP]
      ,[h].[MAC]
      ,[h].[IdRRef]
      ,[h].[Marked]
      ,[h].[SettingsFile]
from [db_scales].[Hosts] [h]
left join [db_scales].[Scales] [s] on [h].[Id] = [s].[HostId]
where [h].[Id] in (select [HostId] from [db_scales].[Scales] where [Scales].[HostId] is not null and [s].[Marked] = 0) and [h].[Marked] = 0
order by [h].[Name]
            ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');
        }

        public List<HostsEntity> GetBusyHosts()
        {
            var entities = DataAccess.HostsCrud.GetEntitiesNativeObject(GetQueryBusyHosts());
            var items = new List<HostsEntity>();
            foreach (var entity in entities)
            {
                if (entity is object[] { Length: 9 } ent)
                {
                    items.Add(new HostsEntity
                    {
                        Id = Convert.ToInt32(ent[0]),
                        CreateDate = Convert.ToDateTime(ent[1]),
                        ModifiedDate = Convert.ToDateTime(ent[2]),
                        Name = Convert.ToString(ent[3]),
                        Ip = Convert.ToString(ent[4]),
                        Mac = Convert.ToString(ent[5]),
                        IdRRef = Guid.Parse(Convert.ToString(ent[6])),
                        Marked = Convert.ToBoolean(ent[7]),
                        SettingsFile = Convert.ToString(ent[8]),
                    });
                }
            }
            return items;
        }

        #endregion
    }
}
