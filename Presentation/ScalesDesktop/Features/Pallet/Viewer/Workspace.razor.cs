using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Resources;
using ScalesDesktop.Services;
using Ws.Domain.Models.Entities.Print;
using Ws.Shared.Enums;

namespace ScalesDesktop.Features.Pallet.Viewer;

public sealed partial class Workspace : ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Inject] private LineContext LineContext { get; set; } = null!;
    
    private List<EnumTypeModel<string>> TabsButtonList { get; set; } = [];
    private string SelectedTab { get; set; } = "main";
    private Tabs TabsRef { get; set; } = null!;

    protected override void OnInitialized()
    {
        TabsButtonList = [new("Информация", "main"), new("Этикетки", "labels")];
        PalletContext.OnStateChanged += StateHasChanged;
    } 

    private void CloseCurrentPallet() => PalletContext.ChangePallet(new());

    private bool IsSelectedTab(string tabName) => SelectedTab == tabName;

    private async Task ChangeSelectedTab(string tabName)
    {
        await TabsRef.SelectTab(tabName);
        StateHasChanged();
    }

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}