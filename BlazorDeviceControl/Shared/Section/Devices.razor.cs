// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.DataModels;
using DataShareCore.DAL.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Devices
    {
        #region Public and private fields and properties

        private List<DeviceEntity> ItemsCast => Items == null ? new List<DeviceEntity>() : Items.Select(x => (DeviceEntity)x).ToList();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            await GetDataAsync(new Task(delegate
            {
                Items = AppSettings.DataAccess.DeviceCrud.GetEntities<DeviceEntity>(null, null).ToList<IBaseEntity>();
            }), false).ConfigureAwait(false);
        }

        #endregion
    }
}
