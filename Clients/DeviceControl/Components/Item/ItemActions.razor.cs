// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;

namespace DeviceControl.Components.Item;

public partial class ItemActions<TItem> : ItemBase<TItem> where TItem : WsSqlTableBase, new()
{
    #region Public and private fields, properties, constructor

    [Parameter] public EventCallback OnItemSave { get; set; }

    [Parameter] public EventCallback OnItemCancel { get; set; }
    
    [Parameter] public new ButtonSettingsModel ButtonSettings { get; set; }
    
    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast() {}

    #endregion
}
