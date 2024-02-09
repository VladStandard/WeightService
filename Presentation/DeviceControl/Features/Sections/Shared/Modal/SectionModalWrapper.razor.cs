using Blazorise;
using Microsoft.AspNetCore.Components;
using Ws.Shared.Enums;

namespace DeviceControl.Features.Sections.Shared.Modal;

public sealed partial class SectionModalWrapper : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public List<EnumTypeModel<string>> TabsButtonList { get; set; } = [];
    [Parameter] public string SelectedTab { get; set; } = "main";

    private Tabs TabsRef { get; set; } = null!;

    private bool IsSelectedTab(string tabName) => SelectedTab == tabName;

    private async Task ChangeSelectedTab(string tabName)
    {
        await TabsRef.SelectTab(tabName);
        StateHasChanged();
    }
}