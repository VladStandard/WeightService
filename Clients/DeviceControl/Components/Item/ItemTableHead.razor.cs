// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Utils;
using FluentNHibernate.Conventions;
using WsBlazorCore.CssStyles;

namespace DeviceControl.Components.Item;

public partial class ItemTableHead : ComponentBase
{
    #region Public and private fields, properties, constructor

    private CssStyleTableHeadModel TableHeadModel { get; set; }
    [Parameter] public List<int> HeadWidth { get; set; } = new();

    #endregion

    protected override void OnInitialized()
    {
        TableHeadModel = HeadWidth.Any() ? 
            new(HeadWidth) : new CssStyleTableHeadModel();
    }
    
}