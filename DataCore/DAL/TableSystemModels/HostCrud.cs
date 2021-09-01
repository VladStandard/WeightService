// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.Utils;
using DataCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataCore.DAL.TableSystemModels
{
    public class HostCrud : BaseCrud<HostEntity>
    {
        #region Constructor and destructor

        public HostCrud(DataAccessEntity dataAccess) : base(dataAccess)
        {
            //
        }

        #endregion

        #region Public and private methods

        public List<HostEntity> GetFreeHosts(int? id)
        {
            var entities = DataAccess.HostsCrud.GetEntitiesNativeObject(SqlQueries.GetFreeHosts);
            var items = new List<HostEntity>();
            foreach (var entity in entities)
            {
                if (entity is object[] { Length: 9 } ent)
                {
                    items.Add(new HostEntity
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

        public List<HostEntity> GetBusyHosts()
        {
            var entities = DataAccess.HostsCrud.GetEntitiesNativeObject(SqlQueries.GetBusyHosts);
            var items = new List<HostEntity>();
            foreach (var entity in entities)
            {
                if (entity is object[] { Length: 9 } ent)
                {
                    items.Add(new HostEntity
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
