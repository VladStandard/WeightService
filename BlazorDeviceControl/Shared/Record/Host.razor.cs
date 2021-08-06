// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL.TableModels;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Record
{
    public partial class Host
    {
        #region Public and private fields and properties

        [Parameter] public HostsEntity Item { get; set; }

        #endregion

        #region Public and private methods


        #endregion
    }
}