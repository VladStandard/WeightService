// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.SectionComponents;

public partial class SectionReload<TItem> : RazorComponentSectionBase<TItem>
    where TItem : WsSqlTableBase, new()
{
    #region Public and private fields, properties, constructor

    private string SqlListCountResult => $"{LocaleCore.Strings.ItemsCount}: {SectionCount:### ### ###}";
    [Parameter] public EventCallback OnSectionUpdate { get; set; }
    [Parameter] public int SectionCount { get; set; }

    private static Dictionary<string, WsSqlIsMarked> MarkedDict => new()
    {
        { "Актуальные", WsSqlIsMarked.ShowOnlyActual },
        { "Cкрытые", WsSqlIsMarked.ShowOnlyHide },
        { "Все", WsSqlIsMarked.ShowAll },
    };

    #endregion

    #region Public and private methods

    protected override void OnAfterRender(bool firstRender) { }

    #endregion
}
