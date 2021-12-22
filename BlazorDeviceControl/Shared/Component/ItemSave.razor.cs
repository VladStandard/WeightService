// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components;
using System;

namespace BlazorDeviceControl.Shared.Component
{
    public partial class ItemSave
    {
        #region Public and private fields and properties

        [Parameter] public int? Id { get; set; }
        [Parameter] public Guid? Uid { get; set; }

        #endregion

        #region Public and private methods

        //

        #endregion
    }
}
