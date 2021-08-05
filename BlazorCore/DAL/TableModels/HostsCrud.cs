using BlazorCore.DAL.DataModels;
using BlazorCore.Utils;
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

        public List<HostsEntity> GetFreeHosts(int? id)
        {
            var entities = DataAccess.HostsCrud.GetEntitiesNativeObject(SqlQueries.GetFreeHosts);
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

        public List<HostsEntity> GetBusyHosts()
        {
            var entities = DataAccess.HostsCrud.GetEntitiesNativeObject(SqlQueries.GetBusyHosts);
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
