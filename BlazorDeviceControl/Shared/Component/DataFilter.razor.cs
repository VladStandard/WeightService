// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Component
{
    public partial class DataFilter
    {
        #region Public and private fields and properties

        [Parameter] public string ItemTypeName { get; set; }
        [Parameter] public IBaseEntity[] Items { get; set; }

        #endregion

        #region Public and private methods

        //

        #endregion
    }
}
