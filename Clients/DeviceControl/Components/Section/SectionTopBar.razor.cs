// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;
using WsLocalizationCore.Utils;
using WsStorageCore.Common;

namespace DeviceControl.Components.Section;

public partial class SectionTopBar : LayoutComponentBase
{
    #region Public and private fields, properties, constructor

    [Parameter] public string Title { get; set; }
    [Parameter] public WsSqlCrudConfigModel SqlCrudConfigSection { get; set; }
    [Parameter] public ButtonSettingsModel ButtonSettings { get; set; }
    [Parameter] public EventCallback<bool> OnSectionUpdate { get; set; }
    [Parameter] public EventCallback OnSectionAdd { get; set; }
    [Parameter] public int SectionCount { get; set; }
    private string SqlListCountResult => $"{WsLocaleCore.Strings.ItemsCount}: {SectionCount:### ### ###}";

    private static Dictionary<string, WsSqlEnumIsMarked> MarkedDict => new()
    {
        { "Актуальные", WsSqlEnumIsMarked.ShowOnlyActual },
        { "Cкрытые", WsSqlEnumIsMarked.ShowOnlyHide },
        { "Все", WsSqlEnumIsMarked.ShowAll }
    };

    private List<int> _rowCountList = new() { 0, 200, 400, 600, 800, 1000 };

    #endregion

    #region Public and private methods

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
            return;
        if (_rowCountList.Contains(SqlCrudConfigSection.SelectTopRowsCount))
            return;
        _rowCountList.Add(SqlCrudConfigSection.SelectTopRowsCount);
        _rowCountList.Sort();
    }

    private void OnItemsCountSelectUpdate()
    {
        OnSectionUpdate.InvokeAsync(false);
    }

    private void OnReload()
    {
        OnSectionUpdate.InvokeAsync(true);
    }

    #endregion
}