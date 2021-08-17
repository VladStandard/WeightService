﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class EntityActions
    {
        #region Public and private fields and properties

        [Parameter] public BaseEntity ParentItem { get; set; }
        [Parameter] public BaseEntity ChildItem { get; set; }

        #endregion
    }
}