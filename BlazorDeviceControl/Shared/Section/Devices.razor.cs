// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL.DataModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Devices
    {
        #region Public and private fields and properties

        public List<DeviceEntity> Items { get; set; }
        public DeviceEntity Entity { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            await GetDataAsync(new Task(delegate
            {
                Items = AppSettings.DataAccess.DeviceCrud.GetEntities(null, null).ToList();
            }), false).ConfigureAwait(false);
        }

        #endregion
    }
}
