// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.CssStyles;

namespace DeviceControl.Components.Item;

public partial class ItemTableHead : ComponentBase
{
    #region Public and private fields, properties, constructor

    [Parameter] public CssStyleTableHeadModel CssTableStyleHead { get; set; }

    #endregion

    #region Public and private methods

    public ItemTableHead()
    {
        CssTableStyleHead = new();
    }

    #endregion
}