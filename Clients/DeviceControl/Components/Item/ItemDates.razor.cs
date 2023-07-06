// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.ComponentModel.DataAnnotations;

namespace DeviceControl.Components.Item;

public partial class ItemDates : LayoutComponentBase
{
    #region Public and private fields, properties, constructor
    
    [Parameter] public WsSqlTableBase SqlItem { get; set; }

    #endregion
}